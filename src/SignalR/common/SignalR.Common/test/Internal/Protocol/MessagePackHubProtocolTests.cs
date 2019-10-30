// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.SignalR.Protocol;
using Xunit;

namespace Microsoft.AspNetCore.SignalR.Common.Tests.Internal.Protocol
{
    using static HubMessageHelpers;

    public class MessagePackHubProtocolTests : MessagePackHubProtocolTestBase
    {
        protected override IHubProtocol HubProtocol => new MessagePackHubProtocol();

        [Fact]
        public void SerializerCanSerializeTypesWithNoDefaultCtor()
        {
            var result = Write(CompletionMessage.WithResult("0", new List<int> { 42 }.AsReadOnly()));
            AssertMessages(new byte[] { ArrayBytes(5), 3, 0x80, StringBytes(1), (byte)'0', 0x03, ArrayBytes(1), 42 }, result);
        }

        [Fact]
        public void WriteAndParseDateTimeConvertsToUTC()
        {
            var dateTime = new DateTime(2018, 4, 9);
            var writer = MemoryBufferWriter.Get();

            try
            {
                HubProtocol.WriteMessage(CompletionMessage.WithResult("xyz", dateTime), writer);
                var bytes = new ReadOnlySequence<byte>(writer.ToArray());
                HubProtocol.TryParseMessage(ref bytes, new TestBinder(typeof(DateTime)), out var hubMessage);

                var completionMessage = Assert.IsType<CompletionMessage>(hubMessage);

                var resultDateTime = (DateTime)completionMessage.Result;
                // The messagepack Timestamp format specifies that time is stored as seconds since 1970-01-01 UTC
                // so the library has no choice but to store the time as UTC
                // https://github.com/msgpack/msgpack/blob/master/spec.md#timestamp-extension-type
                Assert.Equal(dateTime.ToUniversalTime(), resultDateTime);
            }
            finally
            {
                MemoryBufferWriter.Return(writer);
            }
        }

        [Fact]
        public void WriteAndParseDateTimeOffset()
        {
            var dateTimeOffset = new DateTimeOffset(new DateTime(2018, 4, 9), TimeSpan.FromHours(10));
            var writer = MemoryBufferWriter.Get();

            try
            {
                HubProtocol.WriteMessage(CompletionMessage.WithResult("xyz", dateTimeOffset), writer);
                var bytes = new ReadOnlySequence<byte>(writer.ToArray());
                HubProtocol.TryParseMessage(ref bytes, new TestBinder(typeof(DateTimeOffset)), out var hubMessage);

                var completionMessage = Assert.IsType<CompletionMessage>(hubMessage);

                var resultDateTimeOffset = (DateTimeOffset)completionMessage.Result;
                Assert.Equal(dateTimeOffset, resultDateTimeOffset);
            }
            finally
            {
                MemoryBufferWriter.Return(writer);
            }
        }

        public static IEnumerable<object[]> TestDataNames
        {
            get
            {
                foreach (var k in TestData.Keys)
                {
                    yield return new object[] { k };
                }
            }
        }

        // TestData that requires object serialization
        public static IDictionary<string, MessagePackHubProtocolTestBase.ProtocolTestData> TestData => new[]
        {
            // Completion messages
            new ProtocolTestData(
                name: "CompletionWithNoHeadersAndNullResult",
                message: CompletionMessage.WithResult("xyz", payload: null),
                binary: "lQOAo3h5egPA"),
            new ProtocolTestData(
                name: "CompletionWithNoHeadersAndCustomObjectResult",
                message: CompletionMessage.WithResult("xyz", payload: new CustomObject()),
                binary: "lQOAo3h5egOGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgM="),
            new ProtocolTestData(
                name: "CompletionWithNoHeadersAndCustomObjectArrayResult",
                message: CompletionMessage.WithResult("xyz", payload: new[] { new CustomObject(), new CustomObject() }),
                binary: "lQOAo3h5egOShqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQIDhqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQID"),
            new ProtocolTestData(
                name: "CompletionWithHeadersAndCustomObjectArrayResult",
                message: AddHeaders(TestHeaders, CompletionMessage.WithResult("xyz", payload: new[] { new CustomObject(), new CustomObject() })),
                binary: "lQODo0Zvb6NCYXKyS2V5V2l0aApOZXcNCkxpbmVzq1N0aWxsIFdvcmtzsVZhbHVlV2l0aE5ld0xpbmVzsEFsc28KV29ya3MNCkZpbmWjeHl6A5KGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgM="),
            new ProtocolTestData(
                name: "CompletionWithNoHeadersAndEnumResult",
                message: CompletionMessage.WithResult("xyz", payload: TestEnum.One),
                binary: "lQOAo3h5egOjT25l"),

            // Invocation messages
            new ProtocolTestData(
                name: "InvocationWithNoHeadersNoIdAndSingleNullArg",
                message: new InvocationMessage("method", new object[] { null }),
                binary: "lgGAwKZtZXRob2SRwJA="),
            new ProtocolTestData(
                name: "InvocationWithNoHeadersNoIdIntAndEnumArgs",
                message: new InvocationMessage("method", new object[] { 42, TestEnum.One }),
                binary: "lgGAwKZtZXRob2SSKqNPbmWQ"),
            new ProtocolTestData(
                name: "InvocationWithNoHeadersNoIdAndCustomObjectArg",
                message: new InvocationMessage("method", new object[] { 42, "string", new CustomObject() }),
                binary: "lgGAwKZtZXRob2STKqZzdHJpbmeGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOQ"),
            new ProtocolTestData(
                name: "InvocationWithNoHeadersNoIdAndArrayOfCustomObjectArgs",
                message: new InvocationMessage("method", new object[] { new CustomObject(), new CustomObject() }),
                binary: "lgGAwKZtZXRob2SShqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQIDhqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQIDkA=="),
            new ProtocolTestData(
                name: "InvocationWithHeadersNoIdAndArrayOfCustomObjectArgs",
                message: AddHeaders(TestHeaders, new InvocationMessage("method", new object[] { new CustomObject(), new CustomObject() })),
                binary: "lgGDo0Zvb6NCYXKyS2V5V2l0aApOZXcNCkxpbmVzq1N0aWxsIFdvcmtzsVZhbHVlV2l0aE5ld0xpbmVzsEFsc28KV29ya3MNCkZpbmXApm1ldGhvZJKGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOQ"),

            // StreamItem Messages
            new ProtocolTestData(
                name: "StreamItemWithNoHeadersAndNullItem",
                message: new StreamItemMessage("xyz", item: null),
                binary: "lAKAo3h5esA="),
            new ProtocolTestData(
                name: "StreamItemWithNoHeadersAndEnumItem",
                message: new StreamItemMessage("xyz", item: TestEnum.One),
                binary: "lAKAo3h5eqNPbmU="),
            new ProtocolTestData(
                name: "StreamItemWithNoHeadersAndCustomObjectItem",
                message: new StreamItemMessage("xyz", item: new CustomObject()),
                binary: "lAKAo3h5eoaqU3RyaW5nUHJvcKhTaWduYWxSIapEb3VibGVQcm9wy0AZIftUQs8Sp0ludFByb3AqrERhdGVUaW1lUHJvcNb/WOwcgKhOdWxsUHJvcMCrQnl0ZUFyclByb3DEAwECAw=="),
            new ProtocolTestData(
                name: "StreamItemWithNoHeadersAndCustomObjectArrayItem",
                message: new StreamItemMessage("xyz", item: new[] { new CustomObject(), new CustomObject() }),
                binary: "lAKAo3h5epKGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgM="),
            new ProtocolTestData(
                name: "StreamItemWithHeadersAndCustomObjectArrayItem",
                message: AddHeaders(TestHeaders, new StreamItemMessage("xyz", item: new[] { new CustomObject(), new CustomObject() })),
                binary: "lAKDo0Zvb6NCYXKyS2V5V2l0aApOZXcNCkxpbmVzq1N0aWxsIFdvcmtzsVZhbHVlV2l0aE5ld0xpbmVzsEFsc28KV29ya3MNCkZpbmWjeHl6koaqU3RyaW5nUHJvcKhTaWduYWxSIapEb3VibGVQcm9wy0AZIftUQs8Sp0ludFByb3AqrERhdGVUaW1lUHJvcNb/WOwcgKhOdWxsUHJvcMCrQnl0ZUFyclByb3DEAwECA4aqU3RyaW5nUHJvcKhTaWduYWxSIapEb3VibGVQcm9wy0AZIftUQs8Sp0ludFByb3AqrERhdGVUaW1lUHJvcNb/WOwcgKhOdWxsUHJvcMCrQnl0ZUFyclByb3DEAwECAw=="),

            // StreamInvocation Messages
            new ProtocolTestData(
                name: "StreamInvocationWithNoHeadersAndEnumArg",
                message: new StreamInvocationMessage("xyz", "method", new object[] { TestEnum.One }),
                binary: "lgSAo3h5eqZtZXRob2SRo09uZZA="),
            new ProtocolTestData(
                name: "StreamInvocationWithNoHeadersAndNullArg",
                message: new StreamInvocationMessage("xyz", "method", new object[] { null }),
                binary: "lgSAo3h5eqZtZXRob2SRwJA="),
            new ProtocolTestData(
                name: "StreamInvocationWithNoHeadersAndIntStringAndCustomObjectArgs",
                message: new StreamInvocationMessage("xyz", "method", new object[] { 42, "string", new CustomObject() }),
                binary: "lgSAo3h5eqZtZXRob2STKqZzdHJpbmeGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOQ"),
            new ProtocolTestData(
                name: "StreamInvocationWithNoHeadersAndCustomObjectArrayArg",
                message: new StreamInvocationMessage("xyz", "method", new object[] { new CustomObject(), new CustomObject() }),
                binary: "lgSAo3h5eqZtZXRob2SShqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQIDhqpTdHJpbmdQcm9wqFNpZ25hbFIhqkRvdWJsZVByb3DLQBkh+1RCzxKnSW50UHJvcCqsRGF0ZVRpbWVQcm9w1v9Y7ByAqE51bGxQcm9wwKtCeXRlQXJyUHJvcMQDAQIDkA=="),
            new ProtocolTestData(
                name: "StreamInvocationWithHeadersAndCustomObjectArrayArg",
                message: AddHeaders(TestHeaders, new StreamInvocationMessage("xyz", "method", new object[] { new CustomObject(), new CustomObject() })),
                binary: "lgSDo0Zvb6NCYXKyS2V5V2l0aApOZXcNCkxpbmVzq1N0aWxsIFdvcmtzsVZhbHVlV2l0aE5ld0xpbmVzsEFsc28KV29ya3MNCkZpbmWjeHl6pm1ldGhvZJKGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOGqlN0cmluZ1Byb3CoU2lnbmFsUiGqRG91YmxlUHJvcMtAGSH7VELPEqdJbnRQcm9wKqxEYXRlVGltZVByb3DW/1jsHICoTnVsbFByb3DAq0J5dGVBcnJQcm9wxAMBAgOQ"),
        }.ToDictionary(t => t.Name);

        [Theory]
        [MemberData(nameof(TestDataNames))]
        public void ParseMessages(string testDataName)
        {
            var testData = TestData[testDataName];

            TestParseMessages(testData);
        }

        [Theory]
        [MemberData(nameof(TestDataNames))]
        public void WriteMessages(string testDataName)
        {
            var testData = TestData[testDataName];

            TestWriteMessages(testData);
        }

        public static IDictionary<string, MessageSizeTestData> MessageSizeData => new[]
        {
            new MessageSizeTestData("InvocationMessage_WithoutInvocationId", new InvocationMessage("Target", new object[] { 1 }), 15),
            new MessageSizeTestData("InvocationMessage_WithInvocationId", new InvocationMessage("1", "Target", new object[] { 1 }), 16),
            new MessageSizeTestData("InvocationMessage_WithInvocationIdAndStreamId", new InvocationMessage("1", "Target", new object[] { 1 }, new string[] { "2" }), 18),

            new MessageSizeTestData("CloseMessage_Empty", CloseMessage.Empty, 5),
            new MessageSizeTestData("CloseMessage_WithError", new CloseMessage("error"), 10),

            new MessageSizeTestData("StreamItemMessage_WithNullItem", new StreamItemMessage("1", null), 7),
            new MessageSizeTestData("StreamItemMessage_WithItem", new StreamItemMessage("1", 1), 7),

            new MessageSizeTestData("CompletionMessage_Empty", CompletionMessage.Empty("1"), 7),
            new MessageSizeTestData("CompletionMessage_WithResult", CompletionMessage.WithResult("1", 1), 8),
            new MessageSizeTestData("CompletionMessage_WithError", CompletionMessage.WithError("1", "error"), 13),

            new MessageSizeTestData("StreamInvocationMessage", new StreamInvocationMessage("1", "target", Array.Empty<object>()), 15),
            new MessageSizeTestData("StreamInvocationMessage_WithStreamId", new StreamInvocationMessage("1", "target", Array.Empty<object>(), new [] { "2" }), 17),

            new MessageSizeTestData("CancelInvocationMessage", new CancelInvocationMessage("1"), 6),

            new MessageSizeTestData("PingMessage", PingMessage.Instance, 3),
        }.ToDictionary(t => t.Name);

        public static IEnumerable<object[]> MessageSizeDataNames => MessageSizeData.Keys.Select(name => new object[] { name });

        [Theory]
        [MemberData(nameof(MessageSizeDataNames))]
        public void VerifyMessageSize(string testDataName)
        {
            var testData = MessageSizeData[testDataName];
            Assert.Equal(testData.Size, Write(testData.Message).Length);
        }

        public class MessageSizeTestData
        {
            public string Name { get; }
            public HubMessage Message { get; }
            public int Size { get; }

            public MessageSizeTestData(string name, HubMessage message, int size)
            {
                Name = name;
                Message = message;
                Size = size;
            }

            public override string ToString() => Name;
        }
    }
}

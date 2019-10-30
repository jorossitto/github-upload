﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Components
{
    /// <summary>
    /// Associates an event argument type with an event attribute name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class EventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Constructs an instance of <see cref="EventHandlerAttribute"/>.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="eventArgsType"></param>
        public EventHandlerAttribute(string attributeName, Type eventArgsType) : this(attributeName, eventArgsType, false, false)
        {
        }

        /// <summary>
        /// Constructs an instance of <see cref="EventHandlerAttribute"/>.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="eventArgsType"></param>
        /// <param name="enableStopPropagation"></param>
        /// <param name="enablePreventDefault"></param>
        public EventHandlerAttribute(string attributeName, Type eventArgsType, bool enableStopPropagation, bool enablePreventDefault)
        {
            if (attributeName == null)
            {
                throw new ArgumentNullException(nameof(attributeName));
            }

            if (eventArgsType == null)
            {
                throw new ArgumentNullException(nameof(eventArgsType));
            }

            AttributeName = attributeName;
            EventArgsType = eventArgsType;
            EnableStopPropagation = enableStopPropagation;
            EnablePreventDefault = enablePreventDefault;
        }

        /// <summary>
        /// Gets the attribute name.
        /// </summary>
        public string AttributeName { get; }

        /// <summary>
        /// Gets the event argument type.
        /// </summary>
        public Type EventArgsType { get; }

        /// <summary>
        /// Gets the event's ability to stop propagation.
        /// </summary>
        public bool EnableStopPropagation { get; }

        /// <summary>
        /// Gets the event's ability to prevent default event flow.
        /// </summary>
        public bool EnablePreventDefault { get; }
    }
}

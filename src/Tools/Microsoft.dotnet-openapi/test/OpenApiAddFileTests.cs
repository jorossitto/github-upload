// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.DotNet.OpenApi.Tests;
using Microsoft.Extensions.Internal;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.OpenApi.Add.Tests
{
    public class OpenApiAddFileTests : OpenApiTestBase
    {
        public OpenApiAddFileTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void OpenApi_Empty_ShowsHelp()
        {
            var app = GetApplication();
            var run = app.Execute(new string[] { });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            Assert.Contains("Usage: openapi ", _output.ToString());
        }

        [Fact]
        public void OpenApi_NoProjectExists()
        {
            var app = GetApplication();
            _tempDir.Create();
            var run = app.Execute(new string[] { "add", "file", "randomfile.json" });

            Assert.Contains("No project files were found in the current directory", _error.ToString());
            Assert.Equal(1, run);
        }

        [Fact]
        public void OpenApi_ExplicitProject_Missing()
        {
            var app = GetApplication();
            _tempDir.Create();
            var csproj = "fake.csproj";
            var run = app.Execute(new string[] { "add", "file", "--updateProject", csproj, "randomfile.json" });

            Assert.Contains($"The project '{Path.Combine(_tempDir.Root, csproj)}' does not exist.", _error.ToString());
            Assert.Equal(1, run);
        }

        [Fact]
        public void OpenApi_Add_Empty_ShowsHelp()
        {
            var app = GetApplication();
            var run = app.Execute(new string[] { "add" });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            Assert.Contains("Usage: openapi add", _output.ToString());
        }

        [Fact]
        public void OpenApi_Add_File_Empty_ShowsHelp()
        {
            var app = GetApplication();
            var run = app.Execute(new string[] { "add", "file", "--help" });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            Assert.Contains("Usage: openapi ", _output.ToString());
        }

        [Fact]
        public async Task OpenApi_Add_ReuseItemGroup()
        {
            var project = CreateBasicProject(withOpenApi: true);

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", project.NSwagJsonFile });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            var secondRun = app.Execute(new[] { "add", "url", FakeOpenApiUrl });
            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, secondRun);

            var csproj = new FileInfo(project.Project.Path);
            string content;
            using (var csprojStream = csproj.OpenRead())
            using (var reader = new StreamReader(csprojStream))
            {
                content = await reader.ReadToEndAsync();
                Assert.Contains("<PackageReference Include=\"NSwag.ApiDescription.Client\" Version=\"", content);
                Assert.Contains($"<OpenApiReference Include=\"{project.NSwagJsonFile}\"", content);
            }
            var projXml = new XmlDocument();
            projXml.Load(csproj.FullName);

            var openApiRefs = projXml.GetElementsByTagName(Commands.BaseCommand.OpenApiReference);
            Assert.Same(openApiRefs[0].ParentNode, openApiRefs[1].ParentNode);
        }

        [Fact]
        public void OpenApi_Add_File_EquivilentPaths()
        {
            var project = CreateBasicProject(withOpenApi: true);
            var nswagJsonFile = project.NSwagJsonFile;

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", nswagJsonFile });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            app = GetApplication();
            var absolute = Path.GetFullPath(nswagJsonFile, project.Project.Dir().Root);
            run = app.Execute(new[] { "add", "file", absolute });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            var csproj = new FileInfo(project.Project.Path);
            var projXml = new XmlDocument();
            projXml.Load(csproj.FullName);

            var openApiRefs = projXml.GetElementsByTagName(Commands.BaseCommand.OpenApiReference);
            Assert.Single(openApiRefs);
        }

        [Fact]
        public async Task OpenApi_Add_NSwagTypeScript()
        {
            var project = CreateBasicProject(withOpenApi: true);
            var nswagJsonFile = project.NSwagJsonFile;

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", nswagJsonFile, "--code-generator", "NSwagTypeScript" });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            // csproj contents
            var csproj = new FileInfo(project.Project.Path);
            using (var csprojStream = csproj.OpenRead())
            using (var reader = new StreamReader(csprojStream))
            {
                var content = await reader.ReadToEndAsync();
                Assert.Contains("<PackageReference Include=\"NSwag.ApiDescription.Client\" Version=\"", content);
                Assert.Contains($"<OpenApiReference Include=\"{nswagJsonFile}\" CodeGenerator=\"NSwagTypeScript\" />", content);
            }
        }

        [Fact]
        public async Task OpenApi_Add_FromJson()
        {
            var project = CreateBasicProject(withOpenApi: true);
            var nswagJsonFile = project.NSwagJsonFile;

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", nswagJsonFile });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            // csproj contents
            var csproj = new FileInfo(project.Project.Path);
            using (var csprojStream = csproj.OpenRead())
            using (var reader = new StreamReader(csprojStream))
            {
                var content = await reader.ReadToEndAsync();
                Assert.Contains("<PackageReference Include=\"NSwag.ApiDescription.Client\" Version=\"", content);
                Assert.Contains($"<OpenApiReference Include=\"{nswagJsonFile}\"", content);
            }
        }

        [Fact]
        public async Task OpenApi_Add_File_UseProjectOption()
        {
            var project = CreateBasicProject(withOpenApi: true);
            var nswagJsonFIle = project.NSwagJsonFile;

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", "--updateProject", project.Project.Path, nswagJsonFIle });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            // csproj contents
            var csproj = new FileInfo(project.Project.Path);
            using var csprojStream = csproj.OpenRead();
            using var reader = new StreamReader(csprojStream);
            var content = await reader.ReadToEndAsync();
            Assert.Contains("<PackageReference Include=\"NSwag.ApiDescription.Client\" Version=\"", content);
            Assert.Contains($"<OpenApiReference Include=\"{nswagJsonFIle}\"", content);
        }

        [Fact]
        public async Task OpenApi_Add_MultipleTimes_OnlyOneReference()
        {
            var project = CreateBasicProject(withOpenApi: true);
            var nswagJsonFile = project.NSwagJsonFile;

            var app = GetApplication();
            var run = app.Execute(new[] { "add", "file", nswagJsonFile });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            app = GetApplication();
            run = app.Execute(new[] { "add", "file", nswagJsonFile });

            Assert.True(string.IsNullOrEmpty(_error.ToString()), $"Threw error: {_error.ToString()}");
            Assert.Equal(0, run);

            // csproj contents
            var csproj = new FileInfo(project.Project.Path);
            using var csprojStream = csproj.OpenRead();
            using var reader = new StreamReader(csprojStream);
            var content = await reader.ReadToEndAsync();
            var escapedPkgRef = Regex.Escape("<PackageReference Include=\"NSwag.ApiDescription.Client\" Version=\"");
            Assert.Single(Regex.Matches(content, escapedPkgRef));
            var escapedApiRef = Regex.Escape($"<OpenApiReference Include=\"{nswagJsonFile}\"");
            Assert.Single(Regex.Matches(content, escapedApiRef));
        }
    }
}

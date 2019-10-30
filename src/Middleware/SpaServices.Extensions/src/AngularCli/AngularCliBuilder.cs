// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.NodeServices.Npm;
using Microsoft.AspNetCore.NodeServices.Util;
using Microsoft.AspNetCore.SpaServices.Prerendering;
using Microsoft.AspNetCore.SpaServices.Util;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.SpaServices.AngularCli
{
    /// <summary>
    /// Provides an implementation of <see cref="ISpaPrerendererBuilder"/> that can build
    /// an Angular application by invoking the Angular CLI.
    /// </summary>
    [Obsolete("Prerendering is no longer supported out of box")]
    public class AngularCliBuilder : ISpaPrerendererBuilder
    {
        private static TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(5); // This is a development-time only feature, so a very long timeout is fine

        private readonly string _scriptName;

        /// <summary>
        /// Constructs an instance of <see cref="AngularCliBuilder"/>.
        /// </summary>
        /// <param name="npmScript">The name of the script in your package.json file that builds the server-side bundle for your Angular application.</param>
        public AngularCliBuilder(string npmScript)
        {
            if (string.IsNullOrEmpty(npmScript))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(npmScript));
            }

            _scriptName = npmScript;
        }

        /// <inheritdoc />
        public async Task Build(ISpaBuilder spaBuilder)
        {
            var pkgManagerCommand = spaBuilder.Options.PackageManagerCommand;
            var sourcePath = spaBuilder.Options.SourcePath;
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(AngularCliBuilder)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            var logger = LoggerFinder.GetOrCreateLogger(
                spaBuilder.ApplicationBuilder,
                nameof(AngularCliBuilder));
            var scriptRunner = new NodeScriptRunner(
                sourcePath,
                _scriptName,
                "--watch",
                null,
                pkgManagerCommand);
            scriptRunner.AttachToLogger(logger);

            using (var stdOutReader = new EventedStreamStringReader(scriptRunner.StdOut))
            using (var stdErrReader = new EventedStreamStringReader(scriptRunner.StdErr))
            {
                try
                {
                    await scriptRunner.StdOut.WaitForMatch(
                        new Regex("Date", RegexOptions.None, RegexMatchTimeout));
                }
                catch (EndOfStreamException ex)
                {
                    throw new InvalidOperationException(
                        $"The {pkgManagerCommand} script '{_scriptName}' exited without indicating success.\n" +
                        $"Output was: {stdOutReader.ReadAsString()}\n" +
                        $"Error output was: {stdErrReader.ReadAsString()}", ex);
                }
                catch (OperationCanceledException ex)
                {
                    throw new InvalidOperationException(
                        $"The {pkgManagerCommand} script '{_scriptName}' timed out without indicating success. " +
                        $"Output was: {stdOutReader.ReadAsString()}\n" +
                        $"Error output was: {stdErrReader.ReadAsString()}", ex);
                }
            }
        }
    }
}

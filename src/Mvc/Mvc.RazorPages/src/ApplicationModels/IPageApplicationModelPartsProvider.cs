﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// Provides parts that are used to construct a <see cref="PageApplicationModel" /> instance
    /// </summary>
    public interface IPageApplicationModelPartsProvider
    {
        /// <summary>
        /// Creates a <see cref="PageHandlerModel"/> for the specified <paramref name="method"/>.s
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo"/>.</param>
        /// <returns>The <see cref="PageHandlerModel"/>.</returns>
        PageHandlerModel CreateHandlerModel(MethodInfo method);

        /// <summary>
        /// Creates a <see cref="PageParameterModel"/> for the specified <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="ParameterInfo"/>.</param>
        /// <returns>The <see cref="PageParameterModel"/>.</returns>
        PageParameterModel CreateParameterModel(ParameterInfo parameter);

        /// <summary>
        /// Creates a <see cref="PagePropertyModel"/> for the <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/>.</param>
        /// <returns>The <see cref="PagePropertyModel"/>.</returns>
        PagePropertyModel CreatePropertyModel(PropertyInfo property);

        /// <summary>
        /// Determines if the specified <paramref name="methodInfo"/> is a handler.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/>.</param>
        /// <returns><c>true</c> if the <paramref name="methodInfo"/> is a handler. Otherwise <c>false</c>.</returns>
        /// <remarks>
        /// Override this method to provide custom logic to determine which methods are considered handlers.
        /// </remarks>
        bool IsHandler(MethodInfo methodInfo);
    }
}

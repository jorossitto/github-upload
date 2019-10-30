// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Components.RenderTree
{
    /// <summary>
    /// Types in the Microsoft.AspNetCore.Components.RenderTree are not recommended for use outside
    /// of the Blazor framework. These types will change in a future release.
    /// </summary>
    // 
    // Information supplied with an event notification that can be used to update an existing
    // render tree to match the latest UI state when a form field has mutated. To determine
    // which field has been mutated, the renderer matches it based on the event handler ID.
    public class EventFieldInfo
    {
        /// <summary>
        /// Identifies the component whose render tree contains the affected form field.
        /// </summary>
        public int ComponentId { get; set; }

        /// <summary>
        /// Specifies the form field's new value.
        /// </summary>
        public object FieldValue { get; set; }
    }
}

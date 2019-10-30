// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components.RenderTree;

namespace Microsoft.AspNetCore.Components.Rendering
{
    internal class RenderTreeUpdater
    {
        public static void UpdateToMatchClientState(RenderTreeBuilder renderTreeBuilder, ulong eventHandlerId, object newFieldValue)
        {
            // We only allow the client to supply string or bool currently, since those are the only kinds of
            // values we output on attributes that go to the client
            if (!(newFieldValue is string || newFieldValue is bool))
            {
                return;
            }

            // Find the element that contains the event handler
            var frames = renderTreeBuilder.GetFrames();
            var framesArray = frames.Array;
            var framesLength = frames.Count;
            var closestElementFrameIndex = -1;
            for (var frameIndex = 0; frameIndex < framesLength; frameIndex++)
            {
                ref var frame = ref framesArray[frameIndex];
                switch (frame.FrameType)
                {
                    case RenderTreeFrameType.Element:
                        closestElementFrameIndex = frameIndex;
                        break;
                    case RenderTreeFrameType.Attribute:
                        if (frame.AttributeEventHandlerId == eventHandlerId)
                        {
                            if (!string.IsNullOrEmpty(frame.AttributeEventUpdatesAttributeName))
                            {
                                UpdateFrameToMatchClientState(
                                    renderTreeBuilder,
                                    framesArray,
                                    closestElementFrameIndex,
                                    frame.AttributeEventUpdatesAttributeName,
                                    newFieldValue);
                            }

                            // Whether or not we did update the frame, that was the one that matches
                            // the event handler ID, so no need to look any further
                            return;
                        }
                        break;
                }
            }
        }

        private static void UpdateFrameToMatchClientState(RenderTreeBuilder renderTreeBuilder, RenderTreeFrame[] framesArray, int elementFrameIndex, string attributeName, object attributeValue)
        {
            // Find the attribute frame
            ref var elementFrame = ref framesArray[elementFrameIndex];
            var elementSubtreeEndIndexExcl = elementFrameIndex + elementFrame.ElementSubtreeLength;
            for (var attributeFrameIndex = elementFrameIndex + 1; attributeFrameIndex < elementSubtreeEndIndexExcl; attributeFrameIndex++)
            {
                ref var attributeFrame = ref framesArray[attributeFrameIndex];
                if (attributeFrame.FrameType != RenderTreeFrameType.Attribute)
                {
                    // We're now looking at the descendants not attributes, so the search is over
                    break;
                }

                if (attributeFrame.AttributeName == attributeName)
                {
                    // Found an existing attribute we can update
                    attributeFrame = attributeFrame.WithAttributeValue(attributeValue);
                    return;
                }
            }

            // If we get here, we didn't find the desired attribute, so we have to insert a new frame for it
            var insertAtIndex = elementFrameIndex + 1;
            renderTreeBuilder.InsertAttributeExpensive(insertAtIndex, RenderTreeDiffBuilder.SystemAddedAttributeSequenceNumber, attributeName, attributeValue);
            framesArray = renderTreeBuilder.GetFrames().Array; // Refresh in case it mutated due to the expansion

            // Update subtree length for this and all ancestor containers
            // Ancestors can only be regions or other elements, since components can't "contain" elements inline
            // We only have to walk backwards, since later entries in the frames array can't contain an earlier one
            for (var otherFrameIndex = elementFrameIndex; otherFrameIndex >= 0; otherFrameIndex--)
            {
                ref var otherFrame = ref framesArray[otherFrameIndex];
                switch (otherFrame.FrameType)
                {
                    case RenderTreeFrameType.Element:
                        {
                            var otherFrameSubtreeLength = otherFrame.ElementSubtreeLength;
                            var otherFrameEndIndexExcl = otherFrameIndex + otherFrameSubtreeLength;
                            if (otherFrameEndIndexExcl > elementFrameIndex) // i.e., contains the element we're inserting into
                            {
                                otherFrame = otherFrame.WithElementSubtreeLength(otherFrameSubtreeLength + 1);
                            }
                            break;
                        }
                    case RenderTreeFrameType.Region:
                        {
                            var otherFrameSubtreeLength = otherFrame.RegionSubtreeLength;
                            var otherFrameEndIndexExcl = otherFrameIndex + otherFrameSubtreeLength;
                            if (otherFrameEndIndexExcl > elementFrameIndex) // i.e., contains the element we're inserting into
                            {
                                otherFrame = otherFrame.WithRegionSubtreeLength(otherFrameSubtreeLength + 1);
                            }
                            break;
                        }
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics.Contracts;

namespace AppCore.Data.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailAddress { get; set; }
        public string Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Contract.Requires(output != null);
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + EmailAddress);
            output.Content.SetContent(Content);
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CofeeShop.TagHelpers
{
    [HtmlTargetElement("button-submit")]
    public class ButtonTagHelper : TagHelper
    {
        public string Text { get; set; } = "submit";
        public string Class { get; set; } = "btn-primary";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";

            output.Attributes.SetAttribute("type", Text);
            output.Attributes.SetAttribute("class", $"btn btn-{Class}");
            output.Content.SetContent(Text);
        }
    }
}

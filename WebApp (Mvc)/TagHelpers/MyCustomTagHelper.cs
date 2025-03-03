using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CofeeShop.TagHelpers
{
    [HtmlTargetElement("sujal")]
    public class MyCustomTagHelper : TagHelper
    {
        public string path { get; set; }
        public string altText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;

            output.Attributes.SetAttribute("src", path);
            output.Attributes.SetAttribute("alt", altText);
        }
    }
}

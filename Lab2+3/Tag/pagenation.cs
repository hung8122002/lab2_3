using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;

namespace Lab2_3.Tag
{
    [HtmlTargetElement("pagenation")]
    public class pagenation : TagHelper
    {
        public int totalPage { get; set; }

        public int currentPage { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "inline-flex -space-x-px");

            StringBuilder content = new StringBuilder();
            content.Append($@"<li>
      <button type=""submit"" value=""1"" name=""pageIndex"" class=""px-3 py-2 ml-0 leading-tight text-gray-500 bg-white border border-gray-300 rounded-l-lg hover:!bg-gray-200 hover:text-gray-700"">Home</button>
    </li>");
            content.Append($@"<li>
      <button type=""submit"" {(currentPage == 1 ? "hidden" : "")} value=""{currentPage-1}"" name=""pageIndex"" class=""px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:!bg-gray-200 hover:text-gray-700"">Previous</button>
    </li>");
            for (int i = 1; i <= totalPage; i++)
            {
                content.Append($@"<li>
      <button type=""submit"" value=""{i}"" name=""pageIndex"" class=""px-3 py-2 leading-tight text-gray-500 bg-white {(i == currentPage ? "!bg-gray-300" : "")} border border-gray-300 hover:!bg-gray-200 hover:text-gray-700"">{i}</button>
    </li>");
            }
            content.Append($@"<li>
      <button type=""submit"" {(currentPage == totalPage ? "hidden" : "")} value=""{currentPage + 1}"" name=""pageIndex"" class=""px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:!bg-gray-200 hover:text-gray-700"">Next</button>
    </li>");
            content.Append($@"<li>
      <button type=""submit"" value=""{totalPage}"" name=""pageIndex"" class=""px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 rounded-r-lg hover:!bg-gray-200 hover:text-gray-700"">End</button>
    </li>");
            output.Content.SetHtmlContent(content.ToString());
        }
    }
}
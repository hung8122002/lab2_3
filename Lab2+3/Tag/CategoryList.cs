using Lab2_3.Bussiness.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;

namespace Lab2_3.Tag
{
    [HtmlTargetElement("categorylist")]
    public class CategoryList : TagHelper
    {
        public List<CategoryDTO> List { get; set; }
        public int? indexChecked { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "space-y-1 border-t border-gray-200 p-4");

            StringBuilder content = new StringBuilder();
            foreach (var item in List)
            {
                content.Append($@"<li>
                        <label
                          for=""{item.CategoryId}""
                          class=""inline-flex cursor-pointer items-center gap-2""
                        >
                          <input
                            {(item.CategoryId == indexChecked ? "checked" : "")}
                            asp-for=indexChecked
                            type=""radio""
                            name=""cateId""
                            value=""{item.CategoryId}""
                            id=""{item.CategoryId}""
                            class=""h-5 w-5 cursor-pointer rounded border-gray-300""
                          />

                          <span class=""text-sm font-medium text-gray-700"">
                            {item.CategoryName}
                          </span>
                        </label>
                      </li>");
            }
            output.Content.SetHtmlContent(content.ToString());
        }
    }
}

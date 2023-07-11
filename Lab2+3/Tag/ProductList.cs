using Lab2_3.Bussiness.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Lab2_3.Tag
{
    [HtmlTargetElement("ProductList")]
    public class ProductList : TagHelper
    {
        public List<ProductDTO> List { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "mt-4 grid gap-4 sm:grid-cols-2 lg:grid-cols-4");

            StringBuilder content = new StringBuilder();
            foreach (var item in List)
            {
                content.Append($@" <li>
            <a href=""Detail/{item.ProductId}"" class=""group relative block h-64 sm:h-80 lg:h-96"">
              <span
                class=""absolute inset-0 border-2 border-dashed border-black""
              ></span>

              <div
                class=""relative flex justify-center h-full transform items-end border-2 border-black bg-white transition-transform group-hover:-translate-x-2 group-hover:-translate-y-2""
              >
                <div
                  class=""!pt-0 transition-opacity group-hover:absolute group-hover:opacity-0""
                >
                  <img
                    src=""{item.ImageLink}""
                    alt=""""
                    class=""h-[320px] mx-auto""
                  />

                  <h2 style=""
                      display: -webkit-box;
                      -webkit-line-clamp: 1;
                      -webkit-box-orient: vertical;
                    "" class=""my-3 pr-3 overflow-ellipsis overflow-hidden text-center text-xl font-medium sm:text-xl"">
                    {item.ProductName}
                  </h2>
                </div>

                <div
                  class=""absolute p-3 opacity-0 transition-opacity group-hover:!opacity-100 sm:p-6 lg:p-8""
                >
                  <img
                    src=""{item.ImageLink}""
                    alt=""""
                    class=""h-[180px] mx-auto""
                  />
                  <h3 style=""
                      display: -webkit-box;
                      -webkit-line-clamp: 1;
                      -webkit-box-orient: vertical;
                    "" class=""mt-2 overflow-ellipsis overflow-hidden text-xl text-black sm:text-xl"">
                    {item.ProductName}
                  </h3>
                  <p class=""text-black mt-2"">${String.Format("{0:0.00}",  item.UnitPrice)}</p>
                  <p
                    style=""
                      display: -webkit-box;
                      -webkit-line-clamp: 2;
                      -webkit-box-orient: vertical;
                    ""
                    class=""mt-2 text-black overflow-ellipsis overflow-hidden text-sm sm:text-base""
                  >
                    Lorem, ipsum dolor sit amet consectetur adipisicing elit.
                    Delectus laudantium optio sequi magnam dolorem perferendis,
                    sunt minima ad, nam assumenda iste. Officiis qui illo
                    assumenda animi nemo. Autem, rerum fugiat!
                  </p>
                  <div
                    class=""mt-2 inline-flex items-center gap-2 text-indigo-600 sm:mt-12 lg:mt-16""
                  >
                    <p class=""font-medium sm:text-lg"">View Detail</p>

                    <svg
                      xmlns=""http://www.w3.org/2000/svg""
                      class=""h-6 w-6 transition-all group-hover:ms-3 rtl:rotate-180""
                      fill=""none""
                      viewBox=""0 0 24 24""
                      stroke=""currentColor""
                    >
                      <path
                        stroke-linecap=""round""
                        stroke-linejoin=""round""
                        stroke-width=""2""
                        d=""M17 8l4 4m0 0l-4 4m4-4H3""
                      />
                    </svg>
                  </div>
                </div>
              </div>
            </a>
          </li>");
            }
            output.Content.SetHtmlContent(content.ToString());
        }
    }
}

#pragma checksum "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6328b3678b5ba62aecee3cd224ecdbdb0f0b4c87"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FileModel_Index), @"mvc.1.0.view", @"/Views/FileModel/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\_ViewImports.cshtml"
using ClothesShopDiplom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\_ViewImports.cshtml"
using ClothesShopDiplom.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6328b3678b5ba62aecee3cd224ecdbdb0f0b4c87", @"/Views/FileModel/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8fa45630cc79d0aedc65e994254b19d9fc6b4fac", @"/Views/_ViewImports.cshtml")]
    public class Views_FileModel_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ClothesShopDiplom.Models.FileModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("h5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "filemodel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
  
    ViewData["Title"] = "Main Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n\r\n<div class=\"album py-5 bg-body-tertiary\">\r\n    <div >\r\n        <div  class=\"row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3\">\r\n");
#nullable restore
#line 15 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""col-4 mb-5"">
                    <div class=""card shadow-sm"" style=""width:310px; height:100%"" >
                        
                            <img  class=""bd-placeholder-img card-img-top""   style=""width:308.5px;min-height:380px""");
            BeginWriteAttribute("src", "  src=\"", 564, "\"", 594, 1);
#nullable restore
#line 20 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
WriteAttributeValue("", 571, Url.Content(item.Path), 571, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n                 \r\n                        <div class=\"card-body \"  >\r\n                            \r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6328b3678b5ba62aecee3cd224ecdbdb0f0b4c876560", async() => {
#nullable restore
#line 24 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
                                                                                       Write(item.Product.ProductName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 24 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
                                                           WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                <p class=\"card-text\">");
#nullable restore
#line 25 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
                                                Write(item.Product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <div class=\"d-flex justify-content-between align-items-end\">\r\n\r\n                                    <div class=\"btn-group\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6328b3678b5ba62aecee3cd224ecdbdb0f0b4c879693", async() => {
                WriteLiteral(@"
                                            <input name=""choosedSize"" type=""submit"" style=""width:30px; height:25px; font-size: 10px"" class=""btn btn-sm btn-outline-secondary rounded-circle"" value=""XS"" />
                                            <input name=""choosedSize"" type=""submit"" style=""width:30px; height:25px;font-size: 10px"" class=""btn btn-sm btn-outline-secondary rounded-circle"" value=""S"" />
                                            <input name=""choosedSize"" type=""submit"" style=""width:30px; height:25px;font-size: 10px"" class=""btn btn-sm btn-outline-secondary rounded-circle"" value=""M"" />
                                            <input name=""choosedSize"" type=""submit"" style=""width:30px; height:25px;font-size: 10px"" class=""btn btn-sm btn-outline-secondary rounded-circle"" value=""L"" />
                                            <input name=""choosedSize"" type=""submit"" style=""width:30px; height:25px;font-size: 10px"" class=""btn btn-sm btn-outline-secondary rounded-circle"" value=""XL"" />
         ");
                WriteLiteral("                               ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
                                                                                                                WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                    <small class=\"text-body-secondary ml-3\">");
#nullable restore
#line 37 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
                                                                       Write(item.Product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("₽</small>\r\n                                </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 42 "C:\Users\Дмитрий\Desktop\DiplomProductFinal123\ClothesShopDiplom\ClothesShopDiplom\Views\FileModel\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ClothesShopDiplom.Models.FileModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

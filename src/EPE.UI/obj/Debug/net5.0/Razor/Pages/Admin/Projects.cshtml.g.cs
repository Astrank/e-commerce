#pragma checksum "C:\Users\marti\projects\epe\src\EPE.UI\Pages\Admin\Projects.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d79d81a9db6cae0a70c6a2ddad25086f59c3d5bb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPE.UI.Pages.Admin.Pages_Admin_Projects), @"mvc.1.0.razor-page", @"/Pages/Admin/Projects.cshtml")]
namespace EPE.UI.Pages.Admin
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
#line 1 "C:\Users\marti\projects\epe\src\EPE.UI\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d79d81a9db6cae0a70c6a2ddad25086f59c3d5bb", @"/Pages/Admin/Projects.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c4232fd30d10e1c75c96bd67b3cd094c04e2b4a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Admin_Projects : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/admin/projects.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"admin-list\" v-if=\"showList\">\r\n    <button class=\"add-product-btn button is-link\" ");
            WriteLiteral(@"@click=""newProject()"">Add project</button>
    <table class=""table products-table"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Tags</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for=""(project, index) in projects"" ");
            WriteLiteral(@"@click=""getProject(project.id, index)"">
                <td>{{project.id}}</td>
                <td>{{project.title}}</td>
                <td>{{project.tags}}</td>
            </tr>
        </tbody>
    </table>
</div>

<div class=""admin-element""");
            BeginWriteAttribute("v-else", " v-else=\"", 728, "\"", 737, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n    <button class=\"button back-btn\" ");
            WriteLiteral("@click=\"toggleList()\">\r\n        <i class=\"fas fa-long-arrow-alt-left\"></i>\r\n    </button>\r\n    <button class=\"button is-danger delete-product-btn\" v-if=\"projectModel.id\"\r\n            ");
            WriteLiteral(@"@click=""deleteProject(projectModel.id, projectModel.image)"">
        Delete project
    </button>
    <div class=""product-form"">
        <div class=""admin-data-form"">
            <div class=""field"">
                <label class=""label"">Title</label>
                <div class=""control"">
                    <input class=""input"" v-model=""projectModel.title"">
                </div>
            </div>
            <div class=""field"">
                <label class=""label"">Description</label>
                <div class=""control"">
                    <input class=""input"" v-model=""projectModel.description"">
                </div>
            </div>
            <div class=""field"">
                <label class=""label"">Tags</label>
                <div class=""control"">
                    <input class=""input"" v-model=""projectModel.tags"">
                </div>
            </div>
            <div class=""field"">
                <label class=""label"">Primary Image</label>
                <div class=""c");
            WriteLiteral("ontrol\">\r\n                    <input type=\"file\" class=\"input\" ");
            WriteLiteral(@"@change=""getPrimaryImage($event)"">
                </div>
            </div>
            <div class=""field"">
                <label class=""label"">Images</label>
                <div class=""control"">
                    <input type=""file"" class=""input"" multiple ");
            WriteLiteral("@change=\"getImageFiles($event)\">\r\n                </div>\r\n            </div>\r\n\r\n            <button ");
            WriteLiteral("@click=\"createProject()\" v-if=\"!projectModel.id\" class=\"button is-link\">Create product</button>\r\n            <button ");
            WriteLiteral("@click=\"updateProject()\" v-else=\"\" class=\"button is-link\">Update product</button>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d79d81a9db6cae0a70c6a2ddad25086f59c3d5bb6526", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPE.UI.Pages.Admin.ProjectsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EPE.UI.Pages.Admin.ProjectsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EPE.UI.Pages.Admin.ProjectsModel>)PageContext?.ViewData;
        public EPE.UI.Pages.Admin.ProjectsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\marti\projects\epe\EPE.UI\Pages\Projects\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "38f97e2a57a0fb337c69e3a5068a6264f9639f90"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPE.UI.Pages.Projects.Pages_Projects_Edit), @"mvc.1.0.razor-page", @"/Pages/Projects/Edit.cshtml")]
namespace EPE.UI.Pages.Projects
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
#line 1 "C:\Users\marti\projects\epe\EPE.UI\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38f97e2a57a0fb337c69e3a5068a6264f9639f90", @"/Pages/Projects/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c4232fd30d10e1c75c96bd67b3cd094c04e2b4a", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Projects_Edit : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\marti\projects\epe\EPE.UI\Pages\Projects\Edit.cshtml"
    /*
    ViewData["Title"] = "Editar proyecto | ";

<div class="section-header">
    <h1 class="section-title">EDITAR PROYECTO</h1>
</div>
<form method="POST" enctype="multipart/form-data" class="project-form">
    <input type="hidden" asp-for="Project.Id" value="@Model.ProjectToUpdate.Id">
    <input type="hidden" asp-for="Project.ImagePath" value="@Model.ProjectToUpdate.ImagePath">
    <div class="form-group">
        <label asp-for="Project.Title"></label>
        <input type="text" asp-for="Project.Title" class="form-control" value="@Model.ProjectToUpdate.Title">
    </div>
    <div class="form-group">
        <label asp-for="Project.Description"></label>
        <input type="text" asp-for="Project.Description" class="form-control" value="@Model.ProjectToUpdate.Description">
    </div>
    <div class="form-group">
        <label asp-for="Project.Tags"></label>
        <select asp-for="Project.Tags" class="form-select" aria-label="Select tag">
            <option value="null" disabled selected hidden></option>
            <option value="Interiores">Interiores</option>
            <option value="Arquitectura">Arquitectura</option>
        </select>
    </div>
    <div class="form-group">
        <label asp-for="Project.Image"></label>
        <input asp-for="Project.Image" class="form-control">
    </div>
    <input type="submit" value="Save project" class="btn-epe" style="margin: 15px auto; display: block;">
</form>

    */

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Projects.EditModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Projects.EditModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Projects.EditModel>)PageContext?.ViewData;
        public Projects.EditModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

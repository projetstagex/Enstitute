#pragma checksum "/home/dev/Enstitute/Views/Admin/_EditModuleAffectation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ffe079e283c57650a8594a7aafbbc8f54cbbf2a8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin__EditModuleAffectation), @"mvc.1.0.view", @"/Views/Admin/_EditModuleAffectation.cshtml")]
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
#line 1 "/home/dev/Enstitute/Views/_ViewImports.cshtml"
using Enstitute;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/dev/Enstitute/Views/_ViewImports.cshtml"
using Enstitute.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ffe079e283c57650a8594a7aafbbc8f54cbbf2a8", @"/Views/Admin/_EditModuleAffectation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin__EditModuleAffectation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <!-- Modal -->
    <div id=""EditMA"" class=""modal fade"" role=""dialog"">
        <div class=""modal-dialog"">

            <!-- Modal content-->
            <div class=""modal-content"">
                <!--Modal Header-->
                <div class=""modal-header"">
                    <button type=""button"" class=""close"" data-dismiss=""modal""></button>
                </div>
                <!---------------->

                <!-- Modal Body -->
                <div class=""modal-body"">
                    <h4 class=""modal-title"">Edit Module Affectation</h4>

                    <div class=""form-group"">
                        <label class=""form-label"">Formateur :</label>
                        <select name=""beast"" id=""ma_former_edit"" class=""form-control custom-select""></select>
                    </div>

                    <div class=""form-group"">
                        <label class=""form-label"">Module : </label>
                        <select name=""beast"" id=""ma_module_edit"" class=""form-control custom-selec");
            WriteLiteral(@"t""></select>
                    </div>

                    <div class=""form-group"">
                        <label class=""form-label"">Groupe : </label>
                        <select name=""beast"" id=""ma_group_edit"" class=""form-control custom-select""></select>
                    </div>

                    <button type=""button"" class=""btn btn-primary btn-block btn-edit"" id=""ma"">Save</button>

                </div>
                <!---------------->

            </div>
            <!---------------->

        </div>
    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

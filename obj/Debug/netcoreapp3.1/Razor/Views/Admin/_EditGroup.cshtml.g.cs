#pragma checksum "/home/dev/Enstitute/Views/Admin/_EditGroup.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a048eb4f19cf907bed88314119cd80dedb4d30b6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin__EditGroup), @"mvc.1.0.view", @"/Views/Admin/_EditGroup.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a048eb4f19cf907bed88314119cd80dedb4d30b6", @"/Views/Admin/_EditGroup.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin__EditGroup : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <!-- Modal -->
    <div id=""EditGroup"" class=""modal fade"" role=""dialog"">
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
                    <h4 class=""modal-title"">Contact Us</h4>

                    <div class=""form-group"">
                        <input type=""hidden"" class=""form-control"" id=""group_id_edit"">
                    </div>
                    <div class=""form-group"">
                        <label class=""form-label"">Label :</label>
                        <input type=""text"" class=""form-control"" id=""group_label_edit"">
                    </div>
                    <div class=""form-group"">
                        <label cl");
            WriteLiteral(@"ass=""form-label"">Code :</label>
                        <input type=""text"" class=""form-control"" id=""group_code_edit"">
                    </div>
                    <button type=""button"" class=""btn btn-primary btn-block btn-edit"" id=""group"">Save</button>

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

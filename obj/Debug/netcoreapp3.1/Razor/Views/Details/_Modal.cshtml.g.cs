#pragma checksum "/home/dev/Enstitute/Views/Details/_Modal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c342424e0db3a4018ad05c01da8b03fcbd62ad94"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Details__Modal), @"mvc.1.0.view", @"/Views/Details/_Modal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c342424e0db3a4018ad05c01da8b03fcbd62ad94", @"/Views/Details/_Modal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Details__Modal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Modal -->
<div class=""modal"" id=""myModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""Edit Absences"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <!-- model header -->
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLongTitle"">
                    Modifier Absence :
                </h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""></button> 
            </div>
            <!-- model body -->
            <div class=""modal-body"">
                <div class=""card"">
                    <center><label class=""form-label validator"" style=""color:red; font-size:17px;"" ></label></center>
                </div>
                <div class=""card"">
                    <div class=""table-responsive"">
                        <table class=""table table-hover table-outline table-vcenter text-nowrap card-table"">
                            ");
            WriteLiteral(@"<thead>
                                <tr>
                                <th class=""text-center w-1"">#</th>
                                <th class=""text-center"">Seance</th>
                                <th class=""text-center"">Date</th>
                                <th class=""text-center"">Formateur</th>
                                <th class=""text-center"">Etat</th>
                                </tr>
                            </thead>
                            <tbody class=""table-body"" >                            
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
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

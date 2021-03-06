#pragma checksum "/home/dev/Enstitute/Views/Sessions/__AddSessionContextDB.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cac7de9a5aa1e3571ecbd49324c6f910f78d55c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sessions___AddSessionContextDB), @"mvc.1.0.view", @"/Views/Sessions/__AddSessionContextDB.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cac7de9a5aa1e3571ecbd49324c6f910f78d55c2", @"/Views/Sessions/__AddSessionContextDB.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Sessions___AddSessionContextDB : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>

    $(document).ready(function(){
        $( '.add-save-changes-context-session' ).click(function(){

            var days = ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'];

            var date = new Date ($( ""#date_input_context"" ).val() );
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            
            var day_label_input = days[date.getDay()];
            var day_label_true = localStorage.getItem(""dayLabel"");
            if ( day_label_true != day_label_input ){
                $( "".validator-add-context"" ).html( ""<strong> Date entrée </strong> n'est pas convenable avec la date de seance ! "" );
            }
            else {
                var ERROR500 = false; 
                $.each($("".list-contexts-added-units""), function(k, v)
                {
                    var context_id = v['id'];

                    $.ajax({
                        type: ""POST"",
                   ");
            WriteLiteral("     url: \"");
#nullable restore
#line 26 "/home/dev/Enstitute/Views/Sessions/__AddSessionContextDB.cshtml"
                         Write(Url.Action("AddSessionContext"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                        dataType: ""text"",
                        data: { 
                            contextID :  context_id,
                            sessionID : parseInt(localStorage.getItem(""sessionID"")), 
                            year : year,
                            month: month, 
                            day : day,
                        },
                        error : function(xhr, status, error){ ERROR500 = true;}
                    });
                });
                $(""#modalContext"").modal(""toggle"");

                if (ERROR500){
                    ShowAlert( ""danger_alert"", ""500 Internal server error !"" );
                }
                else {
                    ShowAlert( ""success_alert"", ""l'operation est effectué avec succés"" );
                    localStorage.removeItem(""dayLabel"");
                    localStorage.removeItem(""sessionID"");
                }
            }
            




        });
    });

</script>");
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

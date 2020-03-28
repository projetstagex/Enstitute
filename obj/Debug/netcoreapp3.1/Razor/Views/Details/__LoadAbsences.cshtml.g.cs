#pragma checksum "/home/dev/Enstitute/Views/Details/__LoadAbsences.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b99944a83982f630041e2195d9b042fb356a08cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Details___LoadAbsences), @"mvc.1.0.view", @"/Views/Details/__LoadAbsences.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b99944a83982f630041e2195d9b042fb356a08cc", @"/Views/Details/__LoadAbsences.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Details___LoadAbsences : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<script>\n   \n    $(document).ready(function(){\n        $(\".modal-absences\").click(function(){\n            var module_id = $(this).attr(\"id\");\n            $(\".table-body\").empty();\n            $.ajax({\n                type: \"POST\",\n                url: \"");
#nullable restore
#line 10 "/home/dev/Enstitute/Views/Details/__LoadAbsences.cshtml"
                 Write(Url.Action("LoadAbsences"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                dataType: ""text"",
                data : {
                    module_id : module_id,
                    student_id : getUrlParameter(""SID"")
                },
                success: function (msg) {
                    data = JSON.parse(msg);
                    $.each( data, function( key, value ){

                        var id = key + 1; 
                        var session = value[ ""session"" ];

                        var absence_id = value[ ""id"" ];

                        var from_hour   = session[ ""fromHour"" ];
                        var from_minute = session[ ""fromMinute"" ];
                        var to_hour     = session[ ""toHour"" ];
                        var to_minute   = session[ ""toMinute"" ];
                        var ft_string   = from_hour + "" : "" + from_minute + "" => "" + to_hour + "" : "" + to_minute;

                        var date        = new Date( value[ ""date"" ] ) ;
                        var year        = date.getFullYear();
                        var mon");
            WriteLiteral(@"th       = date.getMonth();
                        var day         = date.getDay();

                        var status      = value[ ""status"" ];
                        var former      = session[ ""schedule"" ][ ""former"" ][ ""username"" ];

                        if ( status ){
                            $( "".table-body"" ).append(""\
                                <tr>\
                                    <td>"" + id + ""</td>\
                                    <td class='text-center' >"" + ft_string + ""</td>\
                                    <td class='text-center' >"" + year + ""-"" + month + ""-"" + day + ""</td>\
                                    <td class='text-center' >"" + former + ""</td>\
                                    <td>\
                                        <div class='form-group'>\
                                            <div class='custom-switches-stacked'>\
                                                <label class='custom-switch'>\
                                                   ");
            WriteLiteral(@" <input type='radio' name='tog' value='True' id='""+ absence_id +""' class='custom-switch-input switch-status' checked>\
                                                    <span class='custom-switch-indicator'></span>\
                                                    <span class='custom-switch-description'>Justifié</span>\
                                                </label>\
                                                <label class='custom-switch'>\
                                                    <input type='radio' name='tog' value='False' id='""+ absence_id +""' class='custom-switch-input switch-status'>\
                                                    <span class='custom-switch-indicator'></span>\
                                                    <span class='custom-switch-description'>Non Justifié</span>\
                                                </label>\
                                            </div>\
                                        </div>\
                           ");
            WriteLiteral(@"         </td>\
                                </tr>\
                            "");
                        }
                        else {
                            $( "".table-body"" ).append(""\
                                <tr>\
                                    <td>"" + key + ""</td>\
                                    <td class='text-center' >"" + ft_string + ""</td>\
                                    <td class='text-center' >"" + year + ""-"" + month + ""-"" + day + ""</td>\
                                    <td class='text-center' >"" + former + ""</td>\
                                    <td>\
                                        <div class='form-group'>\
                                            <div class='custom-switches-stacked'>\
                                                <label class='custom-switch'>\
                                                    <input type='radio' name='tog' value='True' id='""+ absence_id +""' class='custom-switch-input switch-status' >\
                     ");
            WriteLiteral(@"                               <span class='custom-switch-indicator'></span>\
                                                    <span class='custom-switch-description'>Justifié</span>\
                                                </label>\
                                                <label class='custom-switch'>\
                                                    <input type='radio' name='tog' value='False' id='""+ absence_id +""' class='custom-switch-input switch-status' checked>\
                                                    <span class='custom-switch-indicator'></span>\
                                                    <span class='custom-switch-description'>Non Justifié</span>\
                                                </label>\
                                            </div>\
                                        </div>\
                                    </td>\
                                </tr>\
                            "");                            
                 ");
            WriteLiteral("       }\n                    });\n                },\n                error: function (req, status, error) {\n                    ShowAlert( \"danger_alert\", \"500 Internal server error\" );\n                } \n            });\n        });\n    });\n</script>");
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
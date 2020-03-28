#pragma checksum "/home/dev/Enstitute/Views/Absences/__LoadModalHour.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7020da69b315e958c75c1874992101a987292e6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Absences___LoadModalHour), @"mvc.1.0.view", @"/Views/Absences/__LoadModalHour.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7020da69b315e958c75c1874992101a987292e6d", @"/Views/Absences/__LoadModalHour.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Absences___LoadModalHour : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>

    $(document).ready( function() {

        $("".add-delay"").click( function() {
            var id = $(this).attr(""id"");
            if ( id == ""hour"" ){
                
                $( ""#select_former_delay_hour"" ).empty();
                $( ""#select_day_delay_hour"" ).empty();

                $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 14 "/home/dev/Enstitute/Views/Absences/__LoadModalHour.cshtml"
                     Write(Url.Action("LoadDays"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    success: function (msg) {
                        data = JSON.parse(msg);
                        $.each(data, function(k, v) {
                            $( ""#select_day_delay_hour"" ).append(""\
                            <option value='"" + v['id'] + ""' >\
                                ""+ v['label'] +""\
                            </option>\
                            "");
                        });
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                });

                $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 33 "/home/dev/Enstitute/Views/Absences/__LoadModalHour.cshtml"
                     Write(Url.Action("LoadFormers"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    success: function (msg) {
                        data = JSON.parse(msg);
                        
                        $.each(data, function(k, v) {
                            $( ""#select_former_delay_hour"" ).append(""\
                            <option value='"" + v['id'] + ""' >\
                                ""+ v['firstName'] + "" "" + v['lastName'] +""\
                            </option>\
                            "");
                        });
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                });

            }

        });
        $( "".add-absence"" ).click( function() {
            
            var id = $(this).attr(""id"");

            if ( id == ""hour"" ){

                $( ""#select_day_absence_hour"" ).empty();
                $( ""#select_former_absence_hour"" ).empty();

           ");
            WriteLiteral("     $.ajax({\n                    type: \"POST\",\n                    url: \"");
#nullable restore
#line 65 "/home/dev/Enstitute/Views/Absences/__LoadModalHour.cshtml"
                     Write(Url.Action("LoadDays"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    success: function (msg) {
                        data = JSON.parse(msg);
                        $.each(data, function(k, v) {
                            $( ""#select_day_absence_hour"" ).append(""\
                            <option value='"" + v['id'] + ""' >\
                                ""+ v['label'] +""\
                            </option>\
                            "");
                        });
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                });

                $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 84 "/home/dev/Enstitute/Views/Absences/__LoadModalHour.cshtml"
                     Write(Url.Action("LoadFormers"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    success: function (msg) {
                        data = JSON.parse(msg);
                        
                        $.each(data, function(k, v) {
                            $( ""#select_former_absence_hour"" ).append(""\
                            <option value='"" + v['id'] + ""' >\
                                ""+ v['firstName'] + "" "" + v['lastName'] +""\
                            </option>\
                            "");
                        });
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                });

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

#pragma checksum "/home/dev/Enstitute/Views/Absences/__LoadModalSelects.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8886332c640c3336984ac4eb44b942ac057d5e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Absences___LoadModalSelects), @"mvc.1.0.view", @"/Views/Absences/__LoadModalSelects.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8886332c640c3336984ac4eb44b942ac057d5e3", @"/Views/Absences/__LoadModalSelects.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Absences___LoadModalSelects : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>
    $(document).ready(function(){
        
        $( ""#date_input_absence"" ).change(function(){
            var dayID = new Date( $( ""#date_input_absence"" ).val() ).getDay();
            var groupID = $( ""#groups_select"" ).val();
            $( ""#session_input_absence"" ).empty();
            $.ajax({
                type: ""POST"",
                url: """);
#nullable restore
#line 10 "/home/dev/Enstitute/Views/Absences/__LoadModalSelects.cshtml"
                 Write(Url.Action("LoadSessions"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                dataType: ""text"",
                data : { 
                    dayID: dayID,
                    groupID : groupID,
                },
                success: function (msg) {
                    data = JSON.parse(msg);
                    $.each(data, function(k, v) {
                        $( ""#session_input_absence"" ).append(""\
                        <option value='"" + v['id'] + ""' >\
                            ""+ v['fromHour'] + "":"" + v['fromMinute']  +""\
                            "" + v['toHour']  + "":"" + v['toMinute'] +""\
                        </option>\
                        "");
                    });
                },
                error: function (req, status, error) {
                    ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                } 
            });
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

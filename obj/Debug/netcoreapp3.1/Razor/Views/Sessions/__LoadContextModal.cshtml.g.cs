#pragma checksum "/home/dev/Enstitute/Views/Sessions/__LoadContextModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95b9f84b8e5553fc6278039e5dfadb463ea262b8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sessions___LoadContextModal), @"mvc.1.0.view", @"/Views/Sessions/__LoadContextModal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95b9f84b8e5553fc6278039e5dfadb463ea262b8", @"/Views/Sessions/__LoadContextModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Sessions___LoadContextModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>

$(document).ready(function(){
    $( "".event-disp"" ).click(function(){
        
        $( ""#modalContext"" ).modal(""toggle"");
        $( ""#tree1"" ).empty();
        $( "".list-contexts-added"" ).empty();
        $( "".validator-add-context"" ).empty();

        var session_id =  $(this).attr(""id"") ;
        var day_label = $( this ).attr( ""data-day"" );


        localStorage.setItem(""sessionID"", session_id);
        localStorage.setItem(""dayLabel"", day_label);


        var former_id = """);
#nullable restore
#line 19 "/home/dev/Enstitute/Views/Sessions/__LoadContextModal.cshtml"
                    Write(ViewBag.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\n\n        $.ajax({\n            type: \"POST\",\n            url: \"");
#nullable restore
#line 23 "/home/dev/Enstitute/Views/Sessions/__LoadContextModal.cshtml"
             Write(Url.Action("LoadModules"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            dataType: ""text"",
            data: { 
                formerID :  former_id,
                sessionID : session_id
            },
            async: false,
            success: function (brut_data) {
                data = JSON.parse(brut_data);
                $.each(data, function( k, v ){
                    $( ""#tree1"" ).append( ""<li style='font-size:14px; font-weight: bold;' > "" + v[ 'title' ] + ""<ul>"");
                    $.each(v[ 'precisions' ], function(x, y){
                        $( ""#tree1"" ).append( ""<li style='font-size:12px;margin-left:15px;font-weight:bold;' > "" + y['label'] + ""<ul>"");
                        $.each( y[ 'contexts' ], function( a, b ){
                            $( ""#tree1"" ).append( ""<li style='font-size:10px;margin-left:30px' id='li_before_a_"" + b['id'] + ""' > ""+ b['label'] +"" <button class='add-context-single' id='"" + b['id'] + ""' ><i class='fa fa-plus' style='color:green' aria-hidden='true'></i></button></li>"");
                        });
             ");
            WriteLiteral("           $( \"#tree1\" ).append( \"</ul></li>\");\n                    });\n                    $( \"#tree1\" ).append( \"</ul></li>\");\n                });\n            }\n        });\n    });\n});\n\n</script>");
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

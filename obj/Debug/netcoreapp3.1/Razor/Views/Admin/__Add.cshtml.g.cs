#pragma checksum "/home/dev/Enstitute/Views/Admin/__Add.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a59e8261d6eb09853bc18d8877b62f9c854dddd6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin___Add), @"mvc.1.0.view", @"/Views/Admin/__Add.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a59e8261d6eb09853bc18d8877b62f9c854dddd6", @"/Views/Admin/__Add.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin___Add : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>


    $( document ).ready( function() {

        $( "".btn-add"" ).click( function() {

            var id = $(this).attr(""id"");
            
            if ( id == ""precision"" ){
                var label_input = $( ""#precision_label"" );
                var code_input = $( ""#precision_code"" );
                var module_id = $( ""#precision_module"" );
                var label = htmlspecialchars(label_input);
                var code = htmlspecialchars(code_input);
                if ( label == """" || code == """" ){}
                else if ( label < 3 || label > 50 || code < 3 || code > 10 ){}
                else {
                    $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 21 "/home/dev/Enstitute/Views/Admin/__Add.cshtml"
                     Write(Url.Action("AddPrecision"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    data : {module_id: module_id, label: label, code: code},
                    success: function (msg) {
                        data = JSON.parse(msg);
                        if ( data[""code""] == 1 ){}
                        else if ( data[""code""] == 0 ){
                            $(""#AddModule"").modal('toggle');
                            ShowAlert( ""success_alert"", ""Precision Added succesfully"" );
                        }
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                    });
                }
            }

            else if ( id == ""module"" ){
                var title_input = $( ""#module_title"" );
                var code_input = $( ""#module_code"" );
                var multip = $( ""#module_multiplier"" );
                var title = htmlspecialchars(title_input);
                var c");
            WriteLiteral(@"ode = htmlspecialchars(code_input);
                if ( title == """" || code == """" || multip <= 0  ){}
                else if ( title < 3 || title > 50 || code < 3 || code > 10 ){}
                else {
                    $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 50 "/home/dev/Enstitute/Views/Admin/__Add.cshtml"
                     Write(Url.Action("AddModule"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    data : {title : title,code: code, multiplier : multip},
                    success: function (msg) {
                        data = JSON.parse(msg);
                        if ( data[""code""] == 1 ){}
                        else if ( data[""code""] == 0 ){
                            $(""#AddModule"").modal('toggle');
                            ShowAlert( ""success_alert"", ""Module Added succesfully"" );
                        }
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                    });
                }
            }

            else if ( id == ""departement"" ){
            }

            else if ( id == ""formation"" ){
            }

            else if ( id == ""sector"" ){
            }

            else if ( id == ""grade"" ){
            }

            else if ( id == ""group"" ){
                var code");
            WriteLiteral(@" = $( ""#group_code"" ).val();
                var label = $( ""#group_label"" ).val();
                var scholaryear_code = $( ""#group_scholaryear"" ).val();
                var sector_label = $( ""#group_sector"" ).val();

                if( code == """" || label == """" ){

                }
                else {
                    $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 92 "/home/dev/Enstitute/Views/Admin/__Add.cshtml"
                     Write(Url.Action("AddGroup"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    data : {
                        code: code, 
                        label: label,
                        scholaryear_code: scholaryear_code,
                        sector_label: sector_label,
                    },
                    success: function (msg) {
                        data = JSON.parse(msg);
                        if ( data[""code""] == 1 ){
                            $( "".table-data"" ).empty();
                            RefreashGroupTable();
                        }
                        else if ( data[""code""] == 0 ){
                            $(""#AddGroup"").modal('toggle');
                            $( "".table-data"" ).empty();
                            RefreashGroupTable();
                            ShowAlert( ""success_alert"", ""Precision Added succesfully"" );
                        }
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"",");
            WriteLiteral(@" ""500 Internal server error"" );
                    } 
                    });
                }
            }
            
            else if ( id == ""student"" ){
                var date = new Date( $('#student_date').val() );
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                var first_name = $( ""#student_firstname"" ).val();
                var last_name = $( ""#student_lastname"" ).val();
                var group = $( ""#student_group"" ).val();
                var email = $( ""#student_email"" ).val();
                var phone = $( ""#student_phone"" ).val();
                var nationality = $( ""#student_nationality"" ).val();
                var gender = $( ""#student_gendre"" ).val();
                var zipcode = $( ""#student_gendre"" ).val();
                var city = $( ""#student_gendre"" ).val();
                var street = $( ""#student_gendre"" ).val();
                var appartement = $( ""#student_gend");
            WriteLiteral(@"re"" ).val();
                var password = $( ""#student_password"" ).val();
                var nic = $( ""#student_nic"" ).val();
            
                if ( nationality == """" || phone == """" || email == """" || last_name == """" || first_name == """" || 
                username == """" || street == """" || city == """" || zipcode == """" ){}
                else {
                    $.ajax({
                    type: ""POST"",
                    url: """);
#nullable restore
#line 144 "/home/dev/Enstitute/Views/Admin/__Add.cshtml"
                     Write(Url.Action("AddStudent"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    dataType: ""text"",
                    data : {
                        first_name: first_name, 
                        last_name: last_name,
                        email: email,
                        phone: phone,
                        nationality: nationality,
                        gender_label: gender_label,
                        group_label :  group_label,
                        street :  street,
                        appartement :  appartement,
                        zipcode :  zipcode,
                        city :  city,
                        day: day,
                        month: month, 
                        year: year,
                        password :password,
                        nic: nic
                    },
                    success: function (msg) {
                        data = JSON.parse(msg);
                        if ( data[""code""] == 1 ){
                            $( "".table-data"" ).empty();
                            RefreashStudentT");
            WriteLiteral(@"able();
                        }
                        else if ( data[""code""] == 0 ){
                            $(""#AddStudent"").modal('toggle');
                            ShowAlert( ""success_alert"", ""Precision Added succesfully"" );
                        }
                    },
                    error: function (req, status, error) {
                        ShowAlert( ""danger_alert"", ""500 Internal server error"" );
                    } 
                    });
                }


            }

            else if ( id == ""context"" ){
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

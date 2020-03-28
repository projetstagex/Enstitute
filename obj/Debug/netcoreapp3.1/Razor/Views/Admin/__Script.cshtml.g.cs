#pragma checksum "/home/dev/Enstitute/Views/Admin/__Script.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d51a959d94599a05b6a566181291d3263a811557"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin___Script), @"mvc.1.0.view", @"/Views/Admin/__Script.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d51a959d94599a05b6a566181291d3263a811557", @"/Views/Admin/__Script.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4d408c2afdfcd6397514c7e9f9e44285dcb42fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin___Script : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script>

    function RefreashModuleTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Titre </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>Coefficient </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 16 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadModules"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td><td> ""+ value['title'] +"" </td><td> ""+ value['code'] +"" </td><td> ""+ value['multiplier'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-module' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditModule'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-module' id='""+ value['id'] +""' type='button'>\
                                    <i class='fe fe-trash'></i>\
                                </butto");
            WriteLiteral(@"n>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {
                showAlert( ""danger_alert"", ""500 Internal server error"" );
            } 
        });
    }

    function RefreashPrecisionTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>Module </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 58 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadPrecisions"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['code'] +"" </td>\
                            <td> ""+ value['module']['code'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-precision' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditPrecision'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-precision' id='""+ value['id'] +""' type='button'>\
 ");
            WriteLiteral(@"                                   <i class='fe fe-trash'></i>\
                                </button>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashStudentTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>FullName </th>\
                <th class='d-none d-md-table-cell'>Group </th>\
                <th class='d-none d-md-table-cell'>Nationality </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 101 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadStudents"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['firstName'] + value['lastName'] + "" </td>\
                            <td> ""+ value['group']['label'] +"" </td>\
                            <td> ""+ value['nationality'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-student' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditStudent'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-student' id='""+ value['");
            WriteLiteral(@"id'] +""' type='button'>\
                                    <i class='fe fe-trash'></i>\
                                </button>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashGroupTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>Sector </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 144 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadGroups"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['code'] +"" </td>\
                            <td> ""+ value['sector']['code'] +"" </td>\
                            <td><button class='btn btn-purple edit-group' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditGroup'><i class='fe fe-edit'></i></button></td>\
                            <td><button class='btn btn-red delete-group' id='""+ value['id'] +""' type='button'><i class='fe fe-trash'></i></button></td>\
                        </tr>"");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_aler");
            WriteLiteral(@"t"", ""500 Internal server error"" );} 
        });
    }

    function RefreashContextTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>Precision </th>\
                <th class='d-none d-md-table-cell'>Module</th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 179 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadContexts"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);

                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['code'] +"" </td>\
                            <td> ""+ value['precision']['code'] +"" </td>\
                            <td> ""+ value['precision']['module']['code'] +"" </td>\
                            <td><button class='btn btn-purple edit-context' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditContext'><i class='fe fe-edit'></i></button></td>\
                            <td><button class='btn btn-red delete-context' id='""+ value['id'] +""' type='button'><i class='fe fe-trash'></i></button></td>\
                        </tr>"");
                ");
            WriteLiteral(@"}); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashFormationTable(input) {

        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>TotalYears </th>\
                <th class='d-none d-md-table-cell'>Depatement </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 217 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadSectors"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['totalYears'] +"" </td>\
                            <td> ""+ value['departement']['code'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-departement' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditDepartement'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-departement' id='""+ value['id'] +""' ");
            WriteLiteral(@"type='button'>\
                                    <i class='fe fe-trash'></i>\
                                </button>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashGradeTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 259 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadGrades"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['code'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-grade' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditGrade'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-grade' id='""+ value['id'] +""' type='button'>\
                                    <i class='fe fe-trash'></i>\
                  ");
            WriteLiteral(@"              </button>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashSectorTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
                <th class='d-none d-md-table-cell'>Code </th>\
                <th class='d-none d-md-table-cell'>Formation </th>\
                <th class='d-none d-md-table-cell'>Grade </th>\
                <th class='d-none d-md-table-cell'></th>\
                <th class='d-none d-md-table-cell'></th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 302 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadSectors"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td> ""+ value['formation']['code'] +"" </td>\
                            <td> ""+ value['grade']['code'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-sector' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditSector'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-sector' id='""+ value['id'] +""' type='button'>");
            WriteLiteral(@"\
                                    <i class='fe fe-trash'></i>\
                                </button>\
                            </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }

    function RefreashDepartementTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Label </th>\
            </tr>\
        "");
        $.ajax({
            type: ""POST"",
            url: """);
#nullable restore
#line 341 "/home/dev/Enstitute/Views/Admin/__Script.cshtml"
             Write(Url.Action("LoadDepartements"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data : { input: input },
            dataType: ""text"",
            success: function (msg) {
                data = JSON.parse(msg);
                $.each( data, function( key, value ) {
                    $( "".table-data"" ).append(""\
                        <tr>\
                            <td> ""+ value['id'] +"" </td>\
                            <td> ""+ value['label'] +"" </td>\
                            <td>\
                                <button class='btn btn-purple edit-departement' id='""+ value['id'] +""' type='button' data-toggle='modal' data-target='#EditDepartement'>\
                                    <i class='fe fe-edit'></i>\
                                </button>\
                            </td>\
                            <td>\
                                <button class='btn btn-red delete-departement' id='""+ value['id'] +""' type='button'>\
                                    <i class='fe fe-trash'></i>\
                                </button>\
                 ");
            WriteLiteral(@"           </td>\
                        </tr>\
                    "");
                }); 
            },
            error: function (req, status, error) {showAlert( ""danger_alert"", ""500 Internal server error"" );} 
        });
    }
    
    function RefreashModuleAffectationTable(input) {
        $( "".table-data"" ).append(""\
            <tr>\
                <th>#</th>\
                <th class='d-none d-md-table-cell'>Fromateur </th>\
                <th class='d-none d-md-table-cell'>Module </th>\
                <th class='d-none d-md-table-cell'>Groupe </th>\
            </tr>\
        "");
    }

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

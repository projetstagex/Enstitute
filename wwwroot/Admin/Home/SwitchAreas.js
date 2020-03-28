$(document).ready(function(){
    
    $("#departement_field").hide();
    $("#former_field").hide();
    $("#grade_field").hide();
    $("#group_field").hide();
    $("#module_affectation_field").hide();
    $("#module_field").hide();
    $("#sector_field").hide();
    $("#student_field").hide();

    $("input[name='add_button']").click(function(){
        $("#departement_field").hide();
        $("#former_field").hide();
        $("#grade_field").hide();
        $("#group_field").hide();
        $("#module_affectation_field").hide();
        $("#module_field").hide();
        $("#sector_field").hide();
        $("#student_field").hide();


        var id = $(this).val();
        var field = "#"+id+"_field";
        $(field).show();
    });

});

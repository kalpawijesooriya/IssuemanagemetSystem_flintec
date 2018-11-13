$('#Material').select2({
    placeholder: "Select Material.....",
    allowClear: false
});

function SubmitMaterialDelay() {

    obj = new Object();

    obj.issue_date = new Date();
    obj.material_id = document.getElementById("Material").value;
    obj.description = document.getElementById("description").value;
    obj.line_line_id = document.getElementById("lineID").innerText;
    obj.issue_issue_ID ="2";
    obj.responsible_person_emp_id ="";
    obj.issue_satus ="0";
    obj.location ="KTY";
    obj.responsible_person_confirm_status = "0";

    var obj_array = new Array();
    obj_array.push(obj);

    var issueJson = JSON.stringify(obj_array);

    $.ajax({
        type: "POST",
        dataType: 'text',
        url: "/Supervisor/AddMaterialDelay",
        data: { issueJson: issueJson},
        success: function (feedback) {
            alert(feedback);
        },
        error: function () {
            alert("Error occurred");
        }
    });
}
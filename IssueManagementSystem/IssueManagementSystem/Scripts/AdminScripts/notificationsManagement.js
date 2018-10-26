
$('#selectdepartment').select2({
    placeholder: "Select Department.....",
    allowClear: false
});

$('#selectLine').select2({
    placeholder: "Select Line....",
    allowClear: false
});


$('#selectIssue').select2({
    placeholder: "Select Issue....",
    allowClear: false
});

$('#Name').select2({
    placeholder: "Search Name....",
    allowClear: false
});

$('#Department').select2({
    placeholder: "Search Department....",
    allowClear: false
});
function addPerson() {
    var name = document.getElementById("name").value;
    var department = document.getElementById("dept").value;


    addLine(name, department);
}

function addLine(name, department) {

    var list_element = document.createElement("li");
    var main_div = document.createElement("div");

    list_element.className = "ui-state-default";
    list_element.style.cssText = 'overflow:hidden; display:flex;';
    main_div.style.cssText = 'display:grid;grid-template-columns:4fr 1fr 1fr 1fr 1fr 1fr;';

    var inner_div = list_element.appendChild(main_div);

    var divName = document.createElement("div");
    var divName_inner = document.createElement("p");
    divName_inner.nodeValue = name + " (" + department + ") ";
    divName.appendChild(divName_inner);
    inner_div.appendChild(divName);


    var divEmail = document.createElement("div");
    var divEmail_inner = document.createElement("input");
    divEmail_inner.type = "checkbox";
    divEmail.appendChild(divEmail_inner);
    inner_div.appendChild(divEmail);

    var divSMS = document.createElement("div");
    var divSMS_inner = document.createElement("input");
    divSMS_inner.type = "checkbox";
    divSMS.appendChild(divSMS_inner);
    inner_div.appendChild(divSMS);

    var divCall = document.createElement("div");
    var divCall_inner = document.createElement("input");
    divCall_inner.type = "checkbox";
    divCall.appendChild(divCall_inner);
    inner_div.appendChild(divCall);

    var div2 = document.createElement("div");
    var div2_inner = document.createElement("input");
    div2_inner.style.cssText = 'width:80% !important;';
    div2_inner.type = "text";
    div2.appendChild(div2_inner);
    inner_div.appendChild(div2);

    var div1 = document.createElement("div");
    var div1_inner = document.createElement("input");
    div1_inner.style.cssText = 'width:80% !important;';
    div1_inner.type = "text";
    div1.appendChild(div1_inner);
    inner_div.appendChild(div1);

    document.getElementById("sortable").appendChild(list_element);

}
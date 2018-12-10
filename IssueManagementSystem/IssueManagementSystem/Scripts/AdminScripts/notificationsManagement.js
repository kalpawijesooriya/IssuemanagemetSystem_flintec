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

$('#managerName').select2({
    placeholder: "Search Name....",
    allowClear: false
});

$('#managerDepartment').select2({
    placeholder: "Search Department....",
    allowClear: false
});

function addPerson() {
    var name  = document.getElementById("name").innerHTML;
    var empID = document.getElementById("empID").innerHTML;

    empID = empID.trim();

    if (empID != "") {

        var flag = 0;

        if ($('#sortable li').length > 1) {
            //if UL has more than one list element

            var ul = document.getElementById("sortable");
            var items = ul.getElementsByTagName("li");
            for (var i = 1; i < items.length; ++i) {
                console.log(i);
                var EmployeeNumber = Number(items[i].childNodes[0].childNodes[0].childNodes[0].innerHTML.split('-')[0].trim());

                if (EmployeeNumber == empID)
                {
                    alert("User already added");
                    flag = 1;
                    break;
                }
            }
        } 
    }
    else {
        alert("Please select a person");
    }

    if (flag == 0) {
        addLine(name, empID);
    }
}

function addmanager()
{
    var name = document.getElementById("managername").innerHTML;
    var empID = document.getElementById("managerempID").innerHTML;

    empID = empID.trim();

    if (empID != "") {

        var flag = 0;

        if ($('#managersortable li').length > 1) {
            //if UL has more than one list element

            var ul = document.getElementById("managersortable");
            var items = ul.getElementsByTagName("li");
            for (var i = 1; i < items.length; ++i) {
                console.log(i);
                var EmployeeNumber = Number(items[i].childNodes[0].childNodes[0].childNodes[0].innerHTML.split('-')[0].trim());

                if (EmployeeNumber == empID) {
                    alert("User already added");
                    flag = 1;
                    break;
                }
            }
        }
    }
    else {
        alert("Please select a person");
    }

    if (flag == 0) {
        addLinemanager(name, empID);
    }
}

function addLine(name, empID) {

    var list_element = document.createElement("li");
    var main_div = document.createElement("div");

    list_element.className = "ui-state-default";
    list_element.style.cssText = 'overflow:hidden;display:flex;';
    main_div.style.cssText = 'display:grid;grid-template-columns:3fr 1fr 1fr 1fr 2fr 1fr;';

    var inner_div = list_element.appendChild(main_div);

    var divName = document.createElement("div");
    var divName_inner = document.createElement("p");
    divName_inner.setAttribute("id",name);
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
    div2.setAttribute("class", "text-center");
    div2_inner.type = "text";
    div2.appendChild(div2_inner);
    inner_div.appendChild(div2);

    var div1 = document.createElement("div");
    var div1_inner = document.createElement("input");
    div1_inner.style.cssText = 'width:80% !important;';
    div1.setAttribute("class", "text-center");
    div1_inner.type = "text";
    div1.appendChild(div1_inner);
    inner_div.appendChild(div1);

    document.getElementById("sortable").appendChild(list_element);

    document.getElementById(name).innerHTML = empID+" - "+name;

}

function addLinemanager(name, empID) {

    var list_element = document.createElement("li");
    var main_div = document.createElement("div");

    list_element.className = "ui-state-default";
    list_element.style.cssText = 'overflow:hidden;display:flex;;width:80%';
    main_div.style.cssText = 'display:grid;grid-template-columns:3fr 1fr 1fr 1fr 1fr';

    var inner_div = list_element.appendChild(main_div);

    var divName = document.createElement("div");
    var divName_inner = document.createElement("p");
    divName .style.cssText = 'margin-left:20px';
    divName_inner.setAttribute("id", name);
    divName.appendChild(divName_inner);
    inner_div.appendChild(divName);

    var divEmail = document.createElement("div");
    var divEmail_inner = document.createElement("input");
   divEmail.style.cssText = 'margin-left:-45%';
    divEmail_inner.type = "checkbox";
    divEmail.appendChild(divEmail_inner);
    inner_div.appendChild(divEmail);

    var divSMS = document.createElement("div");
    var divSMS_inner = document.createElement("input");
    divSMS.style.cssText = 'margin-left:-65%';
    divSMS_inner.type = "checkbox";
    divSMS.appendChild(divSMS_inner);
    inner_div.appendChild(divSMS);

    var divCall = document.createElement("div");
    var divCall_inner = document.createElement("input");
   divCall.style.cssText = 'margin-left:-75%';
    divCall_inner.type = "checkbox";
    divCall.appendChild(divCall_inner);
    inner_div.appendChild(divCall);

    var div2 = document.createElement("div");
    var div2_inner = document.createElement("input");
    div2_inner.style.cssText = 'width:80% !important;';
    div2.setAttribute("class", "text-center");
    div2.style.cssText = 'width:60%;margin-left:-95%';
    div2_inner.type = "text";
    div2.appendChild(div2_inner);
    inner_div.appendChild(div2);


    document.getElementById("managersortable").appendChild(list_element);

    document.getElementById(name).innerHTML = empID + " - " + name;

}






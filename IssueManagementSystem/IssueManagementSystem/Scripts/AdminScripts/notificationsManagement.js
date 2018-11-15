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

function addLine(name, empID) {

    var list_element = document.createElement("li");
    var main_div = document.createElement("div");

    list_element.className = "ui-state-default";
    list_element.style.cssText = 'overflow:hidden;display:flex;';
    main_div.style.cssText = 'display:grid;grid-template-columns:4fr 1fr 1fr 1fr 1fr 1fr;';

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

    document.getElementById(name).innerHTML = empID+" - "+name;

}


function fillDepartmentDropDown() {

    var name = document.getElementById("Name").value;

    $.ajax({
        type: "POST",
        dataType: 'text',
        url: "/admin/fillDepartmentDropDown",
        data: { person_name:name},
        success: function (itemNameArray) {
            console.log(itemNameArray);
            var obj = JSON.parse(itemNameArray);
            console.log(obj);
        },
        error: function () {
            alert("Error occurred");
        }

    });
    
}

function fillNameDropDown() {

    var department = document.getElementById("Department").value;
    var nameBox = document.getElementById("Name");

    $.ajax({
        type: "POST",
        dataType: 'text',
        url: "/admin/fillNameDropDown",
        data: { department:department },
        success: function (itemNameArray) {
            var obj = JSON.parse(itemNameArray);
            console.log(obj);

            nameBox.options.length = 0;
            var opt1 = document.createElement('option');
            opt1.innerHTML = '';
            opt1.value = '';
            nameBox.appendChild(opt1);

            for (var i = 0; i < obj.length; i++) {
                var opt = document.createElement('option');
                opt.innerHTML = obj[i];
                opt.value = obj[i];
                nameBox.appendChild(opt);
            }
        },
        error: function () {
            alert("Error occurred");
        }
    });
}

function fillUserDetails() {

    var nameBox = document.getElementById("Name");

    var name_text = document.getElementById("name");
    var dept_text = document.getElementById("dept");
    var empID_text = document.getElementById("empID");
    var position_text = document.getElementById("position");
    var phone_text = document.getElementById("phone");
    var email_text = document.getElementById("email");
    
    if (nameBox.value != "")
    {
        var userID = nameBox.value.split('-')[0];
        userID = userID.trim();


        $.ajax({
            type: "POST",
            dataType: 'text',
            url: "/admin/getUserDetails",
            data: { userID: userID },
            success: function (itemNameArray) {
                var obj = JSON.parse(itemNameArray);
                console.log(obj);

                name_text.innerHTML = obj[0].Name;
                dept_text.innerHTML = obj[0].Department;
                empID_text.innerHTML = obj[0].EmployeeNumber;
                position_text.innerHTML = obj[0].Position;
                phone_text.innerHTML = obj[0].Phone;
                email_text.innerHTML = obj[0].EMail;

            },
            error: function () {
                alert("Error occurred");
            }
        });
    }
}

function saveList() {

    var obj_array = new Array();

    var ul = document.getElementById("sortable");
    var items = ul.getElementsByTagName("li");
    for (var i = 1; i < items.length; ++i)
         {
        obj = new Object();

        obj.issue_id = document.getElementById("selectIssue").value;
        obj.line_id = document.getElementById("selectLine").value;
        obj.EmployeeNumber = Number(items[i].childNodes[0].childNodes[0].childNodes[0].innerHTML.split('-')[0].trim());
        obj.assigned_date = new Date();

        var state1 = items[i].childNodes[0].childNodes[1].childNodes[0].checked;
        console.log(state1);
        if (state1.toString() == "true") { obj.email = 1; } if (state1.toString() == "false") { obj.email = 0; }

        var state2 = items[i].childNodes[0].childNodes[3].childNodes[0].checked;
        if (state2.toString() == "true") { obj.call = 1; } if (state2.toString() == "false") { obj.call = 0;}

        var state3 = items[i].childNodes[0].childNodes[2].childNodes[0].checked;
        if (state3.toString() == "true") { obj.message = 1; } if (state3.toString() == "false") { obj.message = 0;}

        obj.callRepetitionTime = items[i].childNodes[0].childNodes[5].childNodes[0].value;
        obj.sendAlertAfter = items[i].childNodes[0].childNodes[4].childNodes[0].value;
        obj.levelOfResponsibility = i;
        obj.issue_line_person_id = items[i].childNodes[0].childNodes[0].childNodes[0].innerHTML.split('-')[0].trim();

        obj_array.push(obj);
    }
    console.log(obj_array);

    var userList_json = JSON.stringify(obj_array);

    $.ajax({
        type: "POST",
        dataType: 'text',
        url: "/admin/saveList",
        data: { userList_json: userList_json },
        success: function (response) {
           alert(response);
        },
        error: function (response)
        {
           alert("Error occurred");
        }
    });
}
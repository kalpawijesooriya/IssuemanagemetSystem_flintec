$('select').select2({
    minimumResultsForSearch: -1,
    allowClear: false
});

$(document).ready(function() {
        google.load("visualization", "1", {packages:["corechart"]});

        google.setOnLoadCallback(drawChart1);
        google.setOnLoadCallback(drawChart2);
        google.setOnLoadCallback(drawChart3);

        $("#datetimepicker1").datepicker({
        });

        $("#datetimepicker2").datepicker({
        });

        $("#datetimepicker3").datepicker({
        });

        $("#datetimepicker4").datepicker({
        });

        var d = new Date();
        d.setMonth(d.getMonth() - 1);
        var d2 = new Date();

        $("#datetimepicker1").datepicker().datepicker("setDate", d);
        $("#datetimepicker2").datepicker().datepicker("setDate",d2);
        $("#datetimepicker3").datepicker().datepicker("setDate", d);
        $("#datetimepicker4").datepicker().datepicker("setDate",d2);

        filterByDate();
        createTable();
        loadTableData();
        loadTablePage(1);
        filterTableData();
});

 var data_obj = new Array();
 var raw_data = new Array();

function loadTableData(){

        $.ajax({
            type:"POST",
            dataType:'text',
            async:false,
            url:url_loadIssueList,
            data:{},
            success: function (data)
                { 
                        var dataArray = JSON.parse(data);

                        raw_data = dataArray;
                        console.log(raw_data);
                        console.log(data_obj);
                        dataArray.forEach(function (i) 
                            {
                                var tempArr = new Array();

                                var ele1 = i.issue_date.split('(')[1];
                                ele1 = ele1.split(')')[0];

                                tempArr.push(Unix_timestamp(ele1,'ymd'));              //1
                                tempArr.push(i.issue_occurrence_id);                   //2
                                tempArr.push(i.issue);                                 //3
                                tempArr.push(i.line_name);                             //4
                                if (i.issue_satus == '1') { tempArr.push('Unsolved');} //5
                                if (i.issue_satus == '0') { tempArr.push('Solved'); }  //6
                                tempArr.push(i.location);                              //7
                                tempArr.push(i.description);                           //8
                                tempArr.push(i.Name);                                  //9
                                tempArr.push(i.material_id);                           //10
                                tempArr.push(i.machine_machine_id);                    //11
                                tempArr.push(i.line_line_id);                          //12
                                tempArr.push(i.issue_issue_ID);                        //13
                                tempArr.push(i.responsible_person_confirm_status);     //14
                                tempArr.push(i.responsible_person_confirm_feedback);   //15
                                tempArr.push(i.solved_date);                           //16
                                tempArr.push(i.commented_date);                        //17
                                tempArr.push(i.department);                            //18
                                tempArr.push(i.solved_emp);                            //19
                                tempArr.push(i.buzzer_off_by);                         //20
                                tempArr.push(i.buzzer_off_time);                       //21
                                tempArr.push(i.dep_occured);                           //22
                                tempArr.push(i.job_card);                              //23
                                data_obj.push(tempArr);
                            });
                },
                error: function ()
                {
                    alert("Error occurred");
                }
            });

}

function filterTableData(){
   
        data_obj.splice(0, data_obj.length);

        var Date_select_start = (new Date(document.getElementById('datetimepicker3').value+" 00:00 UTC")).toISOString().substring(0, 10);
        var Date_select_end = (new Date(document.getElementById('datetimepicker4').value+" 23:59 UTC")).toISOString().substring(0, 10);

        var Plant_select = document.getElementById('plantSelectBox2').value;
        var Issue_select = document.getElementById('issueSelectBox').value
        var Department_select = document.getElementById('dptSelectBox').value;
        var Line_select =   document.getElementById('lineSelectBox').value;
        var Status_select  = document.getElementById('statusSelectBox').value;

        var o= ""; var m= ""; var b= ""; var nq="";var de="";

        (Issue_select  == "") ? (nq="*0"):(nq="*1"); //Issue_select
        (Line_select   == "") ? (m="*0"):(m="*1");  //Line_select
        (Status_select == "") ? (o="*0"):(o="*1") ;  //Status_select
        (Plant_select  == "")? (b="*0"):(b="*1");    //Plant_select
        (Department_select == "")? (de="*0"):(de="*1"); //Department_select

        var comparing_string = b+nq+m+o+de;

        var condition = false;

        raw_data.forEach(function (i) 
                {
                        var countx=0;
                        var p =i.location.trim();
                        var is =i.issue.trim();
                        var l =i.line_name.trim();
                        var s =i.issue_satus.trim();
                        var dep =i.dep_occured;
                        var dateVar = i.issue_date.split('(')[1];
                        dateVar = dateVar.split(')')[0];

                        var dateCheck_result = dateCheck(new Date(Unix_timestamp(dateVar,'ymd')),
                                                         new Date(document.getElementById('datetimepicker3').value+" 00:00 UTC"),
                                                         new Date(document.getElementById('datetimepicker4').value+" 23:59 UTC")
                                                        );

                        var var_array = ["*0","*1"];
                            OuterLoop:
                            for(var x =0;x<var_array.length;x++){
                                for(var y=0;y<var_array.length;y++){
                                    for(var z=0;z<var_array.length;z++){
                                        for(var q=0;q<var_array.length;q++){
                                            for(var u=0;u<var_array.length;u++){
                                                for(var v=0;v<var_array.length;v++){ 

                                                   var binary_code = var_array[v]+var_array[u]+var_array[q]+var_array[y]+var_array[x];

                                                   if(comparing_string == binary_code)
                                                        {
                                                             o= (s===Status_select);
                                                             m= (l===Line_select); 
                                                             b= (p===Plant_select);
                                                             nq= (is===Issue_select);
                                                             de= (dep==Department_select);
          
                                                            var split_str = binary_code.split("*");

                                                            (split_str[1] == "0") ? (b=true):(true);   //Issue_select
                                                            (split_str[2] == "0") ? (nq=true):(true);  //Line_select
                                                            (split_str[3] == "0") ? (m=true):(true);   //Status_select
                                                            (split_str[4] == "0") ? (o=true):(true);   //Plant_select
                                                            (split_str[5] == "0") ? (de=true):(true);   //Department_select
                                                            condition =  ( b && nq && m && o && de && dateCheck_result);//b+nq+m+o

                                                            break OuterLoop;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        if(condition==true)
                            {
                                var tempArr = new Array();

                                var ele1 = i.issue_date.split('(')[1];
                                ele1 = ele1.split(')')[0];
                                var d = Unix_timestamp(ele1,'ymd');

                                var flag = 0;

                                tempArr.push(d);
                                tempArr.push(i.issue_occurrence_id);
                                tempArr.push(i.issue);
                                tempArr.push(i.line_name);

                                if (i.issue_satus == '1') { tempArr.push('Unsolved');}
                                if (i.issue_satus == '0') { tempArr.push('Solved'); }

                                tempArr.push(i.location);
                                tempArr.push(i.description);
                                tempArr.push(i.Name);
                                tempArr.push(i.material_id);
                                tempArr.push(i.machine_machine_id);
                                tempArr.push(i.line_line_id);
                                tempArr.push(i.issue_issue_ID);
                                tempArr.push(i.responsible_person_confirm_status);
                                tempArr.push(i.responsible_person_confirm_feedback);
                                tempArr.push(i.solved_date);
                                tempArr.push(i.commented_date);
                                tempArr.push(i.department);
                                tempArr.push(i.solved_emp);
                                tempArr.push(i.buzzer_off_by);
                                tempArr.push(i.buzzer_off_time);
                                tempArr.push(i.dep_occured);
                                tempArr.push(i.job_card);
                                data_obj.push(tempArr);
                                flag = 1;

                                condition = false;
                            }
                    });
        loadTablePage(1);
}

function loadTablePage(page){
            var items_per_page = 10;
            var number_of_pages = Math.ceil((data_obj.length)/items_per_page);
            createPagination(number_of_pages);
            var starting_index = ((items_per_page*page)-items_per_page)+1;
            var last_index = (items_per_page*page);
            if(last_index>data_obj.length){last_index=data_obj.length;}
            var obj2 = new Array();

            for(var i=(starting_index-1);i<=(last_index-1);i++)
            {
                    obj2.push(data_obj[i]);
            }

            loadAccordionTable(obj2);

            var idString = (Number(page)-1).toString();
            var paginationItem = document.getElementById(""+idString);
            if(paginationItem != null ){
                     paginationItem.setAttribute("class","page-item active");
                }

            $('[data-toggle="tooltip"]').tooltip();
            $('.myCheckbox').bootstrapToggle();
}

function loadAccordionTable(obj)
{
        var accordion  =  document.getElementById('accordion');
        var childElements = accordion.childNodes;
        $("#no_IssuesDIV").empty();
         if(obj.length<1){
             var no_IssuesDIV = document.createElement('p');
             no_IssuesDIV.innerHTML= "NO ISSUES";
             no_IssuesDIV.style.color="#d1d1d1";
             no_IssuesDIV.style.fontSize ="70px";
             no_IssuesDIV.style.textAlign = "center";
             document.getElementById('no_IssuesDIV').appendChild(no_IssuesDIV);
          }    

        $("#accordion").empty();
        for(var i=0;i<obj.length;i++){
            createAccordionLine(accordion,obj[i][0],obj[i][1],obj[i][2],obj[i][5],obj[i][3],obj[i][4],obj[i][6],
            obj[i][7],obj[i][8],obj[i][9],obj[i][10],obj[i][11],obj[i][12],obj[i][13],obj[i][14],obj[i][15],
            obj[i][16],obj[i][17],obj[i][18],obj[i][19],obj[i][20],obj[i][21]);
        }

        $( function() {
            $( "#accordion" ).accordion();
            $( "#accordion" ).accordion("refresh");
        });

}

    //location                            //5
    //description                         //6
    //Name                                //7
    //material_id                         //8
    //machine_name                        //9
    //line_line_id                        //10
    //issue_issue_ID                      //11
    //responsible_person_confirm_status   //12
    //responsible_person_confirm_feedback //13
    //solved_date                         //14
    //commented_date                      //15
    //department                          //16
    //solved_emp                          //17
    //buzzer_off_by                       //18
    //buzzer_off_time                     //19
    //dep_occured                         //20

function createAccordionLine(accordion,date,id,issue,plant,line,status,desc,resp,matID,machID,linID,issID,respStatus,respFeed,solDate,comDate,dept,solEmp,buzOffBy,buzOffT,deptOccrd,jbcrd){

    var dateDIV  =  document.createElement('div');
    var idDIV    =  document.createElement('div');
    var issueDIV =  document.createElement('div');
    var plantDIV =  document.createElement('div');
    var lineDIV  =  document.createElement('div');
    var statusDIV=  document.createElement('div');

    dateDIV.setAttribute("class","col-md-2");
    dateDIV.innerHTML = date;

    idDIV.setAttribute("class","col-md-1");
    idDIV.innerHTML = id;

    issueDIV.setAttribute("class","col-md-5");
    issueDIV.style.cssText='overflow:hidden';
    issueDIV.style.whiteSpace='nowrap';
    issueDIV.innerHTML = issue+" : "+desc;

    plantDIV.setAttribute("class","col-md-1");
    plantDIV.innerHTML = plant;

    lineDIV.setAttribute("class","col-md-2");
    lineDIV.innerHTML = line;

    statusDIV.setAttribute("class","col-md-1");
    statusDIV.innerHTML = status;

    var rowDIVinner = document.createElement('div');
    rowDIVinner.setAttribute("class","row");
    rowDIVinner.appendChild(dateDIV);
    rowDIVinner.appendChild(idDIV);
    rowDIVinner.appendChild(issueDIV);
    rowDIVinner.appendChild(plantDIV);
    rowDIVinner.appendChild(lineDIV);
    rowDIVinner.appendChild(statusDIV);

    var rowDIVinner1 = document.createElement('div');
    rowDIVinner1.setAttribute("class","col-md-12");
    rowDIVinner1.appendChild(rowDIVinner);

    var rowDIVcontainer = document.createElement('div');
    rowDIVcontainer.setAttribute("class","container");
    rowDIVcontainer.style.display="inline-flex";
    rowDIVcontainer.style.width="100%";

    rowDIVcontainer.appendChild(rowDIVinner1);

    var detailsDIV =  document.createElement('div');

    var p1 =  document.createElement('p');
    var p2 =  document.createElement('p');
    var p3 =  document.createElement('p');
    var p4 =  document.createElement('p');
    var p5 =  document.createElement('p');
    var p6 =  document.createElement('p');
    var p7 =  document.createElement('p');
    var p8 =  document.createElement('p');

    var p9 =  document.createElement('p');
    var p10 = document.createElement('p');
    var p11 = document.createElement('p');
    var p12 = document.createElement('p');
    var p13 = document.createElement('p');
    var p14 = document.createElement('p');
    var p15 = document.createElement('p');
    var p16 = document.createElement('p');
   
    var div_1_left =  document.createElement('div');
    var div_1_right =  document.createElement('div');
    var div_1_container =  document.createElement('div');
    var div_1_row =  document.createElement('div');
    var eye_span =  document.createElement('span');

    div_1_left.appendChild(p1);
    div_1_left.appendChild(p2);
    div_1_left.appendChild(eye_span);
    div_1_left.appendChild(p3);
    div_1_left.appendChild(p16);
    div_1_left.appendChild(p4);
    div_1_left.appendChild(p5);
    div_1_left.appendChild(p6);
    div_1_left.appendChild(p7);
    div_1_left.appendChild(p8);

    div_1_right.appendChild(p9);
    div_1_right.appendChild(p10);
    div_1_right.appendChild(p11);
    div_1_right.appendChild(p12);
    div_1_right.appendChild(p13);
    div_1_right.appendChild(p14);
    div_1_right.appendChild(p15);


    div_1_container.appendChild(div_1_row);
    div_1_row.appendChild(div_1_left);
    div_1_row.appendChild(div_1_right);
 
    div_1_container.setAttribute("class","container");
    div_1_row.setAttribute("class","row");
    div_1_left.setAttribute("class","col-md-6");
    div_1_left.setAttribute("style","overflow-wrap: break-word;");
    div_1_right.setAttribute("class","col-md-6");
    div_1_right.setAttribute("style","overflow-wrap: break-word;");


        $.ajax({
        type:"POST",
        dataType:'text',
        async:false,
        url:url_checkNotificationList_formanagers,
        data:{empID:document.getElementById('hidden_userID').innerHTML,issueOccID:id},
        success:function(flag){

            var flagVal = flag;

            if(flagVal == 'true'){
                    
                var notification_btn_span = document.createElement('span');
                var notification_btn =  document.createElement('input');

                notification_btn_span.appendChild(notification_btn);
                div_1_right.appendChild(notification_btn_span);

                notification_btn_span.title = "Turn on/off SMS,Call,Email notifications";
                notification_btn_span.setAttribute("data-toggle","tooltip");
                notification_btn.type="checkbox";
                notification_btn.setAttribute('onChange',"notificationOnOff('"+id+"')");
                notification_btn.setAttribute("class","myCheckbox");
                notification_btn.dataset.toggle = "toggle";
                notification_btn.setAttribute('checked',"true");
                notification_btn.dataset.on = "<i class='fas fa-bell fa-1x' style='font-size:20px'>&nbspon</i>";
                notification_btn.dataset.off = "<i class='fas fa-bell-slash fa-1x' style='font-size:20px'>&nbspoff&nbsp&nbsp</i>";
            }

        },
        error:function(){
             console.log("Error");
        }
        });


    detailsDIV.appendChild(div_1_container);

    (desc!=null)?(p1.innerHTML="<span style=' color:#5b5a5a;font-weight: bold'>Description</span> : <span  style='font-size:16px;'>"+desc+"</span>"):(p1.innerHTML="");
    (resp!=null)?(p2.innerHTML="<span style=' color:#5b5a5a;font-weight: bold'>Responisble Person </span> :<span  style='font-size:16px'>"+resp+" - "+dept+"</span>"):(p2.innerHTML="");

    if(respStatus!=null && respStatus== 0)
        {  
            eye_span.setAttribute("class","fas fa-eye");
            eye_span.setAttribute("data-toggle","tooltip");
            eye_span.title = "Checked by responsible person";
            eye_span.style.color="#42c44d";
        }
        else{

            eye_span.setAttribute("class","fas fa-eye-slash");
            eye_span.setAttribute("data-toggle","tooltip");
            eye_span.title = "To be checked by responsible person";
            eye_span.style.color="#e04c4c";
        }
 

    (machID!=null)?(p4.innerHTML       ="<span style='color:#5b5a5a;font-weight: bold'>Machine </span>:<span  style='font-size:16px'>"+machID+"</span>"):(p4.innerHTML ="");
    (matID!=null)?(p3.innerHTML        ="<span style='color:#5b5a5a;font-weight: bold'>Material  </span>:<span  style='font-size:16px'>"+matID+"</span>"):(p3.innerHTML ="");
    (jbcrd!=null)?(p16.innerHTML        ="<span style='color:#5b5a5a;font-weight: bold'>Job Card  </span>:<span  style='font-size:16px'>"+jbcrd+"</span>"):(p16.innerHTML ="");
    //(linID!=null)?(p5.innerHTML      ="<span style='color:#5b5a5a;font-weight: bold'>Line Id </span>:<span  style='font-size:16px'>"+linID+"</span>"):(p5.innerHTML ="");
    //(issID!=null)?(p6.innerHTML      ="<span style='color:#5b5a5a;font-weight: bold'>Issue Id </span>:<span  style='font-size:16px'>"+issID+"</span>"):(p6.innerHTML ="");
    (respFeed!=null)?(p8.innerHTML     ="<span style='color:#5b5a5a;font-weight: bold'>Responsible Person Feedback </span>:<span  style='font-size:16px'>"+respFeed+"</span>"):(p8.innerHTML ="");
    (solDate!=null)?(p9.innerHTML      ="<span style='color:#5b5a5a;font-weight: bold'>Solved Date </span>:<span  style='font-size:16px'>"+Unix_timestamp((solDate.split('(')[1]).split(')')[0],'ymdt')+"</span>"):(p9.innerHTML ="");
    (comDate!=null)?(p10.innerHTML     ="<span style='color:#5b5a5a;font-weight: bold'>Commented Date</span> :<span  style='font-size:16px'>"+ Unix_timestamp((comDate.split('(')[1]).split(')')[0],'ymdt')+"</span>"):(p10.innerHTML ="");
    //(manNotStatus!=null)?(p11.innerHTML="<span style='color:#5b5a5a;font-weight: bold'>Responsible Person Feedback </span>:<span  style='font-size:16px'>"+manNotStatus):(p11.innerHTML ="");
    //(dept!=null)?(p12.innerHTML      ="<span style='color:#5b5a5a;font-weight: bold'>Responsible Department </span>:<span  style='font-size:16px'>"+dept+"</span>"):(p12.innerHTML ="");
    (solEmp!=null)?(p13.innerHTML    ="<span style='color:#5b5a5a;font-weight: bold'>Issue resolved by </span>:<span  style='font-size:16px'>"+solEmp+"</span>"):(p13.innerHTML ="");
    (buzOffBy!=null)?(p14.innerHTML    ="<span style='color:#5b5a5a;font-weight: bold'>Buzzer Turned off by </span>:<span  style='font-size:16px'>"+buzOffBy+" at "+Unix_timestamp((buzOffT.split('(')[1]).split(')')[0],'ymdt')+"</span>"):(p14.innerHTML ="");
    //(buzOffT!=null)?(p15.innerHTML     ="<span style='color:#5b5a5a;font-weight: bold'>Buzzer Turned off at </span>:<span  style='font-size:16px'>"+Unix_timestamp((buzOffT.split('(')[1]).split(')')[0],'ymdt')+"</span>"):(p15.innerHTML ="");
    //(deptOccrd!=null)?(p16.innerHTML ="<span style='color:#5b5a5a;font-weight: bold'>Issue Occured Department </span>:<span  style='font-size:16px'>"+deptOccrd+"</span>"):(p16.innerHTML ="");

    accordion.appendChild(rowDIVcontainer);
    accordion.appendChild(detailsDIV);
}

function createPagination(numberOfPages){

    var paginationDiv = document.getElementById("paginationDiv");

    if(paginationDiv.firstChild){
        while (paginationDiv.firstChild){
            paginationDiv.removeChild(paginationDiv.firstChild);
        }
    }

    var ulElement =  document.createElement('ul');
    ulElement.setAttribute("class","pagination");

    for (var i = 0; i < numberOfPages; i++){
            var liElement =  document.createElement('li');
            liElement.setAttribute("class","page-item ");
            liElement.setAttribute("id",i);
            var aElement =  document.createElement('div');
            aElement.setAttribute("class","page-link");
            aElement.innerHTML= i+1 ;
            aElement.setAttribute('onclick',"loadTablePage("+(Number(i)+1)+")");

            liElement.appendChild(aElement);
            ulElement.appendChild(liElement);
    }
    paginationDiv.appendChild(ulElement);
}

function dateCheck(checkingDate,startDate,endDate){

 if(checkingDate<endDate && checkingDate>startDate)
    {
        return(true);
    }
        else return(false);
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

var chartData1;

function drawChart1() {
    
    var dataArray = new Array();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 23:59 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url:url_fillChart1,
        data: { barChart: '1',startDate:startDate,endDate:endDate,plantLocation:plantLocation},
        success: function (feedback) {

            chartData1 = JSON.parse(feedback);
            var a1 = new Array(chartData1.length + 1);
            a1[0] = ["Element", "Frequency", { role: "style" }]
            var x = new Array();
            count = 1;

            chartData1.forEach(function (element) {
                var ele1 = element.machine_machine_id;
                var ele2 = element.count;
                var ele3 = getRandomColor();
                x = [ele1, ele2, ele3];
                a1[count] = x;
                count++;
            });


            dataArray = a1;
        },
        error: function () {
            alert("Error occurred");
        }
    });
    var data = google.visualization.arrayToDataTable(dataArray);
    var view = new google.visualization.DataView(data);

    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        }, 2]);

    var options = {
                    bar: { groupWidth: "95%" },
                    legend: { position: "none" },
                    chartArea: { 'width': '100%', 'height': '60%', 'top': '0' },
                    hAxis: {
                        textStyle: {
                            fontSize: 9
                        }
                    },
                    animation:{
                    duration: 2000,
                    easing: 'out',
                    startup: true
                        }
                };

    var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
    chart.draw(view, options);
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

var chartData1;

function drawChart2() {
    var dataArray1 = new Array();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 23:59 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url:url_fillChart1,
        data: { barChart: '2',startDate:startDate,endDate:endDate,plantLocation:plantLocation},
        success: function (feedback) {

            chartData1 = JSON.parse(feedback);
            var a1 = new Array(chartData1.length + 1);
            a1[0] = ["Element", "Frequency", { role: "style" }]
            var x = new Array();
            count = 1;

            chartData1.forEach(function (element) {
                var ele1 = element.Search_Description;
                var ele2 = element.count;
                var ele3 = getRandomColor();
                x = [ele1, ele2, ele3];
                a1[count] = x;
                count++;
            });

            dataArray1 = a1;
        },
        error: function () {
            alert("Error occurred");
        }
    });
    var data = google.visualization.arrayToDataTable(dataArray1);
    var view = new google.visualization.DataView(data);

    view.setColumns([0, 1,
        {
            calc: "stringify",
            sourceColumn: 1,
            type: "string",
            role: "annotation"
        }, 2]);

    var options = {
        bar: { groupWidth: "95%" },
        legend: { position: "none" },
        chartArea: { 'width': '100%', 'height': '60%', 'top': '0' },
        hAxis: {
            textStyle: {
                fontSize: 9
            },
        animation:{
          duration: 2000,
          easing: 'out',
          startup: true
              }
        }
    };
    var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values1"));
    chart.draw(view, options);
}

function drawChart3() {
    var dataArray2 = new Array();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 23:59 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url:url_fillChart3,
        data: { startDate:startDate,endDate:endDate,plantLocation:plantLocation },
        success: function (feedback) {

            chartData1 = JSON.parse(feedback);
            var a1 = new Array(chartData1.length + 1);
            a1[0] = ["Issue", "Number of Occurrence"]
            var x = new Array();
            count = 1;

            chartData1.forEach(function (element){
                var ele1 = element.issue;
                var ele2 = element.count;
                x = [ele1, ele2];
                a1[count] = x;
                count++;
            });
            dataArray2 = a1;
        },
        error: function () {
            alert("Error occurred");
        }
    });

    var options = {
        title: 'Number of Issues by Type',
        is3D: true,
        chartArea: { 'width': '100%', 'height': '80%', 'top': '0' },
        legend: { position: "bottom" },
        slices: { 0: { color: '#f40000' }, 1: { color: '#ffcc00' }, 2: { color: '#1e63ce' }, 3: { color: '#31bc07' }, 4: { color: '#a80da3' } }
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));

    var data = google.visualization.arrayToDataTable(dataArray2);
    chart.draw(data, options);

    var percent = 0;

    if(dataArray2 != null){
        var handler = setInterval(function(){
            percent += 3;
            data.setValue(0, 1, percent);
            data.setValue(1, 1, 100 - percent);
            chart.draw(data, options);
            if (percent > 74)
                clearInterval(handler);
        }, 2);
    }
}

function createTable() {
    $("#myTable").empty();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 23:59 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url:url_fillChart2,
        data: {startDate:startDate,endDate:endDate,plantLocation:plantLocation },
        success: function (feedback) {

            chartData1 = JSON.parse(feedback);

            chartData1.forEach(function (element) {
                var ele1 = element.issue_date.split('(')[1];
                ele1 = ele1.split(')')[0];

                var ele2 = element.issue;
                var ele3 = element.machine_machine_id;
                var ele4 = element.material_id;
                var ele5 = element.Name;
                var ele6 = element.DateDiff

                if (ele3 == null && ele4 !=null){createRow(Unix_timestamp(ele1,'md')+"</br>(" + (ele6 / 60).toFixed(1) + "Hrs)",ele2,ele4,ele5) }
                if (ele4 == null && ele3 !=null){createRow(Unix_timestamp(ele1,'md')+"</br>(" + (ele6 / 60).toFixed(1) + "Hrs)",ele2,ele3,ele5) }
                if (ele4 == null && ele3 ==null){createRow(Unix_timestamp(ele1,'md')+"</br>(" + (ele6 / 60).toFixed(1) + "Hrs)",ele2,  "",ele5) }
            });
        },
        error: function () {
            alert("Error occurred");
        }
    });
}

function createRow(issue_date, issue, desc, resp_name) {

    var mainDiv = document.getElementById('myTable');
    var tableRow = document.createElement("tr");
    var tableData = document.createElement("td");
    tableData.innerHTML = issue_date;
    var tableData2 = document.createElement("td");
    tableData2.innerHTML = issue;
    var tableData3 = document.createElement("td");
    tableData3.innerHTML = desc;
    var tableData4 = document.createElement("td");
    tableData4.innerHTML = resp_name;
    tableRow.appendChild(tableData);
    tableRow.appendChild(tableData2);
    tableRow.appendChild(tableData3);
    tableRow.appendChild(tableData4);

    mainDiv.appendChild(tableRow);
}

function Unix_timestamp(t,dateType) {

    var unixtimestamp = Number(t);
    var months_arr = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var date = new Date(unixtimestamp);
    var year = date.getFullYear();
    var month = months_arr[date.getMonth()];
    var day = date.getDate();
    var hours = date.getHours();
 
    var minutes = date.getMinutes();

    ( (""+minutes).length<2 )?(minutes="0"+minutes):(minutes=minutes);

    var seconds = date.getSeconds();

    ( (""+seconds).length<2 )?(seconds="0"+seconds):(seconds=seconds);

    if (dateType == "md") { var convdataTime = month + '-' + day; return convdataTime;}
    if (dateType == "ymd") { var convdataTime = year+ '-' + month + '-' + day; return convdataTime; }
    if (dateType == "ymdt") { var convdataTime = year+ '-' + month + '-' + day+ ' ' +hours+ ':' +minutes ; return convdataTime; }
    if (dateType == "ymdts") { var convdataTime = year+ '-' + month + '-' + day+' '+hours+':'+minutes+':'+seconds; return convdataTime; }
}

function filterByDate()
    {
        createTable();
        var elem1 =document.querySelectorAll(".dateGap");
        elem1.forEach(function (i)
                {
                    i.innerHTML ="from "+ (new Date(document.getElementById('datetimepicker1').value+" 23:59 UTC")).toISOString().substring(0,10) +" to "+(new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString().substring(0,10) ;
                });
        google.setOnLoadCallback(drawChart1);
        google.setOnLoadCallback(drawChart2);
        google.setOnLoadCallback(drawChart3);
        numberOfIssues();

    }
/*
$(document).on("click", ".statistic__item", function(e) 
{
        $('#issueSelectBox').val('Machine Breakdown');
        $('#statusSelectBox').val('1');

        $('#issueSelectBox').trigger('change.select2');
        $('#statusSelectBox').trigger('change.select2');

      //  $('#issueSelectBox').select2("val",$('#issueSelectBox option:eq(2)').val());
      //  $('#issueSelectBox').select2().select2();

        document.getElementById('datetimepicker1').value ='Machine Breakdown';
});
*/
function topCardClick(issue_name){

    if(issue_name=='mb'){

        $('#issueSelectBox').val('Machine Breakdown');
        $('#statusSelectBox').val('1');

        $('#issueSelectBox').trigger('change.select2');
        $('#statusSelectBox').trigger('change.select2');
    }

    if(issue_name=='md'){
        $('#issueSelectBox').val('Material Delay').trigger('change');
        $('#statusSelectBox').val('1').trigger('change');
        
        $('#issueSelectBox').trigger('change');
        $('#statusSelectBox').trigger('change');
    }
    if(issue_name=='ti'){
        $('#issueSelectBox').val('Technical Issue').trigger('change');
        $('#statusSelectBox').val('1').trigger('change');
        
        $('#issueSelectBox').trigger('change');
        $('#statusSelectBox').trigger('change');
    }
    if(issue_name=='it'){
        $('#issueSelectBox').val('IT Issue').trigger('change');
        $('#statusSelectBox').val('1').trigger('change');
        
        $('#issueSelectBox').trigger('change');
        $('#statusSelectBox').trigger('change');
    }
    if(issue_name=='qi'){
        $('#issueSelectBox').val('Quality Issue').trigger('change');
        $('#statusSelectBox').val('1').trigger('change');
        
        $('#issueSelectBox').trigger('change');
        $('#statusSelectBox').trigger('change');
    }

}

var flag1 = 1;

function notificationOnOff(issue_occurrence_id){
        var status;

        if (flag1==1)  
        {
            status =0;
            flag1 = 0;
        }

        else if (flag1==0)  
       { 
            status =1;
            flag1 = 1;
        }

        var issue_line_person_id = document.getElementById('hidden_userID').innerHTML;

        $.ajax({
            type: "POST",
            async:false,
            dataType: 'text',
            url:url_notificationOnOff,
            data: { issue_line_person_id:issue_line_person_id,issue_occurrence_id:issue_occurrence_id,status:status},
            success: function (feedback) {
                   
            },
            error: function () {
                alert("Error occurred");
            }
        });
}


function numberOfIssues(){
    var location = $('#plantSelectBox').val().join("','");
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 23:59 UTC")).toISOString();

    var count = 0;

        $.ajax({
            type: "POST",
            dataType: 'text',
            async:false,
            url:url_totalNumberOfIssues,
            data: {startDate:startDate,endDate:endDate,plant:location },
            success: function (feedback) {
                count = feedback;
            },
            error: function () {
                alert("Error occurred");
            }
        });
    
    var ttlIssues = document.getElementById("ttlIssues");
   
    countNew = 0;
    var setIntervalFunc= setInterval(function(){ 
         if(count>=countNew){
                ttlIssues.innerHTML=countNew;
                countNew++;
            }
        else{clearInterval(setIntervalFunc);}
    }, 10);

}

$('#plantSelectBox').select2({
    minimumResultsForSearch: -1,
    allowClear: false
});

$('#plantSelectBox2').select2({
    minimumResultsForSearch: -1,
    allowClear: false
});

$('#lineSelectBox').select2({
    minimumResultsForSearch: -1,
    allowClear: false,
    //placeholder:"Select Line"
});

$('#dptSelectBox').select2({
    minimumResultsForSearch: -1,
    allowClear: false,
    placeholder:"Select Department"
});

$('#statusSelectBox').select2({
    minimumResultsForSearch: -1,
    allowClear: false,
    //placeholder:"Select Status"
});

$('#issueSelectBox').select2({
    minimumResultsForSearch: -1,
    allowClear: false,
});

$(document).ready(function() {
        google.charts.load("current",{packages: ['corechart']});
        google.charts.setOnLoadCallback(drawChart1);
        google.charts.setOnLoadCallback(drawChart2);
        google.charts.setOnLoadCallback(drawChart3);
        
        loadTableData();
        loadTablePage(1);

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
});

 var data_obj = new Array();
 var raw_data = new Array();

function loadTableData(){

        $.ajax({
            type:"POST",
            dataType:'text',
            async:false,
            url:'/Manager/loadIssueList',
            data:{},
            success: function (data)
                {   console.log(data);

                    var dataArray = JSON.parse(data);

                        raw_data = dataArray;

                        dataArray.forEach(function (i) 
                            {

                                var tempArr = new Array();

                                var ele1 = i.issue_date.split('(')[1];
                                ele1 = ele1.split(')')[0];

                                tempArr.push(Unix_timestamp(ele1,'ymd'));
                                tempArr.push(i.issue_occurrence_id);
                                tempArr.push(i.issue);
                                tempArr.push(i.line_name);

                                if (i.issue_satus == '1') { tempArr.push('Unsolved');}
                                if (i.issue_satus == '0') { tempArr.push('Solved'); }
                                tempArr.push(i.location);
                                tempArr.push(i.description);
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
        var Date_select_end = (new Date(document.getElementById('datetimepicker4').value+" 00:00 UTC")).toISOString().substring(0, 10);

        var Plant_select = document.getElementById('plantSelectBox2').value;
        var Issue_select = document.getElementById('issueSelectBox').value
        var Department_select = document.getElementById('dptSelectBox').value;
        var Line_select =   document.getElementById('lineSelectBox').value;
        var Status_select  = document.getElementById('statusSelectBox').value;

        var o= ""; var m= ""; var b= ""; var nq="";

        (Issue_select == "") ? (nq="*0"):(nq="*1"); //Issue_select
        (Line_select  == "") ? (m="*0"):(m="*1");  //Line_select
        (Status_select== "") ? (o="*0"):(o="*1") ; //Status_select
        (Department_select == "") ? (true):(true); //Department_select
        (Plant_select  == "") ? (b="*0"):(b="*1");  //Plant_select

        var comparing_string = b+nq+m+o;

        var condition = false;

        raw_data.forEach(function (i) 
                {
                        var countx=0;
                        var p =i.location.trim();
                        var is =i.issue.trim();
                        var l =i.line_name.trim();
                        var s =i.issue_satus.trim();

                        var dateVar = i.issue_date.split('(')[1];
                        dateVar = dateVar.split(')')[0];

                        var dateCheck_result = dateCheck(new Date(Unix_timestamp(dateVar,'ymd')),
                                                         new Date(document.getElementById('datetimepicker3').value+" 00:00 UTC"),
                                                         new Date(document.getElementById('datetimepicker4').value+" 00:00 UTC")
                                                        );

                        var var_array = ["*0","*1"];
                            OuterLoop:
                            for(var x =0;x<var_array.length;x++){
                                for(var y=0;y<var_array.length;y++){
                                    for(var z=0;z<var_array.length;z++){
                                        for(var q=0;q<var_array.length;q++){
                                            for(var u=0;u<var_array.length;u++){
                                                for(var v=0;v<var_array.length;v++){ 

                                                   var binary_code = var_array[v]+var_array[u]+var_array[q]+var_array[y];

                                                   if(comparing_string == binary_code)
                                                        {
                                                            console.log(binary_code);
                                                            
                                                             o= (s===Status_select);
                                                             m= (l===Line_select); 
                                                             b= (p===Plant_select);
                                                             nq= (is===Issue_select);
          
                                                            var split_str = binary_code.split("*");

                                                            (split_str[1] == "0") ? (b=true):(true);   //Issue_select
                                                            (split_str[2] == "0") ? (nq=true):(true);  //Line_select
                                                            (split_str[3] == "0") ? (m=true):(true);   //Status_select
                                                            (split_str[4] == "0") ? (o=true):(true);   //Plant_select

                                                            condition =  ( b && nq && m && o && dateCheck_result);//b+nq+m+o

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

                                console.log("Plant_select :"+b+"Issue_select :"+nq+"Line_select:"+m+"Status_select:"+o);
                                
                                tempArr.push(d);
                                tempArr.push(i.issue_occurrence_id);
                                tempArr.push(i.issue);
                                tempArr.push(i.line_name);

                                if (i.issue_satus == '1') { tempArr.push('Unsolved');}
                                if (i.issue_satus == '0') { tempArr.push('Solved'); }
                                tempArr.push(i.location);
                                tempArr.push(i.description);
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
}

function loadAccordionTable(obj)
{
        var accordion  =  document.getElementById('accordion');
        var childElements = accordion.childNodes;

        $("#accordion").empty();

        for(var i=0;i<obj.length;i++){
                createAccordionLine(accordion,obj[i][0],obj[i][1],obj[i][2],obj[i][5],obj[i][3],obj[i][4],obj[i][6]);
        }

        $( function() {
            $( "#accordion" ).accordion();
            $( "#accordion" ).accordion("refresh");
        });

}

function createAccordionLine(accordion,date,id,issue,plant,line,status,desc){

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
    p1.innerHTML="More Details";
    detailsDIV.appendChild(p1);

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
            liElement.setAttribute("class","page-item");
            liElement.setAttribute("id",i);

            var aElement =  document.createElement('div');
            aElement.setAttribute("class","page-link");
            aElement.innerHTML=i+1;
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
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");
    console.log(plantLocation);

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url: "/Manager/fillChart1",
        data: { barChart: '1',startDate:startDate,endDate:endDate,plantLocation:plantLocation},
        success: function (feedback) {
            chartData1 = JSON.parse(feedback);
            var a1 = new Array(chartData1.length + 1);
            a1[0] = ["Element", "Density", { role: "style" }]
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
    console.log("drawChart1");
    console.log(dataArray);
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
                fontSize: 9 // or the number you want
            }
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
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url: "/Manager/fillChart1",
        data: { barChart: '2',startDate:startDate,endDate:endDate,plantLocation:plantLocation},
        success: function (feedback) {
            chartData1 = JSON.parse(feedback);
            var a1 = new Array(chartData1.length + 1);
            a1[0] = ["Element", "Density", { role: "style" }]
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
    console.log("drawChart2");
    console.log(dataArray1);
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
                fontSize: 9 // or the number you want
            }
        }
    };
    var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values1"));
    chart.draw(view, options);
}

function drawChart3() {
    var dataArray2 = new Array();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url: "/Manager/fillChart3",
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
}

function createTable() {
    $("#myTable").empty();
    var startDate = (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString();
    var endDate   = (new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString();
    var plantLocation = $('#plantSelectBox').val().join("','");

    $.ajax({
        type: "POST",
        async: false,
        dataType: 'text',
        url: "/Manager/fillChart2",
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

                if (ele3 == null && ele4 != null) { createRow(Unix_timestamp(ele1,'md') + "</br>(" + (ele6 / 60).toFixed(1) + "Hrs)", ele2, ele4, ele5) }
                if (ele4 == null && ele3 != null) { createRow(Unix_timestamp(ele1, 'md') + "</br>(" + (ele6 / 60).toFixed(1) + "Hrs)", ele2, ele3, ele5) }
                if (ele4 == null && ele3 == null) { createRow(Unix_timestamp(ele1, 'md') + "</br>(" + (ele6 / 60).toFixed(1) + "Hrs)", ele2, "", ele5) }
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
    var minutes = "0" + date.getMinutes();
    var seconds = "0" + date.getSeconds();
    if (dateType == "md") { var convdataTime = month + '-' + day; return convdataTime;}
    if (dateType == "ymd") { var convdataTime = year+ '-' + month + '-' + day; return convdataTime; }
}

function filterByDate()
    {
        createTable();
        var elem1 =document.querySelectorAll(".dateGap");
        elem1.forEach(function (i){
                        i.innerHTML ="from "+ (new Date(document.getElementById('datetimepicker1').value+" 00:00 UTC")).toISOString().substring(0,10) +" to "+(new Date(document.getElementById('datetimepicker2').value+" 00:00 UTC")).toISOString().substring(0,10) ;
                });
        google.charts.setOnLoadCallback(drawChart1);
        google.charts.setOnLoadCallback(drawChart2);
        google.charts.setOnLoadCallback(drawChart3);

    }





var set_functions = {
                func0: function (plant_loc) {
                    getVoice("command not found");
                },
                func1: function (plant_loc) {//material
                    search_issue_data("Material Delay",plant_loc);
                },
                func2: function (plant_loc) {//IT
                    search_issue_data("IT Issue",plant_loc);
                },
                func3: function (plant_loc) {//Quality
                    search_issue_data("Quality Issue",plant_loc);
                },
                func4: function (plant_loc) {//Machine
                    search_issue_data("Machine Breakdown",plant_loc);
                },
                func5: function (plant_loc) {//Technical
                    search_issue_data("Technical Issue",plant_loc);
                },
                func6: function (plant_loc) {//All
                    search_issue_data("All",plant_loc);
                }
};

function search_issue_data(issue,plant_loc){ 

        //search issue data in previously recieved JSON data set
            /*
                Name: "bbb"
                buzzer_off_by: "bbb"
                buzzer_off_time: "/Date(1549001760000)/"
                commented_date: "/Date(1549001788000)/"
                dep_occured: 1
                department: "Maintenance"
                description: "xxxxxxxxxxxxxxx"
                issue: "Machine Breakdown"
                issue_date: "/Date(1549001280000)/"
                issue_issue_ID: 1
                issue_occurrence_id: 5540
                issue_satus: "0"
                line_line_id: 1
                line_name: "Potted"
                location: "KOG"
                machine_machine_id: "BigRed9"
                material_id: null
            });
            */

        var dataSet = raw_data; //Raw data is taken from a script in Index.cshtml

        var unsolved_issues = new Array();
        var voice_data_set= new Array();

        dataSet.forEach(function(ele){
            if(ele.issue_satus == "1"){
                unsolved_issues.push(ele);
            }
        });
        
         if(plant_loc != ""){

                unsolved_issues.forEach(function(ele){
                    if(ele.issue == issue && ele.location == plant_loc){
                        voice_data_set.push(ele);
                    }
                });

               if(voice_data_set.length>0){ voice_response(true,voice_data_set,true);}
                else { var x = "";
                             if(issue!="All"){ x= "No " +issue+"s in"+ (plant_loc=="KTY"?"Kaatunaayaka":"Koggala") +" plant";}
                              else{ x= "No  issues in"+ (plant_loc=="KTY"?"Kaatunaayaka":"Koggala") +" plant";}
                             getVoice(x,true);
                    }
            }
        else{
                if(issue!='All'){
                        var issues_koggala = new Array();
                        var issues_kty = new Array();

                        unsolved_issues.forEach(function(ele){
                            if(ele.location == "KOG" && ele.issue == issue){
                                issues_koggala.push(ele);
                            }
                            if (ele.location == "KTY" && ele.issue == issue){
                                issues_kty.push(ele)
                            }
                        });

                            if(issues_koggala.length>0){ 
                               voice_response(true,issues_koggala,false);///////////////////////////////////////////
                             }

                            if(issues_kty.length>0){ 
                                   getVoice(" and ",false);
                                   voice_response(false,issues_kty,true);
                                    
                            }

                            if(issues_kty.length==0 && issues_koggala.length==0){

                                        var x = "No " +issue+"s ";
                                        getVoice(x,true);
                            }
                    }

                else{
                        
                        getVoice("There are "+unsolved_issues.length+"unsolved issues in both plants",true);
                        getVoice("Do you want me to read them out?",true);
                    }


                try {
                    speechRecognizer.start();///////////////////////////////////////////////////////
                }
                catch(err) {
                    console.log(err)
                }
               

            }
    
   }

function voice_response(ap,data,start_var)
    {  
        var loc = (data[0].location=="KTY"?"Kaatunaayaka":"Koggala");
        var iss = data[0].issue;
        var voice_line_1 = "";

        if(data.length>1)
        {voice_line_1 = (ap==true?"there are ":"")+data.length+" "+iss+"s in "+loc}
        if(data.length<=1)
        {voice_line_1 = (ap==true?"there is ":"")+ data.length+" "+iss+ " in "+loc}

        if(voice_line_1!=""){ getVoice(voice_line_1,start_var); }
       
        // what are the issues today?
        // There are 3 machine breakdowns, 2 Material delays in koggala and 2 technical issues in katunayaka
        // There is an usolved material delay for two days in potted line in katunayaka

        try {
            speechRecognizer.start();///////////////////////////////////////////////////////
        }
        catch(err) {
            console.log(err)
        }
    }

function voice_response_2(ap,data)
    {  
        var loc = (data[0].location=="KTY"?"Kaatunaayaka":"Koggala");
        var iss = data[0].issue;
        var voice_line_1 = "";


        if(data.length>1)
        {voice_line_1 = (ap==true?"there are ":"")+data.length+" "+iss+"s in "+loc}
        if(data.length<=1)
        {voice_line_1 = (ap==true?"there is ":"")+ data.length+" "+iss+ " in "+loc}

        if(voice_line_1!=""){ getVoice(voice_line_1,true); }
       
    }
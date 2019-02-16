

var set_functions = {
                func0: function (lvl2_command_) {
                    getVoice("command not found");
                },
                func1: function (lvl2_command_) {//material
                    search_issue_data("Material Delay",lvl2_command_);
                },
                func2: function (lvl2_command_) {//IT
                    search_issue_data("IT Issue",lvl2_command_);
                },
                func3: function (lvl2_command_) {//Quality
                    search_issue_data("Quality Issue",lvl2_command_);
                },
                func4: function (lvl2_command_) {//Machine
                    search_issue_data("Machine Breakdown",lvl2_command_);
                },
                func5: function (lvl2_command_) {//Technical
                    search_issue_data("Technical Issue",lvl2_command_);
                }
};

function search_issue_data(issue,lvl2_command_){ //search issue data in previously recieved JSON data set ""
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

        var dataSet = raw_data;

        var unsolved_issues = new Array();
        var voice_data_set= new Array();

        dataSet.forEach(function(ele){
            if(ele.issue_satus == "1"){
                unsolved_issues.push(ele);
            }
        });
        
         if(lvl2_command_ != ""){

                unsolved_issues.forEach(function(ele){
                    if(ele.issue == issue && ele.location == lvl2_command_){
                        voice_data_set.push(ele);
                    }
                });

               if(voice_data_set.length>0){ voice_response(true,voice_data_set);}
                else {
                            var x = "No " +issue+"s in"+ (lvl2_command_=="KTY"?"Kaatunaayaka":"Koggala") +" plant";
                             getVoice(x);
                    }
            }
        else{

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

                 
                   if(issues_koggala.length>0){voice_response(true,issues_koggala);}
                   if(issues_kty.length>0){ getVoice(" and "); voice_response(false,issues_kty);}
                   if(issues_kty.length==0 && issues_koggala.length==0){
                             var x = "No " +issue+"s ";
                             getVoice(x);
                    }
            }
    
   }

function voice_response(ap,data)
    {  
        var loc = (data[0].location=="KTY"?"Kaatunaayaka":"Koggala");
        var iss = data[0].issue;
        var voice_line_1 = "";


        if(data.length>1)
        {voice_line_1 = (ap==true?"there are ":"")+data.length+" "+iss+"s in "+loc}
        if(data.length<=1)
        {voice_line_1 = (ap==true?"there is ":"")+ data.length+" "+iss+ " in "+loc}

        if(voice_line_1!=""){ getVoice(voice_line_1); }
       
        //what are the issues today?
        //There are 3 machine breakdowns, 2 Material delays in koggala and 2 technical issues in katunayaka
        //There is an usolved material delay for two days in potted line in katunayaka
    }

using IssueManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Controllers
{
    public class LocationAdapter
    {
        //-------------------------------------------
        //KT-ASM KT - ASSEMBLY - SM
        //KT-ACC KT - ASSEMBLY - ACCESSORIES
        //KT-APT KT - ASSEMBLY - POTTED
        //KT-ARC KT - ASSEMBLY - ROCKER
        //KT-AWL KT - ASSEMBLY - WELDED
        //KT-HTR KT - HEAT TREATMENT
        //KT-MCS KT - MACHINE SHOP
        //KT-MD2 KT - ASSEMBLY - MEDICAL DEVICE 2
        //KT-GAG KT - GAGING
        //KT-MCS KT - MACHINE SHOP

        //--------------------------------------
        //KG-APB KG - ASSEMBLY - PB
        //KG-ASP KG - ASSEMBLY - SP
        //KT-ASP KT - ASSEMBLY - SP 
        //KG-AMD1 KG-ASSEMBLY-MEDICAL DEVICE

        //KG-GAG  KG - GAGING SECTION

        //KG-MCS KG - MACHINE SHOP

        //--------------RS1 RS2----------------------
        //KG-AFL KG - ASSEMBLY - FAST LANE
        //KG-FL5 KG - ASSEMBLY - FAST LANE 5
        //KG-ASV KG - ASSEMBLY - SAVANNAH

        line ln = new line();
        public LocationAdapter(int ims_Location) {
            using (issue_management_systemEntities1 db = new issue_management_systemEntities1())
            {
                ln = db.lines.Where(x => x.line_id == ims_Location).SingleOrDefault();
            }
        }

        public String get_NAV_Location()
        {
             return (ln.location);
        }

        public String get_LineName()
        {
            return (ln.line_name);
        }

        public int get_departmentID()
        {
            return (ln.department_id);
        }
    }
}
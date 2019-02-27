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
        //KG-AMT KG - ASSEMBLY - MOTOROLA
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

        //-----------------MD1-----------------------
        //KG-AMD1 KG - ASSEMBLY - MEDICAL DEVICE


        public String get_NAV_Location(String ims_Location ) {


            switch(ims_Location)
            {   


                default:
                    break;
            }
            return ("test Location");
        }
    }
}
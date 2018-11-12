using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueManagementSystem.Models
{
    [Table("FLINTEC$Item")] //<-- this line uses to map FLINTEC_Item class to FLINTEC_Item table in db
                            //it is important when table name and elated class name are different
    public class FLINTEC_Item
    {

        [Key]
        public string No_ { get; set; }

        [Column("No_ 2")]
        public string No__2 { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Search Description")]
        public string Search_Description { get; set; }


    }

    public class MaterialList
    {
        static FLINTEC_Item_dbContext materialContext = new FLINTEC_Item_dbContext();
        List<FLINTEC_Item> mList = materialContext.FLINTEC_Items.ToList();
        
    }
}
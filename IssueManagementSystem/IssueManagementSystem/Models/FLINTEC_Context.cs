using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IssueManagementSystem.Models
{
    public class FLINTEC_Context : DbContext
    {
        public FLINTEC_Context()
        : base("name=FLINTEC_dbContext")
        {
        }
        //Prod_ Order Line
        public virtual DbSet<FLINTEC_Item> FLINTEC_Items { get; set; }
    }

    public class FLINTEC_Prod_Order_Line_Context : DbContext
    {
        public FLINTEC_Prod_Order_Line_Context()
        : base("name=FLINTEC_dbContext")
        {
        }
        public virtual DbSet<FLINTEC_Prod_Order_Line> FLINTEC_Prod_Order_Line { get; set; }
    }

    public class FLINTEC_Prod_Order_Component_Context : DbContext
    {
        public FLINTEC_Prod_Order_Component_Context()
        : base("name=FLINTEC_dbContext")
        {
        }
        public virtual DbSet<FLINTEC_Prod_Order_Component> FLINTEC_Prod_Order_Component { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IssueManagementSystem.Models
{

    public class FLINTEC_Item_dbContext : DbContext
    {

        public FLINTEC_Item_dbContext()
        : base("name=FLINTEC_Item_dbContext")
        {
        }

        public virtual DbSet<FLINTEC_Item> FLINTEC_Items { get; set; }
    }

    public class combinedModel
    {
        public FLINTEC_Item FLINTEC_Item { get; set; }
        public issue_occurrence issue_occurrence { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IssueManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {   [Required(ErrorMessage ="Plese Enter Your UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Plese Enter Your User PassWord")]
        public string PassWord { get; set; }

        public int emp_id { get; set; }
       public String LoginErrorMessage { get; set; }
    }
}

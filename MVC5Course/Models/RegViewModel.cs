using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public enum MemberLevel
    {
        User,
        Admin
    }

    public class RegViewModel
    {
        public string UserID { get; set; }
        public string UserPW { get; set; }

        [UIHint("Enum-radio")]
        public MemberLevel Level { get; set; }
    }
}
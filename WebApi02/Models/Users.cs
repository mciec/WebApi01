using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi02.Models
{
    [Table("dt_users")]
    public class Users
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }

    }
}

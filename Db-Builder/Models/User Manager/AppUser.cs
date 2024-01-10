using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Db_Builder.Models.User_Manager
{
    [Table(name: "User", Schema = "Security")]
    public class AppUser :IdentityUser
    {

        public  string? Name { get; set; }

        public string ? User_Image { set; get;  }
        [ForeignKey("DegreeState")]
        public int ? DegreeStateId { set; get;  }

        public DegreeState? DegreeState { set; get; }

        [ForeignKey("Supervisior")]
        public string? SupervisiorId { set; get; }
        

        public AppUser? Supervisior { set; get; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db_Builder.Models.User_Manager
{
    [Table(name: "DegreeState", Schema = "Security")]

    public class DegreeState :BaseEntity
    {
        
        public string State {  get; set; }
    }
}

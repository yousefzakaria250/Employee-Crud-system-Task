using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Dtos
{
    public class UpdateUserDto
    {
        public string Id { set; get;  }
        public string Name { set; get; }

        public string Email { set; get; }

        public string PhoneNumber { set; get; }

        public string User_Image { set; get; }
        public int DegreeStateId { set; get; }
    }
}

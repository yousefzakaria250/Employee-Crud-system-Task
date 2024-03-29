﻿using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Dtos
{
    public class GetUserDto
    {
        public string Id { set; get;  }
        public string? Name { set; get; }

        public string? Email { set; get; }

        public string? PhoneNumber { set; get; }

        public string? User_Image { set; get; }
        public string State { get; set; }
    }
}

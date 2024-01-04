using Data_Access_Layer.Repositories;
using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Specification.Employee_Specs
{
    public class EmployeeWithFiltersForCountSpecs : BaseSpecification<AppUser>

    {
        public EmployeeWithFiltersForCountSpecs(EmployeeSpecParams specParams) :
               base(U =>
            (string.IsNullOrEmpty(specParams.Search) || U.UserName.ToLower().Contains(specParams.Search))) 
             
        { }

    }
}

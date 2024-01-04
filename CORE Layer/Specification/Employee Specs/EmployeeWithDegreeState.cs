using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Specification.Employee_Specs
{
    public class EmployeeWithDegreeState :BaseSpecification<AppUser>
    {
        //this Constructor is used to get All Jobs
        public EmployeeWithDegreeState(EmployeeSpecParams specParams) :
               base(U =>
            (string.IsNullOrEmpty(specParams.Search) || U.UserName.ToLower().Contains(specParams.Search))  
            )
        {
            AddOrderBy(P => P.UserName);
            AddInclude(U => U.DegreeState); //Default
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }


        //this Constructor is used to get a Specific Job with id 
        public EmployeeWithDegreeState(string id) : base(P => P.Id == id)
        {
            AddInclude(U => U.DegreeState);
        }




    }
}

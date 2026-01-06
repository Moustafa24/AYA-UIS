using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Info_Module;

namespace Presistence.Specifications
{
    public class DepartmentFeeWithIncludesSpec :BaseSpecification<DepartmentFee , int>
    {
        public DepartmentFeeWithIncludesSpec()
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.GradeYear);
        }

        public DepartmentFeeWithIncludesSpec(string departmentName, string gradeYear)
        {
            // للتحقق على مستوى قاعدة البيانات
            Criteria = df => df.Department.Name.ToLower() == departmentName.ToLower()
                          && df.GradeYear.Name.ToLower() == gradeYear.ToLower();

            AddInclude(x => x.Department);
            AddInclude(x => x.GradeYear);
        }



    }
}

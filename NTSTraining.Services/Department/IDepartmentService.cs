using NTSCommon.Models;
using NTSTraining.Models.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.Department
{
    public interface IDepartmentService
    {
        Task<SearchBaseResultModel<DepartmentSearchModel>> searchDepartment(DepartmentSearchModel modelSearch);
        Task createDepartment(DepartmentModel model);
        Task updateDepartment(string id, DepartmentModel model);
        Task<List<DepartmentSearchModel>> getAllDepartment();
        Task<DepartmentSearchModel> getDepartmentById(string id);
        Task deleteDepartment(string id);
    }
}

using NTSCommon.Models;
using NTSTraining.Models.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.Employee
{
    public interface IEmployeeService
    {
        Task<SearchBaseResultModel<EmployeeSearchModel>> searchEmployee(EmployeeSearchModel modelSearch);
        Task<EmployeeModel> getEmployeeById(string id);
        Task createEmployee(EmployeeModel model);
        Task updateEmployee(string id, EmployeeModel model);
        Task deleteEmployee(string id);
    }
}

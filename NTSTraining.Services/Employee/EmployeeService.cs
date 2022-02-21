using Microsoft.EntityFrameworkCore;
using NTS.Common;
using NTS.Common.Resource;
using NTSCommon.Models;
using NTSTraining.Models.Entities;
using NTSTraining.Models.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly NTSTrainingContext sqlContext;
        public EmployeeService(NTSTrainingContext _sqlContext)
        {
            this.sqlContext = _sqlContext;
        }
        public async Task createEmployee(EmployeeModel model)
        {
            var questionType = sqlContext.NhanViens.FirstOrDefault(c => c.Email.ToLower().Equals(model.Email.ToLower()));
            if (questionType != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0031, TextResourceKey.Nhanvien);
            }

            Models.Entities.NhanVien entity = new Models.Entities.NhanVien()
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                HoVaTen = model.HoVaTen,
                GioiTinh = model.GioiTinh,
                SoDienThoai =model.SoDienThoai,
                Cmnd = model.Cmnd,
                PhongBanId= model.PhongBanId,
            };
            sqlContext.NhanViens.Add(entity);
            sqlContext.SaveChanges();
        }

        public async Task deleteEmployee(string id)
        {
            var employee = sqlContext.NhanViens.FirstOrDefault(t => t.Id.Equals(id));
            if (employee == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Nhanvien);
            }
            sqlContext.NhanViens.Remove(employee);
            sqlContext.SaveChanges();
        }

        public async Task<EmployeeModel> getEmployeeById(string id)
        {
            var info = (from model in sqlContext.NhanViens.AsNoTracking()
                        where model.Id.Equals(id)
                        select new EmployeeModel
                        {
                            Id = model.Id,
                            Email = model.Email,
                            HoVaTen = model.HoVaTen,
                            GioiTinh = model.GioiTinh,
                            SoDienThoai = model.SoDienThoai,
                            Cmnd = model.Cmnd,
                            PhongBanId = model.PhongBanId,
                            TenPhongban =model.PhongBan.TenPhongBan
                        }).FirstOrDefault();

            if (info == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Nhanvien);
            }
            return info;
        }

        public async Task<SearchBaseResultModel<EmployeeSearchModel>> searchEmployee(EmployeeSearchModel modelSearch)
        {
            SearchBaseResultModel<EmployeeSearchModel> searchResult = new SearchBaseResultModel<EmployeeSearchModel>();
            var data = (from model in sqlContext.NhanViens.AsNoTracking()
                        select new EmployeeSearchModel
                        {
                            Id = model.Id,
                            Email = model.Email,
                            HoVaTen = model.HoVaTen,
                            GioiTinh = model.GioiTinh,
                            SoDienThoai = model.SoDienThoai,
                            Cmnd = model.Cmnd,
                            PhongBanId = model.PhongBanId,
                            TenPhongban = model.PhongBan.TenPhongBan

                        }).AsQueryable();

            if (!string.IsNullOrEmpty(modelSearch.HoVaTen))
            {
                data = data.Where(r => r.HoVaTen.ToUpper().Contains(modelSearch.HoVaTen.ToUpper()));
            }
            if (!string.IsNullOrEmpty(modelSearch.PhongBanId))
            {
                data = data.Where(r => r.PhongBanId.Equals(modelSearch.PhongBanId));
            }
            searchResult.DataResults = data.OrderBy(s => s.HoVaTen).Skip((modelSearch.PageNumber - 1) * modelSearch.PageSize).Take(modelSearch.PageSize).ToList();
            searchResult.TotalItems = data.Count();
            return searchResult;
        }

        public async Task updateEmployee(string id, EmployeeModel model)
        {
            var employee = sqlContext.NhanViens.FirstOrDefault(t => t.Id.Equals(id));
            if (employee == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.Nhanvien);
            }
            if (employee.Email.ToLower() != model.Email.ToLower())
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0031, TextResourceKey.Nhanvien);
            }
            if (model.HoVaTen == null || model.Email == null || model.GioiTinh == null || model.SoDienThoai == null || model.Cmnd == null || model.PhongBanId == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0032, TextResourceKey.Nhanvien);
            }
            employee.HoVaTen = model.HoVaTen;
            employee.Email = model.Email;
            employee.GioiTinh = model.GioiTinh;
            employee.SoDienThoai = model.SoDienThoai;
            employee.Cmnd = model.Cmnd;
            employee.PhongBanId = model.PhongBanId;
            sqlContext.SaveChanges();
        }
    }
}

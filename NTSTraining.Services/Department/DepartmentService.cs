using Microsoft.EntityFrameworkCore;
using NTS.Common;
using NTS.Common.Resource;
using NTSCommon.Models;
using NTSTraining.Models.Entities;
using NTSTraining.Models.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly NTSTrainingContext sqlContext;

        public DepartmentService(NTSTrainingContext _sqlContext)
        {
            this.sqlContext = _sqlContext;
        }

        public async Task createDepartment(DepartmentModel model)
        {
            var department = sqlContext.PhongBans.FirstOrDefault(d => d.TenPhongBan.ToLower().Equals(model.TenPhongBan.ToLower()));
            if(department != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.PhongBan);
            }
            Models.Entities.PhongBan entity = new Models.Entities.PhongBan()
            {
                Id = Guid.NewGuid().ToString(),
                TenPhongBan = model.TenPhongBan,
            };
            sqlContext.PhongBans.Add(entity);
            sqlContext.SaveChanges();
        }

        public async Task deleteDepartment(string id)
        {
            var department = sqlContext.PhongBans.FirstOrDefault(t => t.Id.Equals(id));
            if (department == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.PhongBan);
            }
            var check = sqlContext.NhanViens.Where(s => s.PhongBanId.Equals(id)).Count();
            if (check > 0)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0004, TextResourceKey.PhongBan);
            }
            sqlContext.PhongBans.Remove(department);
            sqlContext.SaveChanges();
        }

        public async Task<List<DepartmentSearchModel>> getAllDepartment()
        {
            var departments = (from d in sqlContext.PhongBans.AsNoTracking()
                               select new DepartmentSearchModel
                               {
                                   Id = d.Id,
                                   TenPhongBan = d.TenPhongBan,
                               }).OrderBy(d => d.TenPhongBan).ToList();
            if(departments == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.PhongBan);
            }

            return departments;
        }

        public async Task<DepartmentSearchModel> getDepartmentById(string id)
        {
            var department = (from d in sqlContext.PhongBans.AsNoTracking()
                              where d.Id.Equals(id)
                              select new DepartmentSearchModel
                              {
                                  Id = d.Id,
                                  TenPhongBan = d.TenPhongBan,
                              }).FirstOrDefault();
            if(department == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.PhongBan);
            }
            return department;
        }

        public async Task<SearchBaseResultModel<DepartmentSearchModel>> searchDepartment(DepartmentSearchModel modelSearch)
        {
            SearchBaseResultModel<DepartmentSearchModel> searchResult = new SearchBaseResultModel<DepartmentSearchModel>();
            var data = (from a in sqlContext.PhongBans.AsNoTracking()
                        select new DepartmentSearchModel
                        {
                            Id = a.Id,
                            TenPhongBan = a.TenPhongBan,
                        }).AsQueryable();

            if (!string.IsNullOrEmpty(modelSearch.TenPhongBan)) 
            {
                data = data.Where(r => r.TenPhongBan.ToUpper().Contains(modelSearch.TenPhongBan.ToUpper()));
            }
            searchResult.DataResults = data.OrderBy(s => s.TenPhongBan).Skip((modelSearch.PageNumber - 1) * modelSearch.PageSize).Take(modelSearch.PageSize).ToList();
            searchResult.TotalItems = data.Count();
            return searchResult;
        }

        public async Task updateDepartment(string id, DepartmentModel model)
        {
            var department = sqlContext.PhongBans.FirstOrDefault(t => t.Id.Equals(id));
            if (department == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.PhongBan);
            }
            var nameExist = sqlContext.PhongBans.AsNoTracking().FirstOrDefault(o => !o.Id.Equals(id) && o.TenPhongBan.ToLower().Equals(model.TenPhongBan.ToLower()));
            if (nameExist != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.PhongBan);
            }

            department.TenPhongBan = model.TenPhongBan;
            sqlContext.SaveChanges();
        }
    }
}

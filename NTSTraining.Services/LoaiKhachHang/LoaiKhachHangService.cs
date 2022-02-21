using Microsoft.EntityFrameworkCore;
using NTS.Common;
using NTS.Common.Resource;
using NTSCommon.Models;
using NTSTraining.Models.Entities;
using NTSTraining.Models.Models.LoaiKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.LoaiKhachHang
{
    public class LoaiKhachHangService: ILoaiKhachHangService
    {
        private readonly NTSTrainingContext sqlContext;

        public LoaiKhachHangService(NTSTrainingContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        /// <summary>
        /// Tìm kiếm loại khách hàng
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<SearchBaseResultModel<LoaiKhachHangSearchResultModel>> SearchLoaiKhachHangAsync(LoaiKhachHangSearchModel searchModel)
        {
            SearchBaseResultModel<LoaiKhachHangSearchResultModel> searchResult = new SearchBaseResultModel<LoaiKhachHangSearchResultModel>();
            var data = (from a in sqlContext.LoaiKhachHangs.AsNoTracking()
                        select new LoaiKhachHangSearchResultModel
                        {
                            Id = a.Id,
                            TenLoaiKhachHang = a.TenLoaiKhachHang,
                        }).AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.TenLoaiKhachHang))
            {
                data = data.Where(r => r.TenLoaiKhachHang.ToUpper().Contains(searchModel.TenLoaiKhachHang.ToUpper()));
            }
            searchResult.DataResults = data.OrderBy(s => s.TenLoaiKhachHang).Skip((searchModel.PageNumber - 1) * searchModel.PageSize).Take(searchModel.PageSize).ToList();
            searchResult.TotalItems = data.Count();
            return searchResult;
        }

        /// <summary>
        /// Thêm mới loại khách hàng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateLoaiKhachHangAsync(LoaiKhachHangCreateModel model)
        {
            var questionType = sqlContext.LoaiKhachHangs.FirstOrDefault(u => u.TenLoaiKhachHang.ToLower().Equals(model.TenLoaiKhachHang.ToLower()));
            if (questionType != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.LoaiKhachHang);
            }

            Models.Entities.LoaiKhachHang entity = new Models.Entities.LoaiKhachHang()
            {
                Id = Guid.NewGuid().ToString(),
                TenLoaiKhachHang = model.TenLoaiKhachHang,
            };
            sqlContext.LoaiKhachHangs.Add(entity);
            sqlContext.SaveChanges();

        }

        /// <summary>
        /// Xóa loại khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteLoaiKhachHangByIdAsync(string id)
        {
            var loaiKhachHang = sqlContext.LoaiKhachHangs.FirstOrDefault(t => t.Id.Equals(id));
            if (loaiKhachHang == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            var check = sqlContext.KhachHangs.Where(s => s.LoaiKhachHangId.Equals(id)).Count();
            if (check > 0)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0004, TextResourceKey.LoaiKhachHang);
            }
            sqlContext.LoaiKhachHangs.Remove(loaiKhachHang);
            sqlContext.SaveChanges();
        }

        /// <summary>
        /// Lấy thông tin loại khách hàng theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<LoaiKhachHangModel> GetLoaiKhachHangByIdAsync(string id)
        {
            var info = (from a in sqlContext.LoaiKhachHangs.AsNoTracking()
                        where a.Id.Equals(id)
                        select new LoaiKhachHangModel
                        {
                            Id = a.Id,
                            TenLoaiKhachHang = a.TenLoaiKhachHang,
                        }).FirstOrDefault();

            if (info == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            return info;
        }

        /// <summary>
        /// Cập nhập loại câu hỏi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codeTypeModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task UpdateLoaiKhachHangAsync(string id, LoaiKhachHangModel model)
        {
            var loaiKhachHang = sqlContext.LoaiKhachHangs.FirstOrDefault(t => t.Id.Equals(id));
            if (loaiKhachHang == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            var nameExist = sqlContext.LoaiKhachHangs.AsNoTracking().FirstOrDefault(o => !o.Id.Equals(id) && o.TenLoaiKhachHang.ToLower().Equals(model.TenLoaiKhachHang.ToLower()));
            if (nameExist != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0002, TextResourceKey.LoaiKhachHang);
            }

            loaiKhachHang.TenLoaiKhachHang = model.TenLoaiKhachHang;
            sqlContext.SaveChanges();
        }

        public async Task<List<LoaiKhachHangModel>> getAllLoaiKhachhang()
        {
            var info = (from a in sqlContext.LoaiKhachHangs.AsNoTracking()
                        select new LoaiKhachHangModel
                        {
                            Id = a.Id,
                            TenLoaiKhachHang = a.TenLoaiKhachHang,
                        }).OrderBy(a => a.TenLoaiKhachHang).ToList();

            if (info == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            return info;
        }
    }
}

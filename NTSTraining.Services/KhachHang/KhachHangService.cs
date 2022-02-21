using Microsoft.EntityFrameworkCore;
using NTS.Common;
using NTS.Common.Resource;
using NTSCommon.Models;
using NTSTraining.Models.Entities;
using NTSTraining.Models.Models.KhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.KhachHang
{
    public class KhachHangService : IKhachHangService
    {
        private readonly NTSTrainingContext sqlContext;
        public KhachHangService(NTSTrainingContext _sqlContext)
        {
            sqlContext = _sqlContext;
        }
        public async Task CreateKhachHangAsync(KhachHangCreateModel model)
        {
            var questionType = sqlContext.KhachHangs.FirstOrDefault(c => c.Email.ToLower().Equals(model.Email.ToLower()));
            if (questionType != null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0031, TextResourceKey.KhachHang);
            }

            Models.Entities.KhachHang entity = new Models.Entities.KhachHang()
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                GhiChu = model.GhiChu,
                DiaChi = model.DiaChi,
                SoDienThoai = model.SoDienThoai,
                LoaiKhachHangId = model.LoaiKhachHangId,
                TenKhachHang = model.TenKhachHang,
                MaKhachHang = model.MaKhachHang
            };
            sqlContext.KhachHangs.Add(entity);
            sqlContext.SaveChanges();
        }

        public async Task DeleteKhachHangByIdAsync(string id)
        {
            var khachHang = sqlContext.KhachHangs.FirstOrDefault(t => t.Id.Equals(id));
            if (khachHang == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            sqlContext.KhachHangs.Remove(khachHang);
            sqlContext.SaveChanges();
        }

        public async Task<KhachHangCreateModel> GetKhachHangById(string id)
        {
            var info = (from model in sqlContext.KhachHangs.AsNoTracking()
                        where model.Id.Equals(id)
                        select new KhachHangCreateModel
                        {
                            Id = model.Id,
                            Email = model.Email,
                            GhiChu = model.GhiChu,
                            DiaChi = model.DiaChi,
                            SoDienThoai = model.SoDienThoai,
                            LoaiKhachHangId = model.LoaiKhachHangId,
                            TenKhachHang = model.TenKhachHang,
                            MaKhachHang = model.MaKhachHang
                        }).FirstOrDefault();

            if (info == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            return info;
        }

        public async Task<SearchBaseResultModel<KhachHangSearchModel>> SearchKhachHangAsync(KhachHangSearchModel modelSearch)
        {
            SearchBaseResultModel<KhachHangSearchModel> searchResult = new SearchBaseResultModel<KhachHangSearchModel>();
            var data = (from a in sqlContext.KhachHangs.AsNoTracking()
                        select new KhachHangSearchModel
                        {
                            Id = a.Id,
                            TenKhachHang = a.TenKhachHang,
                            MaKhachHang = a.MaKhachHang,
                            GhiChu = a.GhiChu,
                            DiaChi = a.DiaChi,
                            TenLoaiKhachHang = a.LoaiKhachHang.TenLoaiKhachHang,
                            Email = a.Email,
                            SoDienThoai = a.SoDienThoai,
                            LoaiKhachHangId = a.LoaiKhachHang.Id,

                        }).AsQueryable();

            if (!string.IsNullOrEmpty(modelSearch.TenKhachHang))
            {
                data = data.Where(r => r.TenKhachHang.ToUpper().Contains(modelSearch.TenKhachHang.ToUpper()));
            }if (!string.IsNullOrEmpty(modelSearch.LoaiKhachHangId))
            {
                data = data.Where(r => r.LoaiKhachHangId.Equals(modelSearch.LoaiKhachHangId));
            }
            searchResult.DataResults = data.OrderBy(s => s.TenLoaiKhachHang).Skip((modelSearch.PageNumber - 1) * modelSearch.PageSize).Take(modelSearch.PageSize).ToList();
            searchResult.TotalItems = data.Count();
            return searchResult;
        }

        public async Task UpdateKhachHangAsync(string id, KhachHangCreateModel model)
        {
            var khachHang = sqlContext.KhachHangs.FirstOrDefault(t => t.Id.Equals(id));
            if (khachHang == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0001, TextResourceKey.LoaiKhachHang);
            }
            if(khachHang.Email.ToLower()!= model.Email.ToLower())
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0031, TextResourceKey.KhachHang);
            }
            if (model.DiaChi==null|| model.GhiChu==null|| model.MaKhachHang ==null|| model.SoDienThoai ==null|| model.TenKhachHang==null || model.LoaiKhachHangId == null)
            {
                throw NTSException.CreateInstance(MessageResourceKey.MSG0032, TextResourceKey.KhachHang);
            }
            khachHang.TenKhachHang = model.TenKhachHang;
            khachHang.Email = model.Email;
            khachHang.DiaChi = model.DiaChi;
            khachHang.GhiChu = model.GhiChu;
            khachHang.MaKhachHang = model.MaKhachHang;
            khachHang.LoaiKhachHangId = model.LoaiKhachHangId;
            khachHang.SoDienThoai = model.SoDienThoai;
            sqlContext.SaveChanges();
        }
    }
}

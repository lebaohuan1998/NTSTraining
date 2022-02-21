using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTS.Common.RedisCache;
using NTS.Common.Resource;
using NTS.Common.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Users
{
    public class NtsUserService : INtsUserService
    {
        private readonly RedisCacheService redisCacheService;
        private readonly RedisCacheSettingModel redisCacheSettings;
        public NtsUserService(RedisCacheService redisCacheService, IOptions<RedisCacheSettingModel> options)
        {
            this.redisCacheService = redisCacheService;
            this.redisCacheSettings = options.Value;
        }

        //public async Task<NtsUserTokenModel> NtsJwtLogin(NtsUserLoginModel loginModel)
        //{
        //    NtsUserTokenModel userTokenModel = new NtsUserTokenModel();
        //    var securityStampTemp = PasswordUtils.ComputeHash(loginModel.Password + loginModel.SecurityStamp);
        //    if (!loginModel.PasswordHash.Equals(securityStampTemp))
        //    {
        //        throw NTSException.CreateInstance(MessageResourceKey.MSG0021, TextResourceKey.AccountFail);
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(loginModel.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, loginModel.UserId),
        //            //new Claim(SystemConstant.ClaimTypeArea, user.AreaId.ToString()),
        //            //new Claim(SystemConstant.ClaimTypePermission, string.Join(",", claimFunctions))
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(loginModel.ExpireDateAfter),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    userTokenModel.Name = loginModel.UserName;
        //    userTokenModel.UserId = loginModel.UserId;
        //    userTokenModel.EmployeeId = loginModel.EmployeeId;
        //    userTokenModel.DeviceId = loginModel.DeviceId;
        //    userTokenModel.Token = tokenString;
        //    userTokenModel.ExpireDateAfter = loginModel.ExpireDateAfter;

        //    // Key lưu cache login
        //    string keyLogin = $"{ redisCacheSettings.PrefixSystemKey }{redisCacheSettings.PrefixLoginKey}{tokenString}";

        //    // Ghi thông tin vào cache
        //    await redisCacheService.RemoveAsync(keyLogin);
        //    redisCacheService.Add<NtsUserTokenModel>(keyLogin, userTokenModel, new TimeSpan(loginModel.ExpireDateAfter, 0, 0, 0));

        //    return userTokenModel;
        //}
    }
}

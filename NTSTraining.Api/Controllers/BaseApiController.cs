using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ToolManage.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected string GetUserIdByRequest()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string signedInUserId = claims.FirstOrDefault(cl => cl.Type.Equals(ClaimTypes.Name))?.Value;

            return signedInUserId;
        }

        protected bool CheckPermission(string functionCode)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claimPermissions = claims.FirstOrDefault(cl => cl.Type.Equals("Permissions"));
            if (!string.IsNullOrEmpty(claimPermissions?.Value))
            {
                if (!string.IsNullOrEmpty(functionCode))
                {
                    var hasPermission = claimPermissions.Value
                                                        .Split(",")
                                                        .Intersect(new string[] { functionCode });
                    if (hasPermission.Any())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

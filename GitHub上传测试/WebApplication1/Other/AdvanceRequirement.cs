using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Other
{

    /// <summary>
    /// 继承方法，实现Policy验证
    /// </summary>
    public class AdvanceRequirement: AuthorizationHandler<NameAuthorizationRequirement>,IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NameAuthorizationRequirement requirment)
        {
            if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                string sid = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value;
                if (sid.Equals("1"))
                {
                    context.Succeed(requirment);
                }
            }
            return Task.CompletedTask;
        }





    }
}

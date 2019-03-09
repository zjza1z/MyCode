using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Other
{
    public class CusAllowAnonymousAttribute : Attribute, IAllowAnonymous
    {

    }
}

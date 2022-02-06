using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Web.Areas.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //ещё список всех пользоватлей, и круд с ними
        //policy - гибче
    }
}

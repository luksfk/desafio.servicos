using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteServicos.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TesteServicos.DAL;

namespace TesteServicos.Utils
{
    public class UserService : IUserService
    {
        public string UserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}
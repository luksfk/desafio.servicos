using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TesteServicos.DAL;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;

namespace TesteServicos.Controllers
{
    public abstract class BaseController : Controller
    {
       protected BaseController(IUnitOfWork unitOfWork) { }
    }
}
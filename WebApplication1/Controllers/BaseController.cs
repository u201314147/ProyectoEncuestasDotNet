using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Inei.ViewModel;
using WebApplication1.Security;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        public INEIEntities context = new INEIEntities();
    }
}
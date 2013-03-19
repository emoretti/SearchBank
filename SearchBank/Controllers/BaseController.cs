using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace SearchBank.Controllers
{
    public abstract class BaseController : Controller
    {
        public UnitOfWork UnitOfWork { get;  set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if(UnitOfWork == null)
                UnitOfWork = new UnitOfWork();
        }
    }
}

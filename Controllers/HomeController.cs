using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace example_net_waf.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            for (int i = 0; i < filterContext.ActionParameters.Count; i++)
            {
                string key = filterContext.ActionParameters.Keys.ElementAt(i);
                string value = filterContext.ActionParameters[key] as string;

                if (value != null && value.StartsWith("base64|"))
                {
                    filterContext.ActionParameters[key] = decode(value.Substring(7, value.Length - 7));
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NoParametersGet()
        {
            return Json(new
            {
                result = "ok"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ParametersGet(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ParametersJsonGet(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NoParametersPost()
        {
            return Json(new
            {
                result = "ok"
            });
        }

        [HttpPost]
        public ActionResult ParametersPost(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersPostFormData(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                id = id + " " + DateTime.Now,
                name = name + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersJsonPost(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult ParametersJsonPostFormData(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return Json(new { result = "empty parameters" });

            return Json(new
            {
                result = "ok",
                parameters = parameters + " " + DateTime.Now
            });
        }

        public string decode(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}
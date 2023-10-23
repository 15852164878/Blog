
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using Model;

namespace mvc_ef.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
     MefEntities cd = new MefEntities();
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult a(FormCollection F)
        {
            users ba = new users();
        



           ba.uname = F["userName"];
            ba.upwd= F["userpassword"];
         ba.usex= F["sex"];
           //ba.udate=Convert.ToDateTime(F["data"]);
         ba.uiphone=Convert.ToInt32(F["userphone"]);
         ba.uaddress= F["dauserAddressta"];
            cd.users.Add(ba);
            int cbc = cd.SaveChanges();
            if(cbc>0)
            {
                Response.Write("<script>alert('添加成功');</script>");
                List<users> list = (from x in cd.users select x).ToList();
                ViewBag.Show = list;
                return View("degin");
            }
            else
            {
                Response.Write("<script>alert('失败');</script>");
                return View("Index");
            }

        
        }

        public ActionResult degin()
        {
            List<users> list = (from x in cd.users select x).ToList();
            ViewBag.Show = list;
            return View();
        }
    }
}
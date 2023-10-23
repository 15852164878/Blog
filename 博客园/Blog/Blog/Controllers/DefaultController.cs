using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Web.Services;
using System.Dynamic;

namespace Blogq.Controllers
{
    public class DefaultController : Controller
    {
        //FormCollection F
        public static string name;
        public static string pass;
        public static List<Users> show;

        BlogEntities blog = new BlogEntities();
        // GET: Default


        //主页
        [ValidateInput(false)]
        public ActionResult Index()
        {
            List<Comment> lisc = (from x in blog.Comment select x).ToList();
            ViewBag.commcount = lisc;
            List<bpub> list = (from x in blog.bpub orderby x.bid descending select  x).Take(7).ToList();
            ViewBag.blogshow = list;
            ViewBag.Show = "";
            return View();
        }
        public ActionResult Inde()
        {
            List<Comment> lisc = (from x in blog.Comment select x).ToList();
            ViewBag.commcount = lisc;
            List<bpub> list = (from x in blog.bpub orderby x.bid descending select x).Take(7).ToList();
            ViewBag.blogshow = list;
            ViewBag.Show = show;
            return View("Index");
        }
        //注册登录
        public ActionResult Iogin()
        {
            return View();
        }
        [WebMethod]
        public string Lregister(string Name, string Email, string Password)
        {
            if (Name == "" && Email == "" && Password == "")
            {
                return "3";

            }
            else {
                var query = (from b in blog.Users where b.uname == Name select b).Any();
                if (query == true)
                {
                    return "1";
                }


                Users ba = new Users();
                ba.uname = Name;
                ba.upass = Password;
                ba.email = Email;
                blog.Users.Add(ba);
                int cbc = blog.SaveChanges();
                if (cbc > 0)
                {
                    return "2";
                }
                return "4";
               
            }


        }
        [WebMethod]
        public bool Llogin(string Name, string Password)
        {
            Users ba = new Users();
            var ne = Name;
            var pwd = Password;
            var query = (from b in blog.Users where b.uname == ne && b.upass == pwd select b).Any();
            if (query == true)
            {
                name = ne;
                pass = pwd;
            }
          
            return query;

        }

        public ActionResult ndex()
        {

          
            List<bpub> lis = (from x in blog.bpub orderby x.bid descending select x).Take(7).ToList();
            ViewBag.blogshow = lis;
            List<Comment> lisc = (from x in blog.Comment  select x).ToList();
            ViewBag.commcount = lisc;

            List<Users> list = (from x in blog.Users where x.uname == name && x.upass == pass select x).ToList();
            ViewBag.Show = list;
            show = list;
            return View("Index");
        }

        //个人资料
        public ActionResult account()
        {
            ViewBag.Show = show;
            return View();
        }
        public ActionResult information()
        {
            ViewBag.Show = show;
            return View();
        }

        public void inforupt(string name, string sex, string date, string famliy, string marriage, string condition)
        {
            foreach (var i in show)
            {
                Users cust = (from c in blog.Users
                              where c.uid == i.uid
                              select c).Single();

                cust.uname = i.uname;
                cust.upass = i.upass;
                cust.email = i.email;
                cust.name = name;
                cust.sex = sex;
                //cust.date = date;
                cust.family = famliy;
                cust.marriage = marriage;
                cust.condition = condition;
                cust.portrait = i.portrait;
                blog.SaveChanges();

                List<Users> list = (from x in blog.Users where x.uid == i.uid select x).ToList();
                show = list;

            }

        }

        public void accupt(string nickname, string uname, string pass, string email)
        {
            try
            {
                foreach (var i in show)
                {
                    Users cust = (from c in blog.Users
                                  where c.uid == i.uid
                                  select c).Single();

                    cust.uname = uname;
                    cust.upass = pass;
                    cust.email = email;
                    cust.name = i.name;
                    cust.sex = i.sex;
                    cust.date = i.date;
                    cust.family = i.family;
                    cust.marriage = i.marriage;
                    cust.condition = i.condition;
                    cust.portrait = i.portrait;
                    cust.nickname = nickname;
                    blog.SaveChanges();


                    List<Users> list = (from x in blog.Users where x.uid == i.uid select x).ToList();
                    show = list;

                }
            }
            catch { };

        }
        public void pictureupload(string image)
        {
            foreach (var i in show)
            {
                Users cust = (from c in blog.Users
                              where c.uid == i.uid
                              select c).Single();

                cust.uname = i.uname;
                cust.upass = i.upass;
                cust.email = i.email;
                cust.name = i.name;
                cust.sex = i.sex;
                cust.date = i.date;
                cust.family = i.family;
                cust.marriage = i.marriage;
                cust.condition = i.condition;
                cust.portrait = image;
                cust.nickname = i.nickname;
                blog.SaveChanges();


                List<Users> list = (from x in blog.Users where x.uid == i.uid select x).ToList();
                show = list;
            }
        }
        [HttpGet]
        public ActionResult home(string name)
        {
            
            ViewBag.Show = show;
            ViewBag.page = name;
            return View();
        }
        [HttpPost]
        public ActionResult home()
        {

            ViewBag.Show = show;
            return View();
        }
        //链接
        public ActionResult link()
        {
            foreach (var i in show)
            {
                List<link> list = (from x in blog.link where x.uid ==i.uid select x).ToList();
                ViewBag.linkshow = list;
            }
            return View();
        }
        public ActionResult linkdel(int id)
        {

            link cust = (from c in blog.link
                         where c.lid == id
                          select c).First();

            blog.link.Remove(cust);
            blog.SaveChanges();
            return Redirect("link");

        }
        [HttpPost]
        public ActionResult editLink()
        {
            ViewBag.name = "";
            ViewBag.address = "";
            ViewBag.Show = show;
            return View();
        }
        [HttpGet]
        public ActionResult editLink(string name, string address)
        {
            ViewBag.name = name;
            ViewBag.address = address;
            ViewBag.Show = show;
            return View();
        }
        public ActionResult addlink(FormCollection F)
        {
            foreach (var i in show)
            {
                link cust = new link();
                cust.uid = i.uid;
                cust.lname= F["roleName"];
                cust.laddress= F["roleDesc"];
                blog.link.Add(cust);
                
                blog.SaveChanges();
            }
            return Redirect("link");
           
        }
        //博客
        public ActionResult news()
        {
            foreach (var i in show)
            {
                List<bpub> list = (from x in blog.bpub where x.uid==i.uid select x).ToList();
                ViewBag.count=list.Count();
                ViewBag.bpubshow = list;
                ViewBag.Show = show;
            }
            return View();
        }
        public ActionResult newsdel(int id)
        {
           
                bpub cust = (from c in blog.bpub
                             where c.bid == id
                              select c).First();

            blog.bpub.Remove(cust);
            blog.SaveChanges();
             return Redirect("news");

        }

        //发布博客界面
        public ActionResult publishblog()
        {
            foreach (var i in show)
            {
                List<btype> list = (from x in blog.btype where x.uid == i.uid select x).ToList();
                ViewBag.blogclass = list;
            }
            ViewBag.Show = show;
            ViewBag.title ="";
            ViewBag.content = "";

            return View();
        }
        //发布博客方法
        [ValidateInput(false)]
        public ActionResult blogadd(FormCollection F)
        {
            bpub cust = new bpub();
            foreach (var i in show)
            {
               
                cust.btitle = F["title"];
                cust.uid = i.uid;
                cust.bcon = F["con"];
                cust.btype = F["type"];
                cust.author = i.uname;
                cust.data = DateTime.Now;
                cust.bprotrait = i.portrait;
                cust.count = 0;
                blog.bpub.Add(cust);
            }
           
            blog.SaveChanges();
            return Redirect("news");
        }
        //博客修改界面！    
        public ActionResult newsupt(int id)
        {
            List<bpub> list = (from x in blog.bpub where x.bid == id select x).ToList();
            foreach (var i in list)
            {
                ViewBag.title = i.btitle;
                ViewBag.content = i.bcon;
            }
            foreach (var j in show)
            {
                List<btype> blogclass = (from x in blog.btype where x.uid == j.uid select x).ToList();
                ViewBag.blogclass = blogclass;
            }
            // 博客修改
            return View("publishblog");
        }
        //博客修改数据方法？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？
        [ValidateInput(false)]
        public ActionResult blogupt(FormCollection F)
        {
            bpub cust = new bpub();
            foreach (var i in show)
            {

                cust.btitle = F["title"];
                cust.uid = i.uid;
                cust.bcon = F["con"];
                cust.btype = F["type"];
                cust.author = i.uname;
                cust.data = DateTime.Now;
                cust.bprotrait = i.portrait;
                cust.count = 0;
            }

            blog.SaveChanges();
            return Redirect("news");
        }
        //个人分类
        public ActionResult newsType()
        {
            foreach (var i in show)
            {
                List<btype> list = (from x in blog.btype where x.uid==i.uid select x).ToList();
                ViewBag.btypeshow = list;
                ViewBag.Show = show;
            }
            return View();
        }
        //添加分类
        public ActionResult addtype()
        {
            ViewBag.Show = show;
            return View();
        }
        public ActionResult newtypedel(int id)
        {

            btype cust = (from c in blog.btype
                          where c.tid == id
                         select c).First();

            blog.btype.Remove(cust);
            blog.SaveChanges();
            return Redirect("newsType");

        }
        

         public ActionResult typeadd(FormCollection F)
        {
            foreach (var i in show)
            {
                btype cust = new btype();
                cust.tname = F["roleName"];
                cust.uid = i.uid;
                cust.describe = F["roleDesc"];
                blog.btype.Add(cust);
                blog.SaveChanges();
            }
            return Redirect("newsType");

        }
        public ActionResult dynamic()
        {
            ViewBag.Show = show;
            return View();
        }
        //详情 评论 点赞
        public ActionResult Details()
        {
            ViewBag.Show = show;
            return View();
        }
        [HttpGet]
        public ActionResult detail(int id)
        {
           
            List<bpub> list = (from x in blog.bpub where x.bid==id select x).ToList();
            ViewBag.blogshow = list;

            foreach (var count in list)
            {
                count.count = count.count+1;

            }
            blog.SaveChanges();
            var test = (from a in blog.Comment
               join b in blog.Users on a.uid equals b.uid
               join c in blog.bpub on a.bid equals c.bid
               where (a.bid == id)&&(a.hid==null)
                       orderby c.bid descending
                       select new {cid=a.cid,uid=a.uid, znum=a.znum, uname=b.uname,nickname=b.nickname,portrait=b.portrait,conten=a.conten,data=a.cdate,bid=c.bid });
            List<dynamic> oneList = new List<dynamic>();
            foreach(var one in test.ToList())
            {
                dynamic dyObject = new ExpandoObject();
                dyObject.cid = one.cid;
                dyObject.uid = one.uid;
                dyObject.znum = one.znum;
                dyObject.uname = one.uname;
                dyObject.nickname = one.nickname;
                dyObject.portrait = one.portrait;
                dyObject.conten = one.conten;
                dyObject.data = one.data;
                dyObject.bid = one.bid;
                oneList.Add(dyObject);
            }
            ViewBag.comshow = oneList;
            var tes = (from a in blog.Comment
                       join b in blog.Users on a.uid equals b.uid
                       join c in blog.bpub on a.bid equals c.bid
                       where (a.bid == id) && (a.hid != null)
                       orderby c.bid descending
                       select new { cid = a.cid, uid = a.uid, znum = a.znum, hid =a.hid, uname = b.uname, nickname = b.nickname, portrait = b.portrait, conten = a.conten, data = a.cdate, bid = c.bid });
            List<dynamic> twoList = new List<dynamic>();
            foreach (var tw in tes.ToList())
            {
                dynamic dyObject = new ExpandoObject();
                dyObject.cid = tw.cid;
                dyObject.uid = tw.uid;
                dyObject.znum = tw.znum;
                dyObject.hid = tw.hid;
                dyObject.uname = tw.uname;
                dyObject.nickname = tw.nickname;
                dyObject.portrait = tw.portrait;
                dyObject.conten = tw.conten;
                dyObject.data = tw.data;
                dyObject.bid = tw.bid;
                twoList.Add(dyObject);
            }
            ViewBag.tcomshow = twoList;


            ViewBag.Show = show;
            return View("Details");


        }
      

        //评论
        public int pubcomm(int uid,int bid,string conten, DateTime dat)
        {
             int cid=0;
            Comment cust = new Comment();
            cust.uid = uid;
            cust.bid = bid;
            cust.conten = conten;
            cust.cdate = dat;
            cust.znum = 0;
            blog.Comment.Add(cust);
            blog.SaveChanges();
      
            var test = (from a in blog.Comment
                        join b in blog.Users on a.uid equals b.uid
                        join c in blog.bpub on a.bid equals c.bid
                        where (a.bid == bid) && (a.hid == null)
                        select new { cid = a.cid, uid = a.uid, znum = a.znum, uname = b.uname, nickname = b.nickname, portrait = b.portrait, conten = a.conten, data = a.cdate, bid = c.bid });
            List<dynamic> oneList = new List<dynamic>();
            foreach (var one in test.ToList())
            {
                dynamic dyObject = new ExpandoObject();
                dyObject.cid = one.cid;
                dyObject.uid = one.uid;
                dyObject.znum = one.znum;
                dyObject.uname = one.uname;
                dyObject.nickname = one.nickname;
                dyObject.portrait = one.portrait;
                dyObject.conten = one.conten;
                dyObject.data = one.data;
                dyObject.bid = one.bid;
                oneList.Add(dyObject);
            }
            ViewBag.comshow = oneList;
            var tes = (from a in blog.Comment
                       join b in blog.Users on a.uid equals b.uid
                       join c in blog.bpub on a.bid equals c.bid
                       where (a.bid == bid) && (a.hid != null)
                       select new { cid = a.cid, hid = a.hid,znum=a.znum, uname = b.uname, nickname = b.nickname, portrait = b.portrait, conten = a.conten, data = a.cdate, bid = c.bid });
            List<dynamic> twoList = new List<dynamic>();
            foreach (var tw in tes.ToList())
            {
                dynamic dyObject = new ExpandoObject();
                dyObject.cid = tw.cid;
                dyObject.hid = tw.hid;
                dyObject.znum = tw.znum;
                dyObject.uname = tw.uname;
                dyObject.nickname = tw.nickname;
                dyObject.portrait = tw.portrait;
                dyObject.conten = tw.conten;
                dyObject.data = tw.data;
                dyObject.bid = tw.bid;
                twoList.Add(dyObject);
            }
            ViewBag.tcomshow = twoList;
            List<Comment> commid = (from x in blog.Comment where x.uid == uid&x.bid==bid&x.hid==null&x.conten==conten&x.cdate==dat select x).ToList();
            foreach(var i in commid)
            {
               cid= i.cid;
            }
            return cid;
            

        }
        //回复
        public void hfcomm(int uid, int bid,int hid, string conten, DateTime dat)
        {
            Comment cust = new Comment();
            cust.uid = uid;
            cust.bid = bid;
            cust.hid = hid;
            cust.conten = conten;
            cust.cdate = dat;
            blog.Comment.Add(cust);
            blog.SaveChanges();
        }
        [WebMethod]
        public void commdel(int cid)
        {
            Comment cust = (from c in blog.Comment
                            where  (c.cid == cid&&c.hid==null)
                            select c).First();

            blog.Comment.Remove(cust);
            blog.SaveChanges();

        }
        
         public void twcomdel(int cid)
        {
            Comment cust = (from c in blog.Comment
                            where c.cid == cid
                            select c).First();

            blog.Comment.Remove(cust);
            blog.SaveChanges();
         
        }
        public JsonResult postbid(int uid)
        {
            var bid = (from x in blog.bpub
                     where x.uid == uid
                     select new{ bid=x.bid});

            List<dynamic> twoList = new List<dynamic>();
            foreach (var tw in bid.ToList())
            {
                dynamic dyObject = new ExpandoObject();
                dyObject.cid = tw.bid;
                twoList.Add(dyObject);
            }
            return Json(twoList);

        }
        
          public void zsum(int zNum,int cid)
        {
            var b = from i in blog.Comment
                    where i.cid == cid
                    select i;
            foreach(var i in b)
            {
                i.znum = zNum;
            }
            blog.SaveChanges();

        }
    }
}
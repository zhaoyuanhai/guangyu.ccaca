using CCACAWebUI.DB;
using CCACAWebUI.Filters;
using CCACAWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace CCACAWebUI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(DbEntityContext dbEntity)
            : base(dbEntity)
        { }

        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            var homeConfigList = DbContext.Home.ToList();
            var carousel = DbContext.Carousels.ToList();
            var contact = DbContext.Configures.Where(x => x.Type == (int)ConfigTypeEnum.Contact).ToList();
            var projectInfo = DbContext.ProjectInfos.ToList();
            var information = DbContext.Informations
                .OrderByDescending(x => x.CreateTime)
                .Take(6).ToList();
            var newActive = DbContext.NewActive.Take(6).ToList();

            int? qqid = contact.FirstOrDefault(x => x.Title == "QQ")?.ID;
            int? phoneid = contact.FirstOrDefault(x => x.Title == "电话")?.ID;

            Translate(homeConfigList, Language);
            Translate(carousel, Language);
            Translate(contact, Language);
            Translate(information, Language);

            return View(new IndexModel()
            {
                ConfigList = homeConfigList,
                CarouselFigure = carousel,
                Contact = contact,
                Project = projectInfo,
                Infomation = information,
                NewActive = newActive,
                QQId = qqid,
                PhoneId = phoneid
            });
        }

        public IActionResult CCaca()
        {
            var ccacaConfigList = DbContext.Configures.Where(x => x.Type == (int)ConfigTypeEnum.CCACA).ToList();
            var companyList = DbContext.Company.ToList();
            Translate(companyList, Language);
            Translate(ccacaConfigList, Language);
            return View(new CcacaModel()
            {
                OneId = 1,
                TowId = 2,
                ThreeId = 3,
                ConfigList = ccacaConfigList,
                Company = companyList
            });
        }

        public IActionResult NewInfo(int pageIndex = 1, int pageCount = 10)
        {
            var infos = DbContext.Informations
                .OrderByDescending(x => x.CreateTime)
                .Skip((pageIndex - 1) * pageCount)
                .Take(pageCount).ToList();
            ViewBag.PageCount = Math.Ceiling(DbContext.Informations.Count() / (pageCount * 1.0));
            ViewBag.PageIndex = pageIndex;

            Translate(infos, Language);
            return View(infos);
        }

        /// <summary>
        /// 最新活动
        /// </summary>
        /// <returns></returns>
        public IActionResult NewActive(int pageIndex = 1, int pageCount = 4)
        {
            var datas = DbContext.NewActive
                .OrderByDescending(x => x.CreateTime)
                .Skip((pageIndex - 1) * pageCount)
                .Take(pageCount).ToList();
            ViewBag.PageCount = Math.Ceiling(DbContext.NewActive.Count() / (pageCount * 1.0));
            ViewBag.pageIndex = pageIndex;

            return View(datas);
        }

        public IActionResult NewActiveItem(int id)
        {
            var model = DbContext.NewActive.FirstOrDefault(x => x.ID == id);
            return View(model);
        }

        public IActionResult GetInformation(int pageIndex = 1, int pageSize = 6)
        {
            var count = DbContext.Informations.Count();
            var datas = DbContext.Informations.OrderByDescending(x => x.CreateTime)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            var pageCount = (int)Math.Ceiling(count * 1.0 / pageSize);
            Translate(datas, Language);
            return Json(new
            {
                datas,
                isLastPage = pageCount == pageIndex,
                pageCount
            });
        }

        public IActionResult ResourceDown(int pageIndex = 1, int pageCount = 8)
        {
            var datas = DbContext.File.OrderByDescending(x => x.ID)
                .Skip((pageIndex - 1) * pageCount)
                .Take(pageCount).ToList();
            ViewBag.PageCount = Math.Ceiling(DbContext.File.Count() / (pageCount * 1.0));
            ViewBag.pageIndex = pageIndex;

            Translate(datas, Language);
            return View(datas);
        }

        public IActionResult ProjectInfo(int pageIndex = 1, int pageSize = 10)
        {
            var count = DbContext.ProjectInfos.Count();
            var projectInfo = DbContext.ProjectInfos
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var pageCount = (int)Math.Ceiling(count * 1.0 / pageSize);
            Translate(projectInfo, Language);
            ViewBag.PageCount = pageCount;
            ViewBag.pageIndex = pageIndex;

            return View(projectInfo);
        }

        [Authorize]
        [IsVip]
        public IActionResult ProjectInfoItem(int id)
        {
            var project = DbContext.ProjectInfos.FirstOrDefault(a => a.ID == id);
            return View(project);
        }

        public IActionResult Vip()
        {
            var member = DbContext.Member.ToList();
            var memberLink = DbContext.MemberLinks.ToList();
            var carousels = DbContext.Carousels.First(x => x.Type == "picture");
            Translate(member, Language);
            Translate(memberLink, Language);
            Translate(carousels, Language);
            return View(new VipModel()
            {
                MemberLink = memberLink,
                Member = member,
                Carousel = carousels
            });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            var contactConfig = DbContext.Configures.Where(x => x.Type == (int)ConfigTypeEnum.Contact).ToList();
            var rqCode = DbContext.Carousels.FirstOrDefault(x => x.Type == "rqcode");

            return View(new ContactModel()
            {
                ConfigList = contactConfig,
                RqCode = rqCode
            });
        }

        public IActionResult InfoItem(int id)
        {
            var info = DbContext.Informations.FirstOrDefault(i => i.ID == id);
            return View(info);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

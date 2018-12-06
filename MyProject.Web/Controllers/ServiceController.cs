using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using MyProject.EntityFramework;
using MyProject.EntityFramework.Models;
using System.Linq;
using System.Collections.Generic;
using MyProject.Web.Models;
using System.Data.Entity;
using System.Reflection;
using System.Collections;
using System.Globalization;
using Abp.Web.Security.AntiForgery;
using Abp.Web.Models;
using System.Net.Http;
using System.Net;
using System.Configuration;

namespace MyProject.Web.Controllers
{
    public class ServiceController : MyProjectControllerBase
    {
        DefaultDbContext dbContent = new DefaultDbContext();

        private string SaveFile(HttpPostedFileBase file, string dir = null)
        {
            var path = (dir == null ? "/images/" : $"/{dir}/") + Guid.NewGuid() + Path.GetExtension(file.FileName);
            string absPath = Server.MapPath("~" + path);
            if (!Directory.Exists(Path.GetDirectoryName(absPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(absPath));

            file.SaveAs(absPath);
            return path;
        }

        [ValidateInput(false)]
        public ActionResult SefRef(RefModel model)
        {
            var ass = Assembly.LoadFile(Server.MapPath("~/bin/MyProject.EntityFramework.dll"));
            var type = ass.GetType("MyProject.EntityFramework.Models." + model.TableName);

            foreach (var item in model.Props)
            {
                var p = type.GetProperty(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (p == null)
                    continue;

                var @ref = new T_Ref()
                {
                    TableName = model.TableName,
                    FiledName = p.Name,
                    LanguageID = model.LanguageId,
                    RowID = model.Id,
                    RowValue = item.Value
                };

                var file = Request.Files["props.file_" + item.Key];
                if (file != null && file.ContentLength > 0)
                {
                    @ref.RowValue = SaveFile(file);
                }

                var refModel = dbContent.Refs.FirstOrDefault(x =>
                      x.TableName == @ref.TableName &&
                      x.FiledName == @ref.FiledName &&
                      x.LanguageID == @ref.LanguageID &&
                      x.RowID == @ref.RowID);

                if (refModel != null)
                {
                    refModel.RowValue = @ref.RowValue;
                }
                else
                {
                    dbContent.Refs.Add(@ref);
                }
            }
            dbContent.SaveChanges();

            return Ok();
        }

        #region 轮播图
        public ActionResult GetFiles(string type)
        {
            var datas = dbContent.Carousels.Where(m => m.Type == type).ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetFile(int id, LanguageModel langId)
        {
            var model = dbContent.Carousels.FirstOrDefault(m => m.ID == id);
            Translate(model, langId);
            return JsonMode(model);
        }

        public ActionResult DeleteFile(int id)
        {
            var model = dbContent.Carousels.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(model).State = EntityState.Deleted;
            var refModel = dbContent.Refs.Where(x => x.TableName == "T_Carousel" && x.RowID == id);
            dbContent.Refs.RemoveRange(refModel);
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyFile(T_Carousel carousel)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
                carousel.Path = SaveFile(file);

            dbContent.Entry(carousel).State = EntityState.Modified;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult CreateFile(T_Carousel carousel)
        {
            var file = Request.Files["file"];
            dbContent.Entry(carousel).State = EntityState.Added;
            carousel.Path = SaveFile(file);
            dbContent.SaveChanges();

            return Ok();
        }
        #endregion

        #region home
        public ActionResult HomeList()
        {
            var modelList = dbContent.Home.ToList();
            return JsonModelList(modelList);
        }

        public ActionResult GetHome(int id, LanguageModel langId)
        {
            var model = dbContent.Home.FirstOrDefault(x => x.ID == id);
            Translate(model, langId);
            return JsonMode(model);
        }

        [ValidateInput(false)]
        public ActionResult ModifyHome(T_Home model)
        {
            dbContent.Entry(model).State = EntityState.Modified;

            var file = Request.Files["file_cover"];
            if (file != null && file.ContentLength > 0)
                model.Cover = SaveFile(file);
            else
                dbContent.Entry(model).Property(x => x.Cover).IsModified = false;

            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult GetHomeImg(string type, LanguageModel langId)
        {
            var model = dbContent.Carousels.FirstOrDefault(x => x.Type == type);
            if (model == null)
            {
                model = new T_Carousel();
            }
            Translate(model, langId);
            return JsonMode(model);
        }

        public ActionResult GetCcaca(int id, LanguageModel langId)
        {
            var config = dbContent.Configures.FirstOrDefault(x => x.ID == id);
            Translate(config, langId);
            return JsonMode(config);
        }

        [ValidateInput(false)]
        public ActionResult ModifyCcaca(T_Configure model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region member
        public ActionResult GetMemberList()
        {
            var datas = dbContent.Member.ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetMember(int id, LanguageModel langId)
        {
            var member = dbContent.Member.FirstOrDefault(x => x.ID == id);
            Translate(member, langId);
            return JsonMode(member);
        }

        public ActionResult CreateMember(T_Member model)
        {
            dbContent.Entry(model).State = EntityState.Added;
            model.CreateTime = DateTime.Now;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyMember(T_Member model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.Entry(model).Property(x => x.CreateTime).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteMember(int id)
        {
            var item = dbContent.Member.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(item).State = EntityState.Deleted;
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region memberLink
        public ActionResult GetMemberLinkList()
        {
            var datas = dbContent.MemberLinks.ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetMemberLink(int id, LanguageModel langId)
        {
            var memberLink = dbContent.MemberLinks.FirstOrDefault(x => x.ID == id);
            Translate(memberLink, langId);
            return JsonMode(memberLink);
        }

        public ActionResult CreateMemberlink(T_MemberLink model)
        {
            var file = Request.Files["file_icon"];
            dbContent.Entry(model).State = EntityState.Added;
            model.CreateDate = DateTime.Now;
            model.Icon = SaveFile(file);
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyMemberlink(T_MemberLink model)
        {
            var file = Request.Files["file_icon"];
            if (file != null && file.ContentLength > 0)
                model.Icon = SaveFile(file);

            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.Entry(model).Property(x => x.CreateDate).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteMemberLink(int id)
        {
            var item = dbContent.MemberLinks.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(item).State = EntityState.Deleted;
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region company
        public ActionResult GetCompanyList()
        {
            var datas = dbContent.Company.ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetCompany(int id, LanguageModel langId)
        {
            var memberLink = dbContent.Company.FirstOrDefault(x => x.ID == id);
            Translate(memberLink, langId);
            return JsonMode(memberLink);
        }

        public ActionResult CreateCompany(T_Company model)
        {
            var file = Request.Files["file_icon"];
            dbContent.Entry(model).State = EntityState.Added;
            model.CreateDate = DateTime.Now;
            model.Icon = SaveFile(file);
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyCompany(T_Company model)
        {
            var file = Request.Files["file_icon"];
            if (file != null && file.ContentLength > 0)
                model.Icon = SaveFile(file);

            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.Entry(model).Property(x => x.CreateDate).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteCompany(int id)
        {
            var item = dbContent.Company.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(item).State = EntityState.Deleted;
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region projectInfo
        public ActionResult GetProjectInfoList()
        {
            var datas = dbContent.ProjectInfos.ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetProjectInfo(int id, LanguageModel langId)
        {
            var project = dbContent.ProjectInfos.FirstOrDefault(x => x.ID == id);
            Translate(project, langId);
            return JsonMode(project);
        }

        [ValidateInput(false)]
        public ActionResult CreateProjectInfo(T_ProjectInfo model)
        {
            model.CreatTime = DateTime.Now;
            dbContent.Entry(model).State = EntityState.Added;
            dbContent.SaveChanges();
            return Ok();
        }

        [ValidateInput(false)]
        public ActionResult ModifyProjectInfo(T_ProjectInfo model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.Entry(model).Property(x => x.CreatTime).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteProjectInfo(int id)
        {
            var item = dbContent.ProjectInfos.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(item).State = EntityState.Deleted;
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region newInfoList
        public ActionResult GetNewInfoList()
        {
            var datas = dbContent.Informations.OrderByDescending(x => x.CreateTime).ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetNewInfo(int id, LanguageModel langId)
        {
            var info = dbContent.Informations.FirstOrDefault(x => x.ID == id);
            Translate(info, langId);
            return JsonMode(info);
        }

        [ValidateInput(false)]
        public ActionResult CreateNewInfo(T_Information model)
        {
            model.CreateTime = model.CreateTime ?? DateTime.Now;
            dbContent.Entry(model).State = EntityState.Added;
            var file = Request.Files["file_conver"];
            model.Conver = SaveFile(file);
            dbContent.SaveChanges();
            return Ok();
        }

        [ValidateInput(false)]
        public ActionResult ModifyNewInfo(T_Information model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            var file = Request.Files["file_conver"];
            if (file != null && file.ContentLength > 0)
                model.Conver = SaveFile(file);
            else
                dbContent.Entry(model).Property(x => x.Conver).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteNewInfo(int id)
        {
            var model = dbContent.Informations.FirstOrDefault(x => x.ID == id);
            dbContent.Entry(model).State = EntityState.Deleted;
            dbContent.SaveChanges();
            return Ok();
        }

        #endregion

        #region contact
        public ActionResult GetContactList()
        {
            var datas = dbContent.Configures.Where(x => x.Type == 2).ToList();
            return JsonModelList(datas);
        }

        public ActionResult GetContact(int id, LanguageModel langId)
        {
            var model = dbContent.Configures.FirstOrDefault(x => x.ID == id);
            Translate(model, langId);
            return JsonMode(model);
        }

        public ActionResult CreateContact(T_Configure config)
        {
            dbContent.Entry(config).State = EntityState.Added;
            config.CreateTime = DateTime.Now;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyContact(T_Configure config)
        {
            dbContent.Entry(config).State = EntityState.Modified;
            dbContent.Entry(config).Property(x => x.CreateTime).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteContact(int id)
        {
            var config = dbContent.Configures.FirstOrDefault(x => x.ID == id);
            dbContent.Configures.Remove(config);
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region user
        public ActionResult GetUserList()
        {
            var datas = dbContent.Users.ToList();
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser(int id)
        {
            var user = dbContent.Users.FirstOrDefault(x => x.ID == id);
            return JsonMode(user);
        }

        public ActionResult CreateUser(T_User user)
        {
            var logPath = Server.MapPath("~/log.txt");
            dbContent.Database.Log = (str) =>
            {
                var file = System.IO.File.Open(logPath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(str);
                sw.Close();
                file.Close();
            };
            user.CreateTime = DateTime.Now;
            var userEntry = dbContent.Entry(user);
            userEntry.State = EntityState.Added;

            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult ModifyUser(T_User user)
        {
            var userEntry = dbContent.Entry(user);
            userEntry.State = EntityState.Modified;
            userEntry.Property(x => x.CreateTime).IsModified = false;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteUser(int id)
        {
            var user = dbContent.Users.FirstOrDefault(x => x.ID == id);
            dbContent.Users.Remove(user);
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region Dict
        public ActionResult GetDictList()
        {
            var datas = dbContent.WordDicts.ToList();
            int id = 50000;
            var chinaDatas = datas.GroupBy(x => x.Word).Select(x => new T_WordDict() { ID = id++, Word = x.Key, Value = x.Key, LanguageId = (int)LanguageModel.中文 });

            var result = new
            {
                Model = new List<T_WordDict>(),
                CurrentLanguages = new Dictionary<int, T_Language[]>()
            };

            result.Model.AddRange(chinaDatas);

            foreach (var data in result.Model)
            {
                var ss = datas.Where(x => x.Word == data.Word && x.LanguageId != (int)LanguageModel.中文);
                result.CurrentLanguages.Add(data.ID,
                    Array.ConvertAll(ss.ToArray(), item => new LanguageExt()
                    {
                        ID = item.LanguageId,
                        Lang = ((LanguageModel)item.LanguageId).ToString(),
                        Value = item.Value,
                        DictId = item.ID
                    }));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetDict(T_WordDict model)
        {
            var dict = dbContent.WordDicts.FirstOrDefault(x => x.Word == model.Word && x.LanguageId == model.LanguageId);
            if (dict == null)
                dbContent.Entry(model).State = EntityState.Added;
            else
                dict.Value = model.Value;

            dbContent.SaveChanges();
            Refresh();
            return Ok();
        }

        public ActionResult GetDict(int id)
        {
            var model = dbContent.WordDicts.FirstOrDefault(x => x.ID == id);
            return JsonMode(model);
        }

        public ActionResult ModifyDict(T_WordDict model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.SaveChanges();
            Refresh();
            return Ok();
        }

        public ActionResult DeleteDict(string word)
        {
            var dicts = dbContent.WordDicts.Where(x => x.Word == word);
            dbContent.WordDicts.RemoveRange(dicts);
            dbContent.SaveChanges();
            Refresh();
            return Ok();
        }

        /// <summary>
        /// 刷新客户端的翻译字典
        /// </summary>
        private void Refresh()
        {
            var url = ConfigurationManager.AppSettings["RefreshApi"];
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
        }
        #endregion

        #region NewActive
        public ActionResult GetNewActiveList()
        {
            var datas = dbContent.NewActive.ToList();
            return JsonModelList(datas);
        }

        [ValidateInput(false)]
        public ActionResult GetNewActive(int id)
        {
            var model = dbContent.NewActive.FirstOrDefault(n => n.ID == id);
            return JsonMode(model);
        }

        public ActionResult CreateNewActive(T_NewActive model)
        {
            dbContent.Entry(model).State = EntityState.Added;
            dbContent.SaveChanges();
            return Ok();
        }

        [ValidateInput(false)]
        public ActionResult ModifyNewActive(T_NewActive model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteNewActive(int id)
        {
            var model = dbContent.NewActive.FirstOrDefault(n => n.ID == id);
            dbContent.NewActive.Remove(model);
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region FileMgr
        public ActionResult GetFileList()
        {
            var datas = dbContent.File.ToList();
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileMgr(int id, LanguageModel langId)
        {
            var model = dbContent.File.FirstOrDefault(x => x.ID == id);
            Translate(model, langId);
            return JsonMode(model);
        }

        [HttpPost]
        public ActionResult CreateFileMgr(HttpPostedFileWrapper file, string name)
        {
            var fileModel = new T_File()
            {
                Name = name ?? file.FileName,
                Path = SaveFile(file, "files"),
                CreateDate = DateTime.Now
            };
            dbContent.File.Add(fileModel);
            dbContent.SaveChanges();
            return Ok();
        }

        public ActionResult DeleteFileMgr(int id)
        {
            var file = dbContent.File.FirstOrDefault(x => x.ID == id);
            dbContent.File.Remove(file);
            dbContent.SaveChanges();
            return Ok();
        }
        #endregion

        #region BackImg
        public ActionResult BackImg(string module, HttpPostedFileWrapper file)
        {
            file.SaveAs(Server.MapPath("~/BackImg/" + module + ".jpg"));
            return Ok();
        }
        #endregion

        /// <summary>
        /// 上传图片接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DontWrapResult]
        [DisableAbpAntiForgeryTokenValidation]
        public ActionResult UpLoadFile()
        {
            string uploadFilePath = "/upimages";

            //定义消息
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["url"] = "";
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                //最大文件大小
                int maxSize = 10000000;
                //文件名
                String fileName = file.FileName;
                //文件格式
                String fileExt = Path.GetExtension(fileName).ToLower();
                //定义允许上传的文件扩展名
                string[] extArr = new[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                if (file.InputStream == null || file.InputStream.Length > maxSize)
                {
                    hash["error"] = 1;
                    hash["message"] = "上传文件大小超过限制！";
                }
                else if (String.IsNullOrEmpty(fileExt) || !extArr.Contains(fileExt))
                {
                    hash["error"] = 1;
                    hash["message"] = "上传文件扩展名是不允许的扩展名！";
                }
                else
                {
                    String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                    string path = Server.MapPath(uploadFilePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileUrl = Path.Combine(path, newFileName);
                    file.SaveAs(fileUrl);
                    hash["error"] = 0;
                    hash["url"] = $"{uploadFilePath}/{newFileName}";
                }
            }
            else
            {
                hash["error"] = 1;
                hash["message"] = "请选择文件！";
            }
            return Json(hash, "text/html;charset=UTF-8");
        }
    }
}
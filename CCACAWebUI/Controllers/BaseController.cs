using CCACAWebUI.DB;
using CCACAWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCACAWebUI.Controllers
{
    public class BaseController : Controller
    {
        protected DbEntityContext DbContext;

        protected LanguageEmun Language { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string languageId = Request.Cookies["LanguageId"];
            if (!string.IsNullOrEmpty(Request.Query["LanguageId"].ToString()))
                languageId = Request.Query["LanguageId"];
            if (Request.HasFormContentType && Request.Form["LanguageId"].ToString() != null)
                languageId = Request.Form["LanguageId"];

            Language = int.TryParse(languageId, out int iLanguage) ?
                (LanguageEmun)iLanguage : LanguageEmun.CHINESE;
        }

        public BaseController(DbEntityContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// 翻译单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Translate<T>(T t, LanguageEmun language)
        {
            if (language == LanguageEmun.CHINESE)
                return t;

            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] fileds = type.GetProperties();
            string[] strFiles = fileds.Select(f => f.Name).ToArray();
            int id = Convert.ToInt32(type.GetProperty("ID").GetValue(t, null));

            foreach (string fileName in strFiles)
            {
                var entity = DbContext.Refs.Where(m => m.TableName == tableName &&
                    m.FiledName == fileName &&
                    m.RowID == id &&
                    m.LanguageID == (int)language).FirstOrDefault();
                if (entity != null)
                    type.GetProperty(fileName).SetValue(t, entity.RowValue);
            }
            return t;
        }

        /// <summary>
        /// 翻译集合实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public List<T> Translate<T>(List<T> entityList, LanguageEmun language)
        {
            foreach (var entity in entityList)
            {
                Translate(entity, language);
            }
            return entityList;
        }
    }
}

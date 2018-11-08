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

        protected LanguageEmun Language
        {
            get
            {
                var languageId = this.RouteData.Values["LanguageId"];
                if (languageId != null)
                {
                    return (LanguageEmun)int.Parse(languageId.ToString());
                }
                return LanguageEmun.CHINESE;
            }
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
            int id = Convert.ToInt32(type.GetProperty("id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(t, null));

            var refsDatas = DbContext.Refs.ToList();
            foreach (string fileName in strFiles)
            {
                var prop = type.GetProperty(fileName);
                var entity = refsDatas.Where(m => m.TableName == tableName &&
                    m.FiledName == fileName &&
                    m.RowID == id &&
                    m.LanguageID == (int)language).FirstOrDefault();
                if (entity != null)
                    type.GetProperty(fileName).SetValue(t, Convert.ChangeType(entity.RowValue, prop.PropertyType));
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

using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using MyProject.EntityFramework;
using MyProject.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class MyProjectControllerBase : AbpController
    {
        protected MyProjectControllerBase()
        {
            LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected ActionResult JsonModelList<TModel>(List<TModel> modelList)
        {
            return Json(new TViewModel<List<TModel>>(modelList), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult JsonMode<TModel>(TModel model)
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult Ok()
        {
            return Json(new
            {
                state = true
            }, JsonRequestBehavior.AllowGet);
        }

        public T Translate<T>(T t, LanguageModel language)
        {
            if (language == LanguageModel.中文)
                return t;

            var dbContext = new DefaultDbContext();

            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] fileds = type.GetProperties();
            string[] strFiles = fileds.Select(f => f.Name).ToArray();
            int id = Convert.ToInt32(type.GetProperty("ID").GetValue(t, null));

            foreach (string fileName in strFiles)
            {
                var entity = dbContext.Refs.Where(m => m.TableName == tableName &&
                    m.FiledName == fileName &&
                    m.RowID == id &&
                    m.LanguageID == (int)language).FirstOrDefault();
                if (entity != null)
                {
                    var p = type.GetProperty(fileName);
                    p.SetValue(t, Convert.ChangeType(entity.RowValue, p.PropertyType));
                }
            }
            return t;
        }
    }
}
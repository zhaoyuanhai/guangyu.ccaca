using CCACAWebUI.DB;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCACAWebUI.Common;

namespace CCACAWebUI
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        private static Dictionary<string, T_WordDict> wordList;
        private DbEntityContext dbContext;
        private int languageId;

        protected string GetUserProp(string type)
        {
            foreach (var item in Context.User.Claims)
            {
                if (item.Type == type)
                {
                    return item.Value;
                }
            }
            return null;
        }

        public BaseViewPage()
            : this(new DbEntityContext())
        {

        }

        public BaseViewPage(DbEntityContext dbEntity)
        {
            this.dbContext = dbEntity;
        }

        protected HtmlString _(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return new HtmlString(string.Empty);

            if (this.languageId == 0)
            {
                string strLanId = this.ViewContext.RouteData.Values["LanguageId"].ToString();
                int.TryParse(strLanId, out int languageId);
                this.languageId = languageId;
            }

            var word = DictCache.GetDict(languageId, key);
            if (word != null)
                return new HtmlString(word.Value);
            return new HtmlString(key);
        }
    }
}

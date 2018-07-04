using MyProject.EntityFramework;
using MyProject.EntityFramework.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Web.Models
{
    public class TViewModel<TModel> where TModel : IEnumerable
    {
        private DefaultDbContext dbContext;

        public IEnumerable Model { get; set; }

        public LanguageModel Language = LanguageModel.中文;

        public TViewModel(TModel model)
        {
            this.dbContext = new DefaultDbContext();
            Model = model;
        }

        public TViewModel(TModel model, LanguageModel language)
            : this(model)
        {
            this.Language = language;
        }

        public Dictionary<int, T_Language[]> CurrentLanguages
        {
            get
            {
                var dict = new Dictionary<int, T_Language[]>();
                var refs = dbContext.Refs.ToList();
                Type type = null;
                foreach (var item in this.Model)
                {
                    if (type == null)
                    {
                        type = item.GetType();
                    }

                    string tableName = type.Name;
                    int id = Convert.ToInt32(type.GetProperty("ID").GetValue(item, null));
                    var langs = refs.Where(m => m.TableName == tableName && m.RowID == id)
                        .GroupBy(m => m.LanguageID)
                        .Select(m =>
                        {
                            var lang = new T_Language();
                            lang.ID = m.Key;
                            lang.Lang = ((LanguageModel)m.Key).ToString();
                            return lang;
                        }).ToArray();
                    dict.Add(id, langs);
                }
                return dict;
            }
        }
    }
}
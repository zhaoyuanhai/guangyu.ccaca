using CCACAWebUI.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCACAWebUI.Common
{
    public class DictCache
    {
        public static DbEntityContext dbContext;
        private static Dictionary<string, T_WordDict> wordList;

        public static void InitWordDict()
        {
            using (var dbContext = new DbEntityContext())
            {
                wordList = dbContext.WordDicts.ToList().ToDictionary(x => $"{x.Word}_{x.LanguageId}");
            }
        }

        public static T_WordDict GetDict(int languageId, string key)
        {
            if (wordList == null)
            {
                InitWordDict();
            }
            if (wordList.ContainsKey($"{key}_{languageId}"))
                return wordList[$"{key}_{languageId}"];
            return null;
        }
    }
}

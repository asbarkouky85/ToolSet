using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSet.Localization
{
    public class AbpSyncLanguagesRequest : RequestBase
    {
        string LocalizationRoot;
        string lang1;
        string lang2;

        public AbpSyncLanguagesRequest(string[] args) : base(args)
        {
        }

        private void SyncLanguages(string lang1, string lang2)
        {
            string resLang1 = Path.Combine(LocalizationRoot, lang1 + ".json");
            string resLang2 = Path.Combine(LocalizationRoot, lang2 + ".json");

            var data1 = GetItems("", lang1);
            var data2 = GetItems("", lang2);

            int i = 0;
            foreach (var item in data1)
            {
                if (!data2.Any(d => d.Key == item.Key))
                {
                    data2[item.Key] = Utils.IdToPhrase(item.Key);
                    i++;
                }

            }
            Console.WriteLine($"{lang1} --> {lang2} : Added {i} Entries..");

            i = 0;
            foreach (var item in data2)
            {
                if (!data1.Any(d => d.Key == item.Key))
                {
                    data1[item.Key] = Utils.IdToPhrase(item.Key);
                    i++;
                }
            }
            Console.WriteLine($"{lang2} --> {lang1} : Added {i} Entries..");
            SaveData("", lang1, data1.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value));
            SaveData("", lang2, data2.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value));

        }

        private Dictionary<string, string> GetItems(string type, string locale)
        {
            var ret = new Dictionary<string, string>();
            string resPath = Path.Combine(LocalizationRoot, locale + ".json");
            if (File.Exists(resPath))
            {
                var txt = File.ReadAllText(resPath);

                AbpResourceFile file = JsonConvert.DeserializeObject<AbpResourceFile>(txt);
                ret = file.Texts;
            }
            return ret;
        }



        public override void ProcessArgs()
        {
            if (Arguments.Length < 4)
                throw new NotEnoughArgumentsException();
            LocalizationRoot = Path.Combine(MainDirectory, Arguments[1]);
            lang1 = Arguments[2];
            lang2 = Arguments[3];
        }

        public override void Execute(Dispatcher service)
        {
            SyncLanguages(lang1, lang2);
        }

        public override string[] GetHelp()
        {
            return new string[]
            {
                "[json path] [language 1] [language 2]",
                "Synchronizing keys between languages"
            };
        }

        void SaveData(string type, string lang, Dictionary<string, string> lst)
        {
            string resLang1 = Path.Combine(LocalizationRoot, lang + ".json");

            var data = new AbpResourceFile { Culture = lang, Texts = lst.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value) };
            File.WriteAllText(resLang1, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.Localization;

namespace SLS
{
    class Debug
    {
        static GameCulture _DumpCulture = null;
        static LocalizedText[] _Dump = null;

        public static LocalizedText Find(string keyStart, string value, bool matchCase = false)
        {
            var first = LanguageDump().FirstOrDefault(x => x.Key.StartsWith(keyStart) && x.Value.Equals(value, matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase));

            return first;
        }

        public static string FindFieldName(string keyStart, string value, bool matchCase = false)
        {
            var found = Find(keyStart, value, matchCase);
            if (found == null)
                return string.Empty;

            return found.Key.Split(new[] { '.' }, 2)[1];
        }

        public static string FindFieldLangname(string key, bool matchCase = false)
        {
            var dump = LanguageDump();
            var first = dump.FirstOrDefault(x => x.Key.Equals(key, matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase));
            if (first == null)
                return string.Empty;

            return first.Value;
        }

        public static LocalizedText[] LanguageDump()
        {
            var redumb = _Dump == null;
            if(_DumpCulture != null)
                redumb |= _DumpCulture != Language.ActiveCulture;

            if(_Dump == null)
            {
                _Dump = Language.FindAll(new System.Text.RegularExpressions.Regex(".*."));
                _DumpCulture = Language.ActiveCulture;
            }
            return _Dump;
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            switch(cmd)
            {
                case "language.findall":
                    var text = LanguageDump();
                    using (StreamWriter sw = new StreamWriter("debug_LangDump.txt", false))
                    foreach(var x in text.OrderBy(x => x.Key))
                    {
                        sw.WriteLine(x.Key + "->" + x.Value.Replace("\n", "\t"));
                    }
                    Globals.Print("Done! Wrote {0} LocalizedTexts to debug_LangDump.txt", text.Length);
                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}

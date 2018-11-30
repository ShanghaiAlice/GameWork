using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static partial class GameTables
    {
        static string confPath;
        public static Dictionary<string, Dictionary<string, string>> _langs;


        public static void Load(string dataFolder)
        {
            confPath = dataFolder;
            string fn;
       //     fn = string.Format("{0}/TElf_宝可梦.csv", dataFolder);
       //     宝可梦 = CSVReader.LoadAsObjects<table_宝可梦>(fn);
        }

    }
}

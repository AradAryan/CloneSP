using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloneSP
{
    class Program
    {
        #region Fields
        /// <summary>
        /// Directory of Place That Program Start Reading SQL files Form There
        /// </summary>
        private static string mainPath =
            @"C:\Users\faranam\Desktop\Desktop\Exam\05 - clone SP\files";

        /// <summary>
        /// Directory of Place Where Program Store Changes There
        /// </summary>
        private static string outputPath =
            @"C:\Users\faranam\Desktop\Exam\05 - clone SP\files\output\";

        #endregion Fields

        #region Renaming Context Function
        /// <summary>
        /// Main Function => Start Renaming Context
        /// </summary>
        public static void RenameFiles()
        {
            ///Get List Of SQL Files
            var temp = Directory.GetFiles(mainPath, "*.sql");

            ///Creating Instance of Streams For Read & Write
            StreamReader sr;
            StreamWriter sw;

            ///Reading File
            foreach (var item in temp)
            {
                sr = new StreamReader(item);
                var context = sr.ReadToEnd();

                ///Replace
                context = Regex.Replace(context, "BaseInfo", "AltBaseInfo", RegexOptions.IgnoreCase);
                context = Regex.Replace(context, "CustInfo", "AltCustInfo", RegexOptions.IgnoreCase);
                context = Regex.Replace(context, "(create procedure)", "ALTER PROC", RegexOptions.IgnoreCase);
                context = Regex.Replace(context, "(create proce)", "ALTER PROCEDURE ", RegexOptions.IgnoreCase);
                context = Regex.Replace(context, @"(\sdbo.\[\w+)(\b)", "$1_Alt", RegexOptions.IgnoreCase);
                context = Regex.Replace(context, @"(\sdbo.\w+)(\b)", "$1_Alt", RegexOptions.IgnoreCase);

                ///Write To File
                sw = new StreamWriter(outputPath + Path.GetFileName(item));
                sw.Write(context);
                sw.Flush();
            }
        }

        #endregion Renaming Context Function

        #region Main Function
        /// <summary>
        /// Main Function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RenameFiles();

            Console.WriteLine("Jobs Done!");
            Console.ReadKey();
        }
        #endregion Main Function
    }
}

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
            @"C:\Users\faranam\Desktop\Exam\05 - clone SP\files";

        /// <summary>
        /// Directory of Place Where Program Store Changes There 
        /// </summary>
        private static string outputPath =
            @"C:\Users\faranam\Desktop\Exam\05 - clone SP\files\output\";
        #endregion Fields

        #region Renaming Context Funtion
        /// <summary>
        /// Main Funtion => Start Renaming Context
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
                context = context.Replace("BaseInfo", "AltBaseInfo");
                context = context.Replace("CustInfo", "AltCustInfo");

                ///Extract DB Name
                string dboName = "";
                var dbName = new string[context.Length];
                dbName = context.Split(' ');
                foreach (var value in dbName)
                {
                    if (value.Contains("dbo."))
                    {
                        dboName = value;
                        break;
                    }
                    else dboName = "404";
                }

                ///Error Hnadling
                if (dboName == "404")
                    break;

                ///Replace DB Name
                else
                {
                    if (dboName.Contains(']'))
                    {
                        int index = dboName.IndexOf(']');
                        dboName.Replace("]", "_alt]");
                    }

                    if (dboName.Contains("\r"))
                    {
                        string[] str = new string[2];
                        string charIndex = "\r";
                        str = dboName.Split(charIndex.ToCharArray());
                        context = context.Replace(str[0], str[0].Replace("]", "_alt]"));
                    }

                    if (context.Contains(dboName))
                    {
                        string[] str = new string[2];
                        string charIndex = "\r";
                        str = dboName.Split(charIndex.ToCharArray());
                        context = context.Replace(str[0], str[0] + "_alt");
                    }

                    ///Write To File
                    sw = new StreamWriter(outputPath + Path.GetFileName(item));
                    sw.Write(context);
                    sw.Flush();
                }
            }
        }
        #endregion Renaming Context Funtion

        #region Main Function
        static void Main(string[] args)
        {
            RenameFiles();

            Console.WriteLine("Jobs Done!");
            Console.ReadKey();
        }
        #endregion Main Function
    }
}

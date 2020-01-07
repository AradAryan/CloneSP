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
        /// <summary>
        /// 
        /// </summary>
        private static string mainPath = @"C:\Users\faranam\Desktop\Exam\05 - clone SP\files";

        /// <summary>
        /// 
        /// </summary>
        private static string outputPath = @"C:\Users\faranam\Desktop\Exam\05 - clone SP\files\output\";

        public static void RenameFiles()
        {
            var temp = Directory.GetFiles(mainPath, "*.sql");
            //Console.WriteLine(Directory.GetFiles(mainPath, "*.sql"));
            var directories = new string[temp.Length];
            StreamReader sr;
            StreamWriter sw;
            foreach (var item in temp)
            {
                sr = new StreamReader(item);
                var context = sr.ReadToEnd();

                context = context.Replace("BaseInfo", "AltBaseInfo");
                context = context.Replace("CustInfo", "AltCustInfo");
                //context.Contains("create proc") || context.Contains("create procedure");
                int placementNumber = 0;
                /*  if (String.Compare(context, "create proc") == 0)
                  {
                      Regex.Replace(context, "create proc", "CREATE PROC");
                      placementNumber = context.IndexOf("CREATE PROC");
                  }
                  if (String.Compare(context, "create procedure") == 0)
                  {
                      Regex.Replace(context, "create procedure", "CREATE PROCEDURE");
                      placementNumber = context.IndexOf("CREATE PROCEDURE");
                  }
                  */
                string dboName = "";
                var dbName = new string[context.Length];
                dbName = context.Split(' ');
                foreach (var value in dbName)
                {
                    if (value.Contains("dbo."))
                        dboName = value;
                }
                context.Replace(dboName, dboName + "_alt");
                sw = new StreamWriter(outputPath + Path.GetFileName(item));
                sw.Write(context);
                sw.Flush();
            }
            // sw.Flush();
        }
        static void Main(string[] args)
        {
            RenameFiles();

            Console.ReadKey();
        }
    }
}

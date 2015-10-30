using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Underwriter_3rdPartyFix
{
    public class Program
    {
        private static string[] listOfFiles = Directory.GetFiles(@"C:\Users\YourName\Desktop\CSV_Folder");
        private static string colName = "ThirdPartyCoverCode";
        private static string toReplace = "ADAM";
        private static string with = "1103";

        public static void Main(string[] args)
        {
            foreach(String file in listOfFiles)
            {
                try
                {
                    //filestream!!!!!!!!!!!!!!
                    var oStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    var iStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var sw = new System.IO.StreamWriter(oStream);
                    var sr = new System.IO.StreamReader(iStream);

                    List<string> newLines = new List<string>();
                    string line = sr.ReadLine();
                    String[] columns = line.Split(',');

                    int replacementColumnNo = findColumnNumber(colName, columns);

                    newLines.Add(line);
                    line = sr.ReadLine();

                    while (line != null)
                    {
                        columns = line.Split(',');

                        if (columns[replacementColumnNo].Contains(toReplace))
                        {
                            columns[replacementColumnNo] = with;
                        }

                        line = String.Join(",", columns);
                        newLines.Add(line);
                        line = sr.ReadLine();
                    }

                    String allLines = String.Join(Environment.NewLine, newLines);

                    sw.Write(allLines);

                    sw.Close();
                    sr.Close();
                    oStream.Close();
                    iStream.Close();
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }
            }
        }

        private static int findColumnNumber(String colName, String[] columns)
        {
            int j = 0;
            int replacementColumnNo = 0;

            foreach (String column in columns)
            {
                if (column.Equals(colName))
                {
                    replacementColumnNo = j;
                }
                j++;
            }

            return replacementColumnNo;
        }

    }
}

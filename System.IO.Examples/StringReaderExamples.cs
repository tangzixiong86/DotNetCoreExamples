using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO.Examples
{
    public class StringReaderExamples
    {
        public static void Run1()
        {
            StringBuilder stringToRead = new StringBuilder();
            stringToRead.AppendLine("Characters in 1st line to read");
            stringToRead.AppendLine("and 2nd line");
            stringToRead.AppendLine("and the end");

            string aLine, aParagraph = null;
            StringReader strReader = new StringReader(stringToRead.ToString());
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                {
                    aParagraph = aParagraph + aLine + " ";
                }
                else
                {
                    aParagraph = aParagraph + "\n";
                    break;
                }
            }
            Console.WriteLine("aParagraph:\n\n{0}", aParagraph);
            using (StringReader reader = new StringReader(stringToRead.ToString()))
            {
                string readText = reader.ReadToEnd();
                Console.WriteLine(readText);
            }
            
        }
    }
}

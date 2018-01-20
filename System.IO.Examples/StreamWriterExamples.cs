using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO.Examples
{
    public class StreamWriterExamples
    {
        public static void Run1()
        {
            // Get the directories currently on the C drive.
            DirectoryInfo[] cDirs = new DirectoryInfo(@"c:\").GetDirectories();

            // Write each directory name to a file.
            using (StreamWriter sw = new StreamWriter("CDriveDirs.txt"))
            {
                foreach (DirectoryInfo dir in cDirs)
                {
                    sw.WriteLine(dir.Name);

                }
            }

            // Read and show each line from the file.
            string line = "";
            using (StreamReader sr = new StreamReader("CDriveDirs.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        public static void Run2()
        {
            string fileName = "test.txt";
            string textToAdd = "Example text in file";
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.CreateNew);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(textToAdd);
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }
        public static void Run3()
        {
            string fileName = "test.txt";
            string textToAdd = "Example text in file";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(textToAdd);
            }
        }
    }
}

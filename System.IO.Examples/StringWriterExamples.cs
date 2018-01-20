using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO.Examples
{
    public class StringWriterExamples
    {
        public static void Run1()
        {
            StringWriter strWriter = new StringWriter();

            // Use the three overloads of the Write method that are 
            // overridden by the StringWriter class.
            strWriter.Write("file path characters are: ");
            strWriter.Write(
                Path.GetInvalidPathChars(), 0, Path.GetInvalidPathChars().Length);
            strWriter.Write('.');

            // Use the underlying StringBuilder for more complex 
            // manipulations of the string.
            strWriter.GetStringBuilder().Insert(0, "Invalid ");

            Console.WriteLine("The following string is {0} encoded.\n{1}",
                strWriter.Encoding.EncodingName, strWriter.ToString());
            StringBuilder sb = new StringBuilder();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic.Examples
{
    public class ASCIIEncodingExample
    {
        public static void Run()
        {
            // The encoding.
            ASCIIEncoding ascii = new ASCIIEncoding();

            // A Unicode string with two characters outside the ASCII code range.
            String unicodeString =
                "This Unicode string contains two characters " +
                "with codes outside the ASCII code range, " +
                "Pi (\u03a0) and Sigma (\u03a3).";
            Console.WriteLine("Original string:");
            Console.WriteLine(unicodeString);

            // Save positions of the special characters for later reference.
            int indexOfPi = unicodeString.IndexOf('\u03a0');
            int indexOfSigma = unicodeString.IndexOf('\u03a3');

            // Encode string.
            Byte[] encodedBytes = ascii.GetBytes(unicodeString);
            Console.WriteLine();
            Console.WriteLine("Encoded bytes:");
            foreach (Byte b in encodedBytes)
            {
                Console.Write("[{0}]", b);
            }
            Console.WriteLine();

            // Notice that the special characters have been replaced with
            // the value 63, which is the ASCII character code for '?'.
            Console.WriteLine();
            Console.WriteLine(
                "Value at position of Pi character: {0}",
                encodedBytes[indexOfPi]
            );
            Console.WriteLine(
                "Value at position of Sigma character: {0}",
                encodedBytes[indexOfSigma]
            );

            // Decode bytes back to string.
            // Notice missing Pi and Sigma characters.
            String decodedString = ascii.GetString(encodedBytes);
            Console.WriteLine();
            Console.WriteLine("Decoded bytes:");
            Console.WriteLine(decodedString);
        }
    }
}

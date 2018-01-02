using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic.Examples
{
    public class EncodingExamples
    {
        public static void EncodingExampes()
        {
            string unicodeString = "This string contains the unicode character Pi(\u03a0)";

            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            Console.WriteLine("Original string: {0}", unicodeString);
            Console.WriteLine("Ascii converted string: {0}", asciiString);

            // Get a UTF-32 encoding by codepage.
            Encoding e1 = Encoding.GetEncoding(12000);

            // Get a UTF-32 encoding by name.
            Encoding e2 = Encoding.GetEncoding("utf-32");

            // Check their equality.
            Console.WriteLine("e1 equals e2? {0}", e1.Equals(e2));

            // Create an encoding, which is equivalent to calling the 
            // ASCIIEncoding class constructor. 
            // The EncoderReplacementFallback parameter specifies that the
            // string, "(unknown)", replace characters that cannot be encoded. 
            // A decoder replacement fallback is also specified, but in this 
            // code example the decoding operation cannot fail.  

            Encoding ae = Encoding.GetEncoding(
                          "us-ascii",
                          new EncoderReplacementFallback("(unknown)"),
                          new DecoderReplacementFallback("(error)"));

            // The input string consists of the Unicode characters LEFT POINTING 
            // DOUBLE ANGLE QUOTATION MARK (U+00AB), 'X' (U+0058), and RIGHT POINTING 
            // DOUBLE ANGLE QUOTATION MARK (U+00BB). 
            // The encoding can only encode characters in the US-ASCII range of U+0000 
            // through U+007F. Consequently, the characters bracketing the 'X' character
            // are replaced with the fallback replacement string, "(unknown)".

            string inputString = "\u00abX\u00bb";
            string decodedString;
            string twoNewLines = "\n\n";
            byte[] encodedBytes = new byte[ae.GetByteCount(inputString)];
            int numberOfEncodedBytes = 0;
            int ix = 0;

            // --------------------------------------------------------------------------
            Console.Clear();

            // Display the name of the encoding.
            Console.WriteLine("The name of the encoding is \"{0}\".\n", ae.WebName);

            // Display the input string in text.
            Console.WriteLine("Input string ({0} characters): \"{1}\"",
                               inputString.Length, inputString);

            // Display the input string in hexadecimal.
            Console.Write("Input string in hexadecimal: ");
            foreach (char c in inputString.ToCharArray())
            {
                Console.Write("0x{0:X2} ", (int)c);
            }
            Console.Write(twoNewLines);

            // --------------------------------------------------------------------------
            // Encode the input string. 

            Console.WriteLine("Encode the input string...");
            numberOfEncodedBytes = ae.GetBytes(inputString, 0, inputString.Length,
                                               encodedBytes, 0);

            // Display the encoded bytes.
            Console.WriteLine("Encoded bytes in hexadecimal ({0} bytes):\n",
                               numberOfEncodedBytes);
            ix = 0;
            foreach (byte b in encodedBytes)
            {
                Console.Write("0x{0:X2} ", (int)b);
                ix++;
                if (0 == ix % 6) Console.WriteLine();
            }
            Console.Write(twoNewLines);

            // --------------------------------------------------------------------------
            // Decode the encoded bytes, yielding a reconstituted string.

            Console.WriteLine("Decode the encoded bytes...");
            decodedString = ae.GetString(encodedBytes);

            // Display the input string and the decoded string for comparison.
            Console.WriteLine("Input string:  \"{0}\"", inputString);
            Console.WriteLine("Decoded string:\"{0}\"", decodedString);
        }
    

        public static void GetBytesExamples()
        {
            // The characters to encode:
            //    Latin Small Letter Z (U+007A)
            //    Latin Small Letter A (U+0061)
            //    Combining Breve (U+0306)
            //    Latin Small Letter AE With Acute (U+01FD)
            //    Greek Small Letter Beta (U+03B2)
            //    a high-surrogate value (U+D8FF)
            //    a low-surrogate value (U+DCFF)
            String myStr = "za\u0306\u01FD\u03B2\uD8FF\uDCFF";

            // Get different encodings.
            Encoding u7 = Encoding.UTF7;
            Encoding u8 = Encoding.UTF8;
            Encoding u16LE = Encoding.Unicode;
            Encoding u16BE = Encoding.BigEndianUnicode;
            Encoding u32 = Encoding.UTF32;

            // Encode the entire string, and print out the counts and the resulting bytes.
            Console.WriteLine("Encoding the entire string:");
            PrintCountsAndBytes(myStr, u7);
            PrintCountsAndBytes(myStr, u8);
            PrintCountsAndBytes(myStr, u16LE);
            PrintCountsAndBytes(myStr, u16BE);
            PrintCountsAndBytes(myStr, u32);

            Console.WriteLine();

            // Encode three characters starting at index 4, and print out the counts and the resulting bytes.
            Console.WriteLine("Encoding the characters from index 4 through 6:");
            PrintCountsAndBytes(myStr, 4, 3, u7);
            PrintCountsAndBytes(myStr, 4, 3, u8);
            PrintCountsAndBytes(myStr, 4, 3, u16LE);
            PrintCountsAndBytes(myStr, 4, 3, u16BE);
            PrintCountsAndBytes(myStr, 4, 3, u32);

        }
        public static void PrintCountsAndBytes(String s, Encoding enc)
        {

            // Display the name of the encoding used.
            Console.Write("{0,-30} :", enc.ToString());

            // Display the exact byte count.
            int iBC = enc.GetByteCount(s);
            Console.Write(" {0,-3}", iBC);

            // Display the maximum byte count.
            int iMBC = enc.GetMaxByteCount(s.Length);
            Console.Write(" {0,-3} :", iMBC);

            // Encode the entire string.
            byte[] bytes = enc.GetBytes(s);

            // Display all the encoded bytes.
            PrintHexBytes(bytes);

        }
        public static void PrintCountsAndBytes(String s, int index, int count, Encoding enc)
        {

            // Display the name of the encoding used.
            Console.Write("{0,-30} :", enc.ToString());

            // Display the exact byte count.
            int iBC = enc.GetByteCount(s.ToCharArray(), index, count);
            Console.Write(" {0,-3}", iBC);

            // Display the maximum byte count.
            int iMBC = enc.GetMaxByteCount(count);
            Console.Write(" {0,-3} :", iMBC);

            // Encode a range of characters in the string.
            byte[] bytes = new byte[iBC];
            enc.GetBytes(s, index, count, bytes, bytes.GetLowerBound(0));

            // Display all the encoded bytes.
            PrintHexBytes(bytes);

        }
        public static void PrintHexBytes(byte[] bytes)
        {

            if ((bytes == null) || (bytes.Length == 0))
                Console.WriteLine("<none>");
            else
            {
                for (int i = 0; i < bytes.Length; i++)
                    Console.Write("{0:X2} ", bytes[i]);
                Console.WriteLine();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System.IO.Examples
{
    public class FileStreamExamples
    {
        public static void Run1()
        {
            string path = @"MyTest.txt";

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());

                }
            }

            //Open the stream and read it back.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
        }
        public static void Run2()
        {
            try
            {
                // Create a file and write data to it.

                // Create an array of bytes.
                byte[] messageByte = Encoding.ASCII.GetBytes("Here is some data.");

                // Create a file using the FileStream class.
                FileStream fWrite = new FileStream("test.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 8, FileOptions.None);

                // Write the number of bytes to the file.
                fWrite.WriteByte((byte)messageByte.Length);

                // Write the bytes to the file.
                fWrite.Write(messageByte, 0, messageByte.Length);

                // Close the stream.
                fWrite.Close();


                // Open a file and read the number of bytes.

                FileStream fRead = new FileStream("test.txt", FileMode.Open);

                // The first byte is the string length.
                int length = (int)fRead.ReadByte();

                // Create a new byte array for the data.
                byte[] readBytes = new byte[length];

                // Read the data from the file.
                fRead.Read(readBytes, 0, readBytes.Length);

                // Close the stream.
                fRead.Close();

                // Display the data.
                Console.WriteLine(Encoding.ASCII.GetString(readBytes));

                Console.WriteLine("Done writing and reading data.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
        public static void Run3()
        {
            // Create a synchronization object that gets 
            // signaled when verification is complete.
            ManualResetEvent manualEvent = new ManualResetEvent(false);

            // Create random data to write to the file.
            byte[] writeArray = new byte[100000];
            new Random().NextBytes(writeArray);

            FileStream fStream =
                new FileStream("Test#@@#.dat", FileMode.Create,
                FileAccess.ReadWrite, FileShare.None, 4096, true);

            // Check that the FileStream was opened asynchronously.
            Console.WriteLine("fStream was {0}opened asynchronously.",
                fStream.IsAsync ? "" : "not ");

            // Asynchronously write to the file.
            IAsyncResult asyncResult = fStream.BeginWrite(
                writeArray, 0, writeArray.Length,
                new AsyncCallback(EndWriteCallback),
                new State(fStream, writeArray, manualEvent));

            // Concurrently do other work and then wait 
            // for the data to be written and verified.
            manualEvent.WaitOne(5000, false);
        }
        // When BeginWrite is finished writing data to the file, the
        // EndWriteCallback method is called to end the asynchronous 
        // write operation and then read back and verify the data.
        static void EndWriteCallback(IAsyncResult asyncResult)
        {
            State tempState = (State)asyncResult.AsyncState;
            FileStream fStream = tempState.FStream;
            fStream.EndWrite(asyncResult);

            // Asynchronously read back the written data.
            fStream.Position = 0;
            asyncResult = fStream.BeginRead(
                tempState.ReadArray, 0, tempState.ReadArray.Length,
                new AsyncCallback(EndReadCallback), tempState);

            // Concurrently do other work, such as 
            // logging the write operation.
        }
        // When BeginRead is finished reading data from the file, the 
        // EndReadCallback method is called to end the asynchronous 
        // read operation and then verify the data.
        static void EndReadCallback(IAsyncResult asyncResult)
        {
            State tempState = (State)asyncResult.AsyncState;
            int readCount = tempState.FStream.EndRead(asyncResult);

            int i = 0;
            while (i < readCount)
            {
                if (tempState.ReadArray[i] != tempState.WriteArray[i++])
                {
                    Console.WriteLine("Error writing data.");
                    tempState.FStream.Close();
                    return;
                }
            }
            Console.WriteLine("The data was written to {0} and verified.",
                tempState.FStream.Name);
            tempState.FStream.Close();

            // Signal the main thread that the verification is finished.
            tempState.ManualEvent.Set();
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        // Maintain state information to be passed to 
        // EndWriteCallback and EndReadCallback.
        class State
        {
            // fStream is used to read and write to the file.
            FileStream fStream;

            // writeArray stores data that is written to the file.
            byte[] writeArray;

            // readArray stores data that is read from the file.
            byte[] readArray;

            // manualEvent signals the main thread 
            // when verification is complete.
            ManualResetEvent manualEvent;

            public State(FileStream fStream, byte[] writeArray,
                ManualResetEvent manualEvent)
            {
                this.fStream = fStream;
                this.writeArray = writeArray;
                this.manualEvent = manualEvent;
                readArray = new byte[writeArray.Length];
            }

            public FileStream FStream
            { get { return fStream; } }

            public byte[] WriteArray
            { get { return writeArray; } }

            public byte[] ReadArray
            { get { return readArray; } }

            public ManualResetEvent ManualEvent
            { get { return manualEvent; } }
        }

    }
}

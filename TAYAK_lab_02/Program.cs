using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TAYAK_lab_02
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                using (StreamReader sr = new StreamReader("var1.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}

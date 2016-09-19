﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TAYAK_lab_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileToOpen;
            string baseAddr = @"C:\Users\admin\Source\Repos\TAYAK_lab2\TAYAK_lab_02\";
            Console.WriteLine("Напишите, какой файл открыть (var1, var2, var3).");
            fileToOpen = Console.ReadLine();
            if (fileToOpen != "var1" && fileToOpen != "var2" && fileToOpen != "var3")
            {
                Console.WriteLine("Неизвестный файл. Закрываюсь");
                Environment.Exit(0);
            }

            try
            { 
                using (StreamReader sr = new StreamReader(baseAddr + fileToOpen + ".txt"))
                {
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

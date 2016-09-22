using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        StateReader.addState(line);
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при открытии файла.");
                Console.WriteLine(e.Message);
            }
            ///////
            foreach (var key in StateReader.stateDic.Keys)
                Console.WriteLine("\tKey: {0} | Value: {1}", key, StateReader.stateDic[key]);
            //////

            StateMachine sm = new StateMachine();

            Console.WriteLine("\nВведите строку символов для разбора:");
            string inCharacterString = Console.ReadLine();
            sm.analyzeString(inCharacterString);

            string outCharacterString;


            try
            {
                outCharacterString = Convert.ToString(inCharacterString);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Ошибка, запустите программу заново");
            }

        }
    }
}

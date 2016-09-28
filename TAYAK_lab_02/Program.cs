using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TAYAK_lab_02 {
    class Program {
        private static string outputArray(List<Tuple<char, int>> list) {
            string str = "";
            int i = 0;
            foreach (var l in list) {
                str = str + list[i].Item1 + Convert.ToString(list[i].Item2) + " ";
                i++;
            }
            return str;
        }
        static void Main(string[] args) {
            string fileToOpen;
            string baseAddr = @"C:\Users\admin\Source\Repos\TAYAK_lab2\TAYAK_lab_02\";

            Console.Write("Напишите, какой файл открыть: ");
            fileToOpen = Console.ReadLine();
            try {
                using (StreamReader sr = new StreamReader(baseAddr + fileToOpen + ".txt")) {
                    String line;
                    StateReader s = new StateReader();
                    while ((line = sr.ReadLine()) != null) {
                        Console.WriteLine(line);
                        s.addState(line);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Ошибка при открытии файла.");
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            StateMachine sm = new StateMachine();
            if (StateReader.isDetermine) {
                Console.WriteLine("\t** Автомат недетерминирован. **");
                foreach (var key in StateReader.stateDic.Keys)
                    Console.WriteLine("\tKey: {0} | Value: {1}", key, Program.outputArray(StateReader.stateDic[key]));
                sm.determine();
            }

            Console.WriteLine("\n\t** Автомат детерминирован. **");
            foreach (var key in StateReader.stateDic.Keys)
                Console.WriteLine("\tKey: {0} | Value: {1}", key, Program.outputArray(StateReader.stateDic[key]));

            Console.WriteLine("\nВведите строку символов для разбора:");
            string inCharacterString = Console.ReadLine();
            sm.analyzeString(inCharacterString);
        }
    }
}

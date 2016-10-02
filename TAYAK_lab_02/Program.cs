using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TAYAK_lab_02 {
    class Program {
        private static string outputArray(List<Tuple<string, int>> list) {
            string str = "";
            for(int i = 0; i !=list.Count; i++)
                str += list[i].Item1 + Convert.ToString(list[i].Item2) + " ";
            return str;
        }
        static void Main(string[] args) {
            StateReader s = new StateReader();
            StateMachine sm = new StateMachine();

            string baseAddr = @"C:\Users\admin\Source\Repos\TAYAK_lab2\TAYAK_lab_02\";
            Console.Write("Напишите, какой файл открыть: ");
            string fileToOpen = Console.ReadLine();

            try {
                using (StreamReader sr = new StreamReader(baseAddr + fileToOpen + ".txt")) {
                    String line;
                    
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

            if(StateReader.hasEpsilon)
            {
                Console.WriteLine("\t** Автомат содержит эпсилон-переходы. **");
                foreach (var key in StateReader.stateDic.Keys)
                    Console.WriteLine("\tKey: {0} | Value: {1}", key, Program.outputArray(StateReader.stateDic[key]));
                sm.killAllEpsilon();
            }

            if (StateReader.isNotDetermined) {
                Console.WriteLine("\t** Автомат недетерминирован. **");
                foreach (var key in StateReader.stateDic.Keys)
                    Console.WriteLine("\tKey: {0} | Value: {1}", key, Program.outputArray(StateReader.stateDic[key]));
                sm.allDetermine();
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

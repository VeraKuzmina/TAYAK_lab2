using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAYAK_lab_02
{
    class StateReader
    {
        //Ключ словаря - кортеж <терминальное или нет состояние, номер состояния, символ>; значение 
        public static Dictionary<Tuple<bool, int, char>, Tuple<bool, int>> stateDic = new Dictionary<Tuple<bool, int, char>, Tuple<bool, int>>();

        public static void addState(string input)
        {
            string pattern = @"[,=]";
            string[] expr = Regex.Split(input, pattern);
            foreach (var elem in expr)
            {
                if (expr[0][0] != 'q' && expr[0][0] != 'Q')
                {
                    throw new FormatException("Строка начинается не с \"q\"");
                }
            }
        }
    }
}

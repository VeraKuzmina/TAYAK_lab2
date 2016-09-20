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
        //Ключ словаря - кортеж <терминальное или нет состояние, номер состояния, символ>; значение -- 
        public static Dictionary<Tuple<int, char>, Tuple<char, int>> stateDic = new Dictionary<Tuple<int, char>, Tuple<char, int>>();

        public static void addState(string input)
        {
            string pattern = ",";
            string[] expr = Regex.Split(input, pattern);
            /*foreach (var elem in expr)
            {
                if (expr[0][0] != 'q' && expr[0][0] != 'Q')
                {
                    throw new FormatException("Строка начинается не с \"q\"");
                }
            }*/
            stateDic.Add( new Tuple<int, char>(int.Parse(expr[0].Substring(1)), expr[1][0]), new Tuple<char, int>(expr[1][2], int.Parse(expr[1].Substring(3))) );
        }

    }
}

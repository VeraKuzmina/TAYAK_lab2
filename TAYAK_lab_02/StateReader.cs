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
        //Ключ словаря - кортеж <номер состояния, символ>; значение -- кортеж <символ состояния, номер состояния>
        public static Dictionary<Tuple<char, int, char>, Tuple<char, int>> stateDic = new Dictionary<Tuple<char, int, char>, Tuple<char, int>>();

        public static void addState(string input)
        {

            /*foreach (var elem in expr)
            {
                if (expr[0][0] != 'q' && expr[0][0] != 'Q')
                {
                    throw new FormatException("Строка начинается не с \"q\"");
                }
            }*/
            int indexOfComma = input.IndexOf(',');
            int curState  = int.Parse(input.Substring(1, indexOfComma - 1 ) );
            int nextState = int.Parse(input.Substring(indexOfComma + 4));
            stateDic.Add(new Tuple<char, int, char>(input[indexOfComma - 2], curState, input[indexOfComma + 1]), new Tuple<char, int>(input[indexOfComma + 3], nextState));
        }

    }
}

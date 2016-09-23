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
        private int curState;
        private int nextState;
        private char inState ;
        private char inChar;
        private bool flag;

        public StateReader()
        {
            curState = 0;
            nextState = 0;
            inState = 'q';
            inChar = 'q';
            flag = false;
        }
        //Ключ словаря - кортеж <номер состояния, символ>; значение -- кортеж <символ состояния, номер состояния>
        public static Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>> stateDic = new Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>>();

        public void addState(string input)
        {
            /*foreach (var elem in expr)
            {
                if (expr[0][0] != 'q' && expr[0][0] != 'Q')
                    throw new FormatException("Строка начинается не с \"q\"");
            }*/
            List <Tuple<char, int>> list = new List<Tuple<char, int>>();
            int indexOfComma = input.IndexOf(',');
            int newCurState = int.Parse(input.Substring(1, indexOfComma - 1));
            char newInChar = input[indexOfComma + 1];
            char newInState = input[indexOfComma - 2];
            if (!flag)
            {
                list.Add(new Tuple<char, int>(input[indexOfComma + 3], int.Parse(input.Substring(indexOfComma + 4))));
                stateDic.Add(new Tuple<char, int, char>(inState, curState, inChar), list);
                flag = true;
            }
            foreach (var key in stateDic.Keys)
            {
                
                if (newInState == inState && newCurState == curState && newInChar == inChar)
                {
                    stateDic[Tuple.Create<char, int, char>(inState, curState, inChar)].Add(new Tuple<char, int>(input[indexOfComma + 3], int.Parse(input.Substring(indexOfComma + 4))));
                    curState = newCurState;
                    nextState = int.Parse(input.Substring(indexOfComma + 4));
                    inState = newInState;
                    inChar = newInChar;
                }
                else
                {
                    curState = newCurState;
                    inState = newInState;
                    inChar = newInChar;
                    list.Add(new Tuple<char, int>(input[indexOfComma + 3], int.Parse(input.Substring(indexOfComma + 4))));
                    stateDic.Add(new Tuple<char, int, char>(inState, curState, inChar), list);
                }   
            }   
        }
    }
}

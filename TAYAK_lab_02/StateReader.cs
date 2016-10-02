using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAYAK_lab_02 {
    class StateReader {
        public static bool isNotDetermined;
        public static bool hasEpsilon;
        public static Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>> stateDic = new Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>>();

        public StateReader() {
            isNotDetermined = false;
            hasEpsilon = false;
        }
        
        public void addState(string input) {

            List<Tuple<char, int>> list = new List<Tuple<char, int>>();
            int indexOfComma = input.IndexOf(',');
            char inChar = input[indexOfComma + 1];
            char inState = input[0];
            int curState = int.Parse(input.Substring(1, indexOfComma - 1));
            int  nextState = int.Parse(input.Substring(indexOfComma + 4));

            if (inChar == '~') hasEpsilon = true;

            var keys = stateDic.Keys;
            if (keys.Count == 0 || !(stateDic.ContainsKey(Tuple.Create<char,int,char>(inState, curState, inChar) )) )
            {
                list.Add(Tuple.Create<char, int>(input[indexOfComma + 3], nextState));
                stateDic.Add(Tuple.Create<char, int, char>(inState, curState, inChar), list);
            }
            else
            {
                stateDic[Tuple.Create<char, int, char>(inState, curState, inChar)].Add(Tuple.Create<char, int>(input[indexOfComma + 3], nextState));
                isNotDetermined = true;
            }
               
        }
    }
}

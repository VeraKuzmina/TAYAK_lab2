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
        public static Dictionary<Tuple<string, int, char>, List<Tuple<string, int>>> stateDic = new Dictionary<Tuple<string, int, char>, List<Tuple<string, int>>>();

        public StateReader() {
            isNotDetermined = false;
            hasEpsilon = false;
        }
        
        public void addState(string input) {

            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            int indexOfComma = input.IndexOf(',');
            char inChar = input[indexOfComma + 1];
            string inState = input[0].ToString();
            int curState = int.Parse(input.Substring(1, indexOfComma - 1));
            int  nextState = int.Parse(input.Substring(indexOfComma + 4));

            if ((inState.Equals(input[indexOfComma + 3].ToString())) && (curState == nextState) && (inChar == '~'))
            {
                Console.WriteLine("не учитываем");
                return;
            }

            if (inChar == '~') hasEpsilon = true;

            var keys = stateDic.Keys;
            if (keys.Count == 0 || !(stateDic.ContainsKey(Tuple.Create<string,int,char>(inState, curState, inChar) )) ) {
                list.Add(Tuple.Create<string, int>(input[indexOfComma + 3].ToString(), nextState));
                stateDic.Add(Tuple.Create<string, int, char>(inState, curState, inChar), list);
            }
            else {
                stateDic[Tuple.Create<string, int, char>(inState, curState, inChar)].Add(Tuple.Create<string, int>(input[indexOfComma + 3].ToString(), nextState));
                isNotDetermined = true;
            }
               
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAYAK_lab_02 {
    class StateReader {
        private bool flag, flag2;
        public static bool isDetermine;
        public static bool hasEpsilon;
        public static Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>> stateDic = new Dictionary<Tuple<char, int, char>, List<Tuple<char, int>>>();

        public StateReader() {
            flag = false;
            isDetermine = false;
            hasEpsilon = false;
        }
        
        public void addState(string input) {

            List<Tuple<char, int>> list = new List<Tuple<char, int>>();
            int indexOfComma = input.IndexOf(',');
            char inChar = input[indexOfComma + 1];
            char inState = input[0];
            int curState = int.Parse(input.Substring(1, indexOfComma - 1));
            int  nextState = int.Parse(input.Substring(indexOfComma + 4));

          //  if ((inState == input[indexOfComma + 3]) && (inChar == '~'))
              //  return;

            if (inChar == '~') hasEpsilon = true;

            if (!flag) {
                list.Add(new Tuple<char, int>(input[indexOfComma + 3], nextState));
                stateDic.Add(new Tuple<char, int, char>(inState, curState, inChar), list);
                flag = true;
            }
            else {
                var keys = stateDic.Keys;
                flag2 = true;
                foreach (var key in keys) {
                    if (inState == key.Item1 && curState == key.Item2 && inChar == key.Item3) {
                        stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Add(new Tuple<char, int>(input[indexOfComma + 3], nextState));
                        flag2 = false;
                        isDetermine = true;
                    }
                }
            }
            if (flag2) {
                list.Add(new Tuple<char, int>(input[indexOfComma + 3], nextState));
                stateDic.Add(new Tuple<char, int, char>(inState, curState, inChar), list);
            }
        }
    }
}

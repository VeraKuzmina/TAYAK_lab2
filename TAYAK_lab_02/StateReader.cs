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
            var indexOfComma = input.IndexOf(',');
            var inChar       = input[indexOfComma + 1];
            var inState      = input[0].ToString();
            var curState     = int.Parse(input.Substring(1, indexOfComma - 1));
            var nextState    = int.Parse(input.Substring(indexOfComma + 4));
            var currentKey   = Tuple.Create<string, int, char>(inState, curState, inChar);

            if ((inState.Equals(input[indexOfComma + 3].ToString())) && (curState == nextState) && (inChar == '~'))
            {
                Console.WriteLine("не учитываем");
                return;
            }

            if (inChar == '~') hasEpsilon = true;

            var keys = stateDic.Keys;
            if (keys.Count == 0 || !( stateDic.ContainsKey(currentKey) ) ) {
                list.Add(Tuple.Create<string, int>(input[indexOfComma + 3].ToString(), nextState));
                stateDic.Add(currentKey, list);
            }
            else {
                stateDic[currentKey].Add(Tuple.Create<string, int>(input[indexOfComma + 3].ToString(), nextState));
                isNotDetermined = true;
            }
               
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAYAK_lab_02 {
    class StateMachine {
        private int stateNumber;
        private bool isStateTerminal, flag;
        private char stateCharacter;

        public StateMachine() {
            stateNumber = 0;
            stateCharacter = 'q';
            isStateTerminal = false;
            flag = false;
        }

        private void readCharacter(int i, string inString, char inChar) {
            try {
                int new_stateNumber = StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item2;
                char new_stateCharacter = StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1;
                if (i == inString.Length - 1)
                    if (StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1 == 'f' || StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1 == 'F')
                        isStateTerminal = true;
                stateNumber = new_stateNumber;
                stateCharacter = new_stateCharacter;
            }
            catch (Exception e) {
                Console.WriteLine("Нет перехода.");
                flag = true;
            }
        }

        public void analyzeString(string inString) {
            int i = 0;
            for (i = 0; i < inString.Length && !isStateTerminal; ++i)
                readCharacter(i, inString, inString[i]);
            if (i == inString.Length && isStateTerminal && !flag)
                Console.WriteLine("Строку возможно разобрать.");
            else if (i < inString.Length && isStateTerminal)
                Console.WriteLine("Автомат разобрал не всю строку.");
            else
                Console.WriteLine("Автомат не сможет разобрать строку.");
        }

        public void determine()  {
            var keys = StateReader.stateDic.Keys;
            foreach (var key in keys) {
                if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1) {
                    List<Tuple<char, int>> list = StateReader.stateDic[key];
                    int i = 0;
                    string str = "";
                    List<char> arrayState = new List<char>();
                    bool f = false;
                    foreach (var l in list) {
                        if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1 == 'f')
                            f = true;
                        arrayState.Add(StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1);
                        str = str +  Convert.ToString(list[i].Item2);
                        i++;
                    }
                    for (int j = 0; j<i; j++)
                         list.RemoveAt(0);
                    char inState;
                    if (f) inState = 'F';
                    else inState = 'Q';
                    
                    StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Add(new Tuple<char, int>(inState, int.Parse(str)));

                    List<Tuple<char, int>> newState = new List<Tuple<char, int>>();
                    int k = 0;
                    foreach (var s in str)
                    {
                        Console.Write(s);
                        var keys2 = StateReader.stateDic.Keys;
                        int t = 0;
                        foreach (var key2 in keys2)
                        {
                            //   if (StateReader.stateDic[Tuple.Create<char, int, char>(key2.Item1, key2.Item2, key2.Item3)][t].Item1 == arrayState[k] &&
                            //     StateReader.stateDic[Tuple.Create<char, int, char>(key2.Item1, key2.Item2, key2.Item3)][t].Item2 == Convert.ToInt32(s))
                            // {
                            // }
                            t++;
                        }
                        k++;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

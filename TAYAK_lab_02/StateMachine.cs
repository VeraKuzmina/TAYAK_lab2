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

        public void killAllEpsilon()
        {
            bool flag = false; 
            var keys = StateReader.stateDic.Keys;
            while (!flag)
            {
                flag = true;
                foreach (var key in keys)
                {
                    if (key.Item3 == '~')
                        flag = false;
                    
                }
                if (!flag)
                    killEpsilon();
            }
        }

        public static void killEpsilon()
        {
            List<Tuple<char, int>> newState = new List<Tuple<char, int>>();
            List<Tuple<char, int, char>> newState2 = new List<Tuple<char, int, char>>();
            List<Tuple<char, int>> isState = new List<Tuple<char, int>>();
            var keys = StateReader.stateDic.Keys;
            foreach (var key in keys)
            {
                if (key.Item3 == '~')
                {
                    isState.Add(new Tuple<char, int>(key.Item1, key.Item2));
                    if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                    {
                        List<Tuple<char, int>> list = StateReader.stateDic[key];
                        int i = 0;
                        foreach (var l in list)
                        {
                            newState.Add(new Tuple<char, int>(StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1,
                            StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item2));
                            i++;
                        }

                    }
                    else
                        newState.Add(new Tuple<char, int>(StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][0].Item1,
                           StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][0].Item2));
                    break;
                }
            }
            foreach (var key in keys)
            {
                if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                {
                    List<Tuple<char, int>> list = StateReader.stateDic[key];
                    int i = 0;
                    foreach (var l in list)
                    {
                        if ((StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1 == isState[0].Item1) &&
                        (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item2 == isState[0].Item2))
                        {
                            newState2.Add(new Tuple<char, int, char>(key.Item1, key.Item2, key.Item3));
                        }
                        i++;
                    }
                }
                else
                {
                    if ((StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][0].Item1 == isState[0].Item1) &&
                    (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][0].Item2 == isState[0].Item2))
                    {
                        newState2.Add(new Tuple<char, int, char>(key.Item1, key.Item2, key.Item3));
                    }
                }
            }
            bool flag = true;
            if (newState2.Count > 0)
            {
                for (int l = 0; l < newState2.Count(); l++)
                {
                    for (int n = 0; n < newState.Count(); n++)
                    {
                        for (int w = 0; w < StateReader.stateDic[Tuple.Create<char, int, char>(newState2[l].Item1, newState2[l].Item2, newState2[l].Item3)].Count; w++)
                        {
                            if (StateReader.stateDic[Tuple.Create<char, int, char>(newState2[l].Item1, newState2[l].Item2, newState2[l].Item3)][w] == newState[n])
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                            StateReader.stateDic[Tuple.Create<char, int, char>(newState2[l].Item1, newState2[l].Item2, newState2[l].Item3)].Add(new Tuple<char, int>(newState[n].Item1, newState[n].Item2));
                    }
                }
            }
            StateReader.stateDic.Remove(Tuple.Create<char, int, char>(isState[0].Item1, isState[0].Item2, '~'));
        }

        public void determine()  {
            var keys = StateReader.stateDic.Keys;

            List<string> str = new List<string>();
            List<string> arrayState = new List<string>();
            char[] inState = new char[100];
            char ch1, ch2;
            int t = 0;
            int j = 0;
            foreach (var key in keys)
            {
                if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                {
                    List<Tuple<char, int>> list = StateReader.stateDic[key];
                    int i = 0;
                    bool f = false;
                    str.Add("");
                    arrayState.Add("");
                    foreach (var l in list)
                    {
                        if (StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1 == 'f')
                            f = true;
                        arrayState[j] = arrayState[j] + StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1;
                        str[j] = str[j] + Convert.ToString(list[i].Item2);
                        i++;
                    }
                    if (str[j][0] == '0')
                    {
                        ch1 = str[j][1];
                        str[j] = ch1 + "0" + str[j].Substring(2);
                        ch1 = arrayState[j][0];
                        ch2 = arrayState[j][1];
                        arrayState[j] = ch2.ToString() + ch1.ToString() + arrayState[j].Substring(2);
                    }
                    if (f) inState[j] = 'F';
                    else inState[j] = 'Q';
                    for (int q = 0; q < i; q++)
                        list.RemoveAt(0);
                    StateReader.stateDic[Tuple.Create<char, int, char>(key.Item1, key.Item2, key.Item3)].Add(new Tuple<char, int>(inState[j], int.Parse(str[j])));
                    j++;
                }
            }
            for (int y = 0; y < str.Count; y++)
            {
                List<Tuple<char, int, char>> newState = new List<Tuple<char, int, char>>();
                int k = 0;
                foreach (var s in str[y])
                {
                    var keys2 = StateReader.stateDic.Keys;
                    foreach (var key2 in keys2)
                    {
                        if (key2.Item1 == arrayState[y][k] && key2.Item2 == (Convert.ToInt32(s) - 48))
                        {
                            newState.Add(new Tuple<char, int, char>(StateReader.stateDic[Tuple.Create<char, int, char>(key2.Item1, key2.Item2, key2.Item3)][0].Item1,
                                StateReader.stateDic[Tuple.Create<char, int, char>(key2.Item1, key2.Item2, key2.Item3)][0].Item2, key2.Item3));
                        }
                    }
                    k++;
                }
                for (int l = 0; l < newState.Count(); l++)
                {
                    List<Tuple<char, int>> list = new List<Tuple<char, int>>();
                    list.Add(new Tuple<char, int>(newState[l].Item1, newState[l].Item2));
                    if (StateReader.stateDic.ContainsKey(new Tuple<char, int, char>(inState[y], int.Parse(str[y]), newState[l].Item3)))
                    { }    
                    else
                        StateReader.stateDic.Add(new Tuple<char, int, char>(inState[y], int.Parse(str[y]), newState[l].Item3), list);
                }
            }
        }
    }
}

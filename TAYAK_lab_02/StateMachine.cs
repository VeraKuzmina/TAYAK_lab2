using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAYAK_lab_02
{
    class StateMachine
    {
        private int stateNumber;
        private bool isStateTerminal, isNotTransition, flag;
        private string stateCharacter;

        public StateMachine()
        {
            stateNumber = 0;
            stateCharacter = "q";
            flag = false;
            isStateTerminal = false;
            isNotTransition = false;
        }

        private void readCharacter(int i, string inString, char inChar)
        {
            try
            {
                int new_stateNumber = StateReader.stateDic[Tuple.Create<string, int, char>(stateCharacter, stateNumber, inChar)][0].Item2;
                string new_stateCharacter = StateReader.stateDic[Tuple.Create<string, int, char>(stateCharacter, stateNumber, inChar)][0].Item1.ToString();
                if ((i == inString.Length - 1) && (StateReader.stateDic[Tuple.Create<string, int, char>(stateCharacter, stateNumber, inChar)][0].Item1.Contains("f")))
                    isStateTerminal = true;
                if ((i == inString.Length - 1) && (StateReader.stateDic[Tuple.Create<string, int, char>(stateCharacter, stateNumber, inChar)][0].Item1.Equals("q"))
                        && (StateReader.stateDic[Tuple.Create<string, int, char>(stateCharacter, stateNumber, inChar)][0].Item2 == 0) && flag)
                    isStateTerminal = true;
                stateNumber = new_stateNumber;
                stateCharacter = new_stateCharacter;
            }
            catch (Exception e)
            {
                Console.WriteLine("Нет перехода.");
                isNotTransition = true;
            }
        }

        public void analyzeString(string inString)
        {
            int i = 0;
            for (i = 0; i < inString.Length; ++i)
                readCharacter(i, inString, inString[i]);
            if (i == inString.Length && isStateTerminal && !isNotTransition)
                Console.WriteLine("Строку возможно разобрать.");
            else
                Console.WriteLine("Автомат не сможет разобрать строку.");
        }

        public void killAllEpsilon()
        {
            startsWithEpsilonAll();
            bool hasEpsilon = false;
            var keys = StateReader.stateDic.Keys;
            while (!hasEpsilon)
            {
                hasEpsilon = true;
                foreach (var key in keys)
                    if (key.Item3 == '~') hasEpsilon = false;
                if (!hasEpsilon) killEpsilon();
            }
        }

        public void startsWithEpsilonAll()
        {
            bool hasStartsWithEpsilon = false;
            var keys = StateReader.stateDic.Keys;
            while (!hasStartsWithEpsilon)
            {
                hasStartsWithEpsilon = true;
                foreach (var key in keys)
                    if ((key.Item1 == "q") && (key.Item2 == 0) && (key.Item3 == '~')) hasStartsWithEpsilon = false;
                if (!hasStartsWithEpsilon) startsWithEpsilon();
            }
        }

        public void startsWithEpsilon()
        {
            List<Tuple<string, int>> initialState = new List<Tuple<string, int>>();
            var keys = StateReader.stateDic.Keys;
            foreach (var key in keys)
            {
                if ((key.Item1.Equals("q")) && (key.Item2 == 0) && (key.Item3 == '~'))
                {
                    List<Tuple<string, int>> list = StateReader.stateDic[key];
                    for (int j = 0; j != list.Count; j++)
                    {
                        initialState.Add(new Tuple<string, int>(StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][j].Item1,
                            StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][j].Item2));
                        if (StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][j].Item1.Equals("f"))
                            flag = true;
                    }
                }
            }

            List<Tuple<string, int, char>> newState = new List<Tuple<string, int, char>>();
            for (int y = 0; y < initialState.Count; y++)
            {
                var keys2 = StateReader.stateDic.Keys;
                foreach (var key2 in keys2)
                {
                    if (key2.Item1.Equals(initialState[y].Item1) && key2.Item2 == initialState[y].Item2)
                    {
                        List<Tuple<string, int>> list = StateReader.stateDic[key2];
                        int i = 0;
                        foreach (var l in list)
                        {
                            newState.Add(new Tuple<string, int, char>(StateReader.stateDic[Tuple.Create<string, int, char>(key2.Item1, key2.Item2, key2.Item3)][i].Item1,
                            StateReader.stateDic[Tuple.Create<string, int, char>(key2.Item1, key2.Item2, key2.Item3)][i].Item2, key2.Item3));
                            i++;
                        }
                    }
                }
            }
            if (StateReader.stateDic.ContainsKey(new Tuple<string, int, char>("q", 0, '~')))
                StateReader.stateDic.Remove(Tuple.Create<string, int, char>("q", 0, '~'));
            
            for (int l = 0; l < newState.Count(); l++)
            {
                List<Tuple<string, int>> list2 = new List<Tuple<string, int>>();
                list2.Add(new Tuple<string, int>(newState[l].Item1, newState[l].Item2));
                    if (!(StateReader.stateDic.ContainsKey(new Tuple<string, int, char>("q", 0, newState[l].Item3))))
                    StateReader.stateDic.Add(new Tuple<string, int, char>("q", 0, newState[l].Item3), list2);
                else
                {
                    if (!(StateReader.stateDic[Tuple.Create<string, int, char>("q", 0, newState[l].Item3)].Contains(list2[0])))
                        StateReader.stateDic[Tuple.Create<string, int, char>("q", 0, newState[l].Item3)]
                        .Add(Tuple.Create<string, int>(newState[l].Item1, newState[l].Item2));
                }
            }
            StateMachine.isNotDetermined();
        }

        public static void killEpsilon()
        {
            List<Tuple<string, int>> newState = new List<Tuple<string, int>>();
            List<Tuple<string, int, char>> newState2 = new List<Tuple<string, int, char>>();
            List<Tuple<string, int>> isState = new List<Tuple<string, int>>();
            var keys = StateReader.stateDic.Keys;
            foreach (var key in keys)
            {
                if (key.Item3 == '~')
                {
                    isState.Add(new Tuple<string, int>(key.Item1, key.Item2));
                    List<Tuple<string, int>> list = StateReader.stateDic[key];
                    for (int i = 0; i != list.Count; i++)
                        newState.Add(new Tuple<string, int>(StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1,
                            StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][i].Item2));
                    break;
                }
            }
            foreach (var key in keys)
            {
                List<Tuple<string, int>> list = StateReader.stateDic[key];
                for (int i = 0; i != list.Count; i++)
                {
                    if ((StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][i].Item1 == isState[0].Item1) &&
                    (StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)][i].Item2 == isState[0].Item2))
                        newState2.Add(new Tuple<string, int, char>(key.Item1, key.Item2, key.Item3));
                }
            }
            bool hasState = true;
            if (newState2.Count > 0)
            {
                for (int l = 0; l < newState2.Count(); l++)
                {
                    for (int n = 0; n < newState.Count(); n++)
                    {
                        for (int w = 0; w < StateReader.stateDic[Tuple.Create<string, int, char>(newState2[l].Item1.ToString(), newState2[l].Item2, newState2[l].Item3)].Count; w++)
                        {
                            if (StateReader.stateDic[Tuple.Create<string, int, char>(newState2[l].Item1.ToString(), newState2[l].Item2, newState2[l].Item3)][w] == newState[n])
                                hasState = false;
                        }
                        if (hasState)
                            StateReader.stateDic[Tuple.Create<string, int, char>(newState2[l].Item1.ToString(), newState2[l].Item2, newState2[l].Item3)].Add(new Tuple<string, int>(newState[n].Item1, newState[n].Item2));
                    }
                }
            }
            StateReader.stateDic.Remove(Tuple.Create<string, int, char>(isState[0].Item1.ToString(), isState[0].Item2, '~'));
            StateMachine.isNotDetermined();
        }
        public static void isNotDetermined()
        {
            var keys = StateReader.stateDic.Keys;
            StateReader.isNotDetermined = false;
            foreach (var key in keys)
            {
                if (StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                    StateReader.isNotDetermined = true;
            }
        }

        public void allDetermine()
        {
            bool hasDetermine = false;
            var keys = StateReader.stateDic.Keys;
            while (!hasDetermine)
            {
                hasDetermine = true;
                foreach (var key in keys)
                {
                    if (StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                        hasDetermine = false;
                }
                if (!hasDetermine) determine();
            }
        }

        public void determine()
        {
            var keys = StateReader.stateDic.Keys;

            List<List<int>> str = new List<List<int>>();
            List<List<string>> arrayState = new List<List<string>>();
            int j = 0;
            foreach (var key in keys)
            {
                if (StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)].Count > 1)
                {
                    List<Tuple<string, int>> list = StateReader.stateDic[key];
                    str.Add(new List<int>());
                    arrayState.Add(new List<string>());
                    int i = 0;
                    int index, index2;
                    bool addflag = true;
                    for (i = 0; i != list.Count; i++)
                    {
                        for (int y = 0; y < arrayState[j].Count; y++)
                        {
                            for (int k = 0; k < arrayState[j][y].Length; k++)
                            {
                                if (arrayState[j][y].Substring(k).Length >= list[i].Item1.Length)
                                {
                                    if (arrayState[j][y].Substring(k).Contains(list[i].Item1))
                                    {
                                        index = arrayState[j][y].Substring(k).IndexOf(list[i].Item1);
                                        if (index >= 0)
                                        {
                                            if (str[j][y].ToString().Substring(k).Contains(list[i].Item2.ToString()))
                                            {
                                                index2 = str[j][y].ToString().Substring(k).IndexOf(list[i].Item2.ToString());
                                                if (index == index2)
                                                    addflag = false;
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                        if (addflag)
                        {
                            arrayState[j].Add(list[i].Item1);
                            str[j].Add(list[i].Item2);
                        }
                    }

                    int number;
                    string state;
                    bool flag = false;
                    while (!flag)
                    {
                        flag = true;
                        for (int r = 0; r < str[j].Count - 1; r++)
                            if (str[j][r] > str[j][r + 1])
                            {
                                number = str[j][r];
                                str[j][r] = str[j][r + 1];
                                str[j][r + 1] = number;
                                state = arrayState[j][r];
                                arrayState[j][r] = arrayState[j][r+1];
                                arrayState[j][r + 1] = state;
                               flag = false;
                            }
                    }

                    if (str[j][0] == 0)
                    {
                        number = str[j][0];
                        str[j][0] = str[j][1];
                        str[j][1] = number;
                        state = arrayState[j][0];
                        arrayState[j][0] = arrayState[j][1];
                        arrayState[j][1] = state;
                    }

                    for (int q = 0; q < i; q++)
                        list.RemoveAt(0);
                    string numb = "";
                    for (int l = 0; l < str[j].Count; l++)
                        numb = numb + str[j][l].ToString();
                    string state2 = "";
                    for (int l = 0; l < arrayState[j].Count; l++)
                        state2 = state2 + arrayState[j][l].ToString();
                    StateReader.stateDic[Tuple.Create<string, int, char>(key.Item1, key.Item2, key.Item3)].Add(new Tuple<string, int>(state2, int.Parse(numb)));
                    j++;
                }
            }

            for (int y = 0; y < str.Count; y++)
            {
                string numb = "";
                for (int l = 0; l < str[y].Count; l++)
                    numb = numb + str[y][l].ToString();
                string state2 = "";
                for (int l = 0; l < arrayState[y].Count; l++)
                    state2 = state2 + arrayState[y][l].ToString();

                List<Tuple<string, int, char>> newState = new List<Tuple<string, int, char>>();
                for (int k = 0; k < str[y].Count; k++)
                {
                    var keys2 = StateReader.stateDic.Keys;
                    foreach (var key2 in keys2)
                    {
                        if ((key2.Item1.Equals(arrayState[y][k].ToString())) && (key2.Item2 == str[y][k]))
                        {
                                newState.Add(new Tuple<string, int, char>(StateReader.stateDic[Tuple.Create<string, int, char>(key2.Item1, key2.Item2, key2.Item3)][0].Item1,
                                StateReader.stateDic[Tuple.Create<string, int, char>(key2.Item1, key2.Item2, key2.Item3)][0].Item2, key2.Item3));
                        }
                    }
                }

                for (int l = 0; l < newState.Count(); l++)
                {
                    List<Tuple<string, int>> list = new List<Tuple<string, int>>();
                    list.Add(new Tuple<string, int>(newState[l].Item1, newState[l].Item2));
                    if (!(StateReader.stateDic.ContainsKey(new Tuple<string, int, char>(state2, int.Parse(numb), newState[l].Item3))))
                    {
                        StateReader.stateDic.Add(new Tuple<string, int, char>(state2, int.Parse(numb), newState[l].Item3), list);
                    }
                    else
                    {
                        if (!((StateReader.stateDic[Tuple.Create<string, int, char>(state2, int.Parse(numb), newState[l].Item3)][0].Item1 == newState[l].Item1)
                            && (StateReader.stateDic[Tuple.Create<string, int, char>(state2, int.Parse(numb), newState[l].Item3)][0].Item2 == newState[l].Item2)))
                            StateReader.stateDic[Tuple.Create<string, int, char>(state2, int.Parse(numb), newState[l].Item3)]
                            .Add(Tuple.Create<string, int>(newState[l].Item1, newState[l].Item2));
                    }
                }
            }
        }
    }
}

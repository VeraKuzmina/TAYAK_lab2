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
        private bool isStateTerminal, flag;
        private char stateCharacter;

        public StateMachine()
        {
            stateNumber = 0;
            stateCharacter = 'q';
            isStateTerminal = false;
            flag = false;
        }

        private void readCharacter(int i, string inString, char inChar)
        {
            try
            {
                int new_stateNumber = StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item2;
                char new_stateCharacter = StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1;
                if (i == inString.Length - 1)
                    if (StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1 == 'f' || StateReader.stateDic[Tuple.Create<char, int, char>(stateCharacter, stateNumber, inChar)][0].Item1 == 'F')
                        isStateTerminal = true;
                stateNumber = new_stateNumber;
                stateCharacter = new_stateCharacter;
            }
            catch (Exception e)
            {
                Console.WriteLine("Нет перехода.");
                flag = true;
            }
        }

        public void analyzeString(string inString)
        {
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
    }
}

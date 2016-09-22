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
        private bool isStateTerminal;

        public StateMachine()
        {
            stateNumber = 0;
            isStateTerminal = false;
        }

        private void readCharacter(char inChar)
        {
            try
            {
                int new_stateNumber = StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item2;
                if (StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item1 == 'f' || StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item1 == 'F')
                    isStateTerminal = true;
                stateNumber = new_stateNumber;
            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то пошло не так.");
                throw;
            }
        }

        public void analyzeString(string inString)
        {
            int i = 0;
            for (i = 0; i < inString.Length && !isStateTerminal; ++i)
                readCharacter(inString[i]);
            if (i == inString.Length && isStateTerminal)
                Console.WriteLine("Строку возможно разобрать.");
            else if (i < inString.Length && isStateTerminal)
                Console.WriteLine("Автомат разобрал не всю строку.");
            else
                Console.WriteLine("Автомат не сможет разобрать строку.");
        }
    }
}

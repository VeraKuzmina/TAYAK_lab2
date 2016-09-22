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

        public void readCharacter(char inChar)
        {
            try
            {
                stateNumber = StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item2;
                if (StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item1 == 'f' || StateReader.stateDic[Tuple.Create<int, char>(stateNumber, inChar)].Item1 == 'F')
                    isStateTerminal = true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то пошло не так.");
                throw;
            }
        }
    }
}

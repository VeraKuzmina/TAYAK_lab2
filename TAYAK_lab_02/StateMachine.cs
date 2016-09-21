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
    }
}

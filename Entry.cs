using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling_Algorithms
{
    class Entry
    {
        private decimal enter_Time=0;
        private decimal exit_Time=0;

        public decimal Enter_Time { get => enter_Time; set => enter_Time = value; }
        public decimal Exit_Time { get => exit_Time; set => exit_Time = value; }
        public Entry(decimal enter_Time)
        {
            this.enter_Time = enter_Time;
        }

    }
}

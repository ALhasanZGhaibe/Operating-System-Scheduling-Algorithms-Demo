using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling_Algorithms
{
    public class Process
    {
        private int id;
        private String name;
        private decimal arriveTime;
        private decimal burstTime;

        private decimal priority;
        private int readyQueueNumber;

        private List<Entry> entries;
        private decimal duration =0;
        private decimal remainingTime=0;
        private bool started = false;
        private bool finished = false;
        private decimal compeletionTime;
        private decimal turnArroundTime;
        private decimal waitTime;
        //Calculate the time the process spends in cpu..
        private decimal roundRobinTimer = 3;

        //represent the first execution time 
        private decimal startTime;

        private decimal responseTime;

        public Process(int id=0, String name = "",decimal arriveTime = 0, decimal burstTime = 0, decimal priority = 0,
            int readyQueueNumber = 1)
        {
            this.id = id;
            this.name = name;
            this.arriveTime = arriveTime;
            this.burstTime = burstTime;
            this.priority = priority;
            this.readyQueueNumber = readyQueueNumber;
            this.RemainingTime = burstTime;
            this.entries = new List<Entry>();
            //this.compeletionTime = compeletionTime;
            //this.turnArroundTime = turnArroundTime;
            //this.waitTime = waitTime;
            //this.startTime = startTime;
            //this.responseTime = responseTime;
        }

        public decimal ArriveTime { get => arriveTime; set => arriveTime = value; }
        public decimal BurstTime { get => burstTime; set => burstTime = value; }
        public decimal Priority { get => priority; set => priority = value; }
        public int ReadyQueueNumber { get => readyQueueNumber; set => readyQueueNumber = value; }
        public decimal CompeletionTime { get => compeletionTime; set => compeletionTime = value; }
        public decimal TurnArroundTime { get => turnArroundTime; set => turnArroundTime = value; }
        public decimal WaitTime { get => waitTime; set => waitTime = value; }
        public decimal StartTime { get => startTime; set => startTime = value; }
        public decimal ResponseTime { get => responseTime; set => responseTime = value; }
        public string Name { get => name; set => name = value; }
        public decimal Duration { get => duration; set => duration = value; }
        public bool Finished { get => finished; set => finished = value; }
        internal List<Entry> Entries { get => entries; set => entries = value; }
        public bool Started { get => started; set => started = value; }
        public decimal RemainingTime { get => remainingTime; set => remainingTime = value; }
        public int Id { get => id; set => id = value; }
        public decimal RoundRobinTimer { get => roundRobinTimer; set => roundRobinTimer = value; }

        public void Update_StartTime(decimal current_Time)
        {
            this.Started = true;
            this.StartTime = current_Time;
            //this.Entries.Add(new Entry(current_Time));
        }

        public void Pause(decimal current_Time)
        {
            //this.Entries[this.Entries.Count - 1].Exit_Time = current_Time;
        }

        public void Continue(decimal current_Time)
        {
            //this.Entries.Add(new Entry(current_Time));
        }

        public void Finish(decimal current_Time)
        {
            this.Finished = true;
            //Calculate The Values since the process finished...
            this.CompeletionTime = current_Time;
            this.turnArroundTime = this.CompeletionTime - this.arriveTime;
            this.waitTime = this.turnArroundTime - this.burstTime;
           // this.Entries[this.Entries.Count - 1].Exit_Time = current_Time;

        }

        public void update_duration(decimal time_stamp = 1)
        {
            this.Duration += time_stamp;
        }
        public decimal Update_RemainingTime()
        {
            this.RemainingTime = this.burstTime - this.Duration;
            return this.RemainingTime;
        }

        public bool getFinished()
        {
            if (this.RemainingTime <= 0)
            {
                this.Finished = true;
            }
            else this.Finished = false;

            return Finished;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduling_Algorithms
{
    public partial class Form1 : Form
    {
        private NumericUpDown[,] process_Params_UI;
        private CheckBox[] processes_CheckBoxes;
        private TextBox[] readyQueueOneUI;
        private TextBox[] readyQueueTwoUI;
        private TextBox[] readyQueueThreeUI;
        private Process[] processes;
        private List<Process> processes_List;
        private List<Process> readyQueueOne;
        private List<Process> readyQueueTwo;
        private List<Process> readyQueueThree;
        private List<Process> processingUnit;
        private List<Process> finished_processes_List;
        private int currentTime = 0;
        //0 = FCFS , 1 = SJF(non preamptive) , 2 = Priority (non preamptive, 
        private int scheduling_AlgorithmQ1 = 0, scheduling_AlgorithmQ2 = 0,
            scheduling_AlgorithmQ3 = 0;
        private String[] scheduling_Algorithms = new string[]{ "FCFS", "SJF", "SRTF", "PriorityNonpreamtiveS", "PriorityPreamtiveS", "RoundRobinS" };
        //First Come First Served.
        private const int FCFS = 0;
        //Shortest job first.
        private const int SJF = 1;
        //Shortest remaining time first.
        private const int SRTF = 2;
        //Nonpreamptive Priority Schedualing.
        private const int PriorityNonpreamtiveS = 3;
        //Preamptive Priority Schedualing.
        private const int PriorityPreamtiveS = 4;
        //Round Robin Schedualing.
        private const int RoundRobinS = 5;
        private decimal QuantomTime1 = 3;
        private decimal QuantomTime2 = 3;
        private decimal QuantomTime3 = 3;

        public Form1()
        {
            InitializeComponent();
            initializeApplication();
            generate_Random_Processes(5);
            //FCFS_Scheduling(processes_List);
        }

        public void FCFS_Scheduling(List<Process> _processes_List)
        {
            //Sort by Arrive time.
            //Excute by Burst Time in sequential order.
            //Calculate Completion, wait and TurnAroundTime. 
            List<Process> sorted_processes = _processes_List;
            sorted_processes.Sort((x, y) => decimal.Compare(x.ArriveTime, y.ArriveTime));
            //Show sorted processes on console.
            show_processes_on_console(sorted_processes);
        }

        public void generate_Random_Processes(int number_of_processes)
        {
            for (int i = 0; i < number_of_processes; i++)
            {
                processes_CheckBoxes[i].Checked = true;
                for (int j = 0; j < 2; j++) process_Params_UI[j, i].Value = new Random(i * 6000 + j * 1700).Next(1, 10);
            }
            

        }

        public void initializeProcesses()
        {
            //initilize process list...
            processes_List = new List<Process>();
            finished_processes_List = new List<Process>();
            //Temp_for_looping_through_ui...
            processes = new Process[10];
            //initialize processes//
            //synch ui..
            check_Enabled_Processes();
            
        }

        public void initializeApplication()
        {
            //initialize UI...
            Q1SchedulingAlgUI.SelectedIndex = 0;
            Q2SchedulingAlgUI.SelectedIndex = 2;
            Q3SchedulingAlgUI.SelectedIndex = 5;

            info_label.Text = "Please Enter Input and Click Next\n";

            processes_CheckBoxes = new CheckBox[]
            {
                checkBoxP1,checkBoxP2,checkBoxP3
                ,checkBoxP4,checkBoxP5,checkBoxP6
                ,checkBoxP7,checkBoxP8,checkBoxP9
                ,checkBoxP10
            };

            process_Params_UI = new NumericUpDown[,]{ 
                  { p1_AT_nud, p2_AT_nud, p3_AT_nud, p4_AT_nud, p5_AT_nud, p6_AT_nud, p7_AT_nud, p8_AT_nud, p9_AT_nud, p10_AT_nud }
                , { p1_BT_nud, p2_BT_nud, p3_BT_nud, p4_BT_nud, p5_BT_nud, p6_BT_nud, p7_BT_nud, p8_BT_nud, p9_BT_nud, p10_BT_nud}
                , { p1_priority_nud, p2_priority_nud, p3_priority_nud, p4_priority_nud, p5_priority_nud, p6_priority_nud, p7_priority_nud, p8_priority_nud, p9_priority_nud, p10_priority_nud}
                , { p1_RQnum_nud, p2_RQnum_nud, p3_RQnum_nud, p4_RQnum_nud, p5_RQnum_nud, p6_RQnum_nud, p7_RQnum_nud, p8_RQnum_nud, p9_RQnum_nud, p10_RQnum_nud}
                , { p1_CT_nud, p2_CT_nud, p3_CT_nud, p4_CT_nud, p5_CT_nud, p6_CT_nud, p7_CT_nud, p8_CT_nud, p9_CT_nud, p10_CT_nud}
                , { p1_TAT_nud, p2_TAT_nud, p3_TAT_nud, p4_TAT_nud, p5_TAT_nud, p6_TAT_nud, p7_TAT_nud, p8_TAT_nud, p9_TAT_nud, p10_TAT_nud}
                , { p1_WT_nud, p2_WT_nud, p3_WT_nud, p4_WT_nud, p5_WT_nud, p6_WT_nud, p7_WT_nud, p8_WT_nud, p9_WT_nud, p10_WT_nud}
                , { p1_ST_nud, p2_ST_nud, p3_ST_nud, p4_ST_nud, p5_ST_nud, p6_ST_nud, p7_ST_nud, p8_ST_nud, p9_ST_nud, p10_ST_nud}
                , { p1_RT_nud, p2_RT_nud, p3_RT_nud, p4_RT_nud, p5_RT_nud, p6_RT_nud, p7_RT_nud, p8_RT_nud, p9_RT_nud, p10_RT_nud}
            };

            readyQueueOneUI = new TextBox[]{
                rq1_Slot1,rq1_Slot2,rq1_Slot3,rq1_Slot4,rq1_Slot5,
                rq1_Slot6,rq1_Slot7,rq1_Slot8,rq1_Slot9,rq1_Slot10};

            readyQueueTwoUI = new TextBox[]{
                rq2_Slot1,rq2_Slot2,rq2_Slot3,rq2_Slot4,rq2_Slot5,
                rq2_Slot6,rq2_Slot7,rq2_Slot8,rq2_Slot9,rq2_Slot10 };

            readyQueueThreeUI = new TextBox[]{
                rq3_Slot1,rq3_Slot2,rq3_Slot3,rq3_Slot4,rq3_Slot5,
                rq3_Slot6,rq3_Slot7,rq3_Slot8,rq3_Slot9,rq3_Slot10 };

            //initialize Processes and Queues//
            initializeProcesses();
            readyQueueOne = new List<Process>();
            readyQueueTwo = new List<Process>();
            readyQueueThree = new List<Process>();
            processingUnit = new List<Process>();

            //Looping throug process_Params_UI...
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    process_Params_UI[i, j].Value = process_Params_UI[i, j].Minimum;
                }
                process_Params_UI[3, 0].Value = 1;
            }
        }

        public void assign_processes_from_ui()
        {
            processes_List = new List<Process>();
            for (int i = 0; i < 10; i++)
            {
                
                processes[i] = new Process(i+1,"P" + (i+1), process_Params_UI[0,i].Value,process_Params_UI[1,i].Value,process_Params_UI[2,i].Value,(int)process_Params_UI[3,i].Value);
                if(processes[i].ReadyQueueNumber != 0)
                {
                    processes_List.Add(processes[i]);
                }
                //Console.WriteLine(processes[i].Id+" process [" + (i + 1) + "]: " + processes[i].ArriveTime + "," + processes[i].BurstTime + "," +
                //    processes[i].Priority + "," + processes[i].ReadyQueueNumber);
            }
            //Show the result on Console.
            show_processes_on_console(processes_List);
            Console.WriteLine("process_Params_UI.GetLength(0) is : " + process_Params_UI.GetLength(0));
            Console.WriteLine("process_Params_UI.GetLength(1) is : " + process_Params_UI.GetLength(1));
        }

        //Puts all disabled process out of ready queues (assign RQn = 0)
        public void check_Enabled_Processes()
        {
            //RQn for process i is process_Params_UI[3,i]...
            //make all values zero for the newly added process
            for (int i = 0; i < processes_CheckBoxes.Length; i++)
                if (!processes_CheckBoxes[i].Checked)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        process_Params_UI[j, i].Value = process_Params_UI[j, i].Minimum;
                        process_Params_UI[j, i].Enabled = false;
                        if (j > 3) process_Params_UI[j, i].BackColor = SystemColors.Control;
                    }

                }
                else if (!process_Params_UI[3, i].Enabled) {
                    process_Params_UI[3, i].Value = 1;
                    for (int j = 0; j < 9; j++)
                    {
                        process_Params_UI[j, i].Enabled = true;
                        if (j > 3) process_Params_UI[j, i].BackColor = SystemColors.ControlLightLight;
                    }
                }

            assign_processes_from_ui();
        }

        public void checkBox_Handler(int checkBoxIndex) {
            if (processes_CheckBoxes[checkBoxIndex].Checked) processes_CheckBoxes[checkBoxIndex+1].Enabled = true;
            else
            {
                if (!processes_CheckBoxes[checkBoxIndex+1].Checked) processes_CheckBoxes[checkBoxIndex+1].Enabled = false;
                else processes_CheckBoxes[checkBoxIndex].Checked = true;
            }
            check_Enabled_Processes();
            update_UI();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(1);
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(2);
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(3);
        }

        private void CheckBoxP5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(4);
        }

        private void CheckBoxP6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(5);
        }

        private void CheckBoxP7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(6);
        }

        private void CheckBoxP8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(7);
        }

        private void CheckBoxP9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Handler(8);
        }

        private void CheckBoxP10_CheckedChanged(object sender, EventArgs e)
        {
            check_Enabled_Processes();

        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void nuds_ValueChanged(object sender, EventArgs e)
        {
            assign_processes_from_ui();
            checkBoxQ1.Checked = false;
            checkBoxQ2.Checked = false;
            checkBoxQ3.Checked = false;
            button1.Enabled = false;
            foreach (Process process in processes_List)
            {
                //update if rq1 2 3  ....
                if (process.ReadyQueueNumber == 1) { checkBoxQ1.Checked = true; button1.Enabled = true; }
                else if (process.ReadyQueueNumber == 2) { checkBoxQ2.Checked = true; button1.Enabled = true; }
                else if (process.ReadyQueueNumber == 3) { checkBoxQ3.Checked = true; button1.Enabled = true; }
            }
            update_UI();
        }

        private void show_processes_on_console(List<Process> _processes_List)
        {
            foreach (Process process in _processes_List)
            {
                Console.WriteLine(process.Name + " : " + process.ArriveTime + "," + process.BurstTime + "," +
                    process.Priority + "," + process.ReadyQueueNumber);

            }
        }
        private int scheduling_AlgorithmQ(int queue_Number)
        {
            if (queue_Number == 1)
            {
                return scheduling_AlgorithmQ1;
            }
            else if (queue_Number == 2)
            {
                return scheduling_AlgorithmQ2;
            }
            else if (queue_Number == 3)
            {
                return scheduling_AlgorithmQ3;
            }
            else return -1; 

        }

        private List<Process> readyQueue(int queue_Number)
        {
            if (queue_Number == 1)
            {
                return readyQueueOne;
            }
            else if (queue_Number == 2)
            {
                return readyQueueTwo;
            }
            else if (queue_Number == 3)
            {
                return readyQueueThree;
            }
            else return null;
        }
        private decimal QuantomTime(int queue_Number)
        {
            if (queue_Number == 1)
            {
                return QuantomTime1;
            }
            else if (queue_Number == 2)
            {
                return QuantomTime2;
            }
            else if (queue_Number == 3)
            {
                return QuantomTime3;
            }
            else return -1;

        }
        private TextBox[] readyQueueUI(int queue_Number)
        {
            if (queue_Number == 1)
            {
                return readyQueueOneUI;
            }
            else if (queue_Number == 2)
            {
                return readyQueueTwoUI;
            }
            else if (queue_Number == 3)
            {
                return readyQueueThreeUI;
            }
            else return null;

        }
        private void Button1_Click(object sender, EventArgs e)
        {

            //loop remaining process table to check for arrivals
            foreach (Process process in processes_List)
            {
                if (process.ReadyQueueNumber != 0)
                {
                    //if round Robin reset timer for new process.
                    if (scheduling_AlgorithmQ(process.ReadyQueueNumber) == RoundRobinS)
                        process.RoundRobinTimer = QuantomTime(process.ReadyQueueNumber);
                    //add arrived process to ready queue
                    if (process.ArriveTime == currentTime) readyQueue(process.ReadyQueueNumber).Add(process);
                }
            }
            //remove added process from the list of coming process
            foreach (Process process in readyQueueOne)
            {
                process.WaitTime = currentTime - process.ArriveTime - process.Duration;
                
                if (processes_List.Contains(process))
                {
                    processes_List.Remove(process);
                }

                if(scheduling_AlgorithmQ1 == SJF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.BurstTime, y.BurstTime));
                    readyQueueOne = readyQueueOne.OrderBy(x => x.BurstTime).ToList();
                }
                else if(scheduling_AlgorithmQ1 == SRTF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.RemainingTime, y.RemainingTime));
                    readyQueueOne = readyQueueOne.OrderBy(x => x.RemainingTime).ToList();
                }
                else if ((scheduling_AlgorithmQ1 == PriorityNonpreamtiveS)|| (scheduling_AlgorithmQ1 == PriorityPreamtiveS))
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.Priority, y.Priority));
                    readyQueueOne = readyQueueOne.OrderBy(x => x.Priority).ToList();
                }
            }
            foreach (Process process in readyQueueTwo)
            {
                process.WaitTime = currentTime - process.ArriveTime - process.Duration;

                if (processes_List.Contains(process))
                {
                    processes_List.Remove(process);
                }

                if (scheduling_AlgorithmQ2 == SJF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.BurstTime, y.BurstTime));
                    readyQueueTwo = readyQueueTwo.OrderBy(x => x.BurstTime).ToList();
                }
                else if (scheduling_AlgorithmQ2 == SRTF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.RemainingTime, y.RemainingTime));
                    readyQueueTwo = readyQueueTwo.OrderBy(x => x.RemainingTime).ToList();
                }
                else if ((scheduling_AlgorithmQ2 == PriorityNonpreamtiveS) || (scheduling_AlgorithmQ2 == PriorityPreamtiveS))
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.Priority, y.Priority));
                    readyQueueTwo = readyQueueTwo.OrderBy(x => x.Priority).ToList();
                }
            }
            foreach (Process process in readyQueueThree)
            {
                process.WaitTime = currentTime - process.ArriveTime - process.Duration;

                if (processes_List.Contains(process))
                {
                    processes_List.Remove(process);
                }

                if (scheduling_AlgorithmQ3 == SJF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.BurstTime, y.BurstTime));
                    readyQueueThree = readyQueueThree.OrderBy(x => x.BurstTime).ToList();
                }
                else if (scheduling_AlgorithmQ3 == SRTF)
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.RemainingTime, y.RemainingTime));
                    readyQueueThree = readyQueueThree.OrderBy(x => x.RemainingTime).ToList();
                }
                else if ((scheduling_AlgorithmQ3 == PriorityNonpreamtiveS) || (scheduling_AlgorithmQ3 == PriorityPreamtiveS))
                {
                    //readyQueueOne.Sort((x, y) => decimal.Compare(x.Priority, y.Priority));
                    readyQueueThree = readyQueueThree.OrderBy(x => x.Priority).ToList();
                }
            }
            //if process is being executed
            if (processingUnit.Count == 1)
            {
                Update_Process_UI(processingUnit[0]);
                processingUnit[0].update_duration();
                //TODO method gives the queue algrtm of a given process
                if (scheduling_AlgorithmQ(processingUnit[0].ReadyQueueNumber) == RoundRobinS)
                    processingUnit[0].RoundRobinTimer--;
                processingUnitUI.Text += processingUnit[0].Name + ",";
                processingUnit[0].Update_RemainingTime();
                if (processingUnit[0].getFinished())
                {
                    processingUnit[0].Finish(currentTime);
                    //add the finished process to finished list...
                    finished_processes_List.Add(processingUnit[0]);
                    //remove from the processising unit...
                    processingUnit.Clear();
                }
                //If cpu is not cleared check for other process in queues
                //Check queue 1 first
                //Preamptivity (Swichy Swichy)
                if (processingUnit.Count != 0)
                {
                    if (readyQueueOne.Count != 0)
                    {
                        //if process level 1 compare according to algrtm
                        if (processingUnit[0].ReadyQueueNumber == 1)
                        {
                            if (scheduling_AlgorithmQ1 == SRTF)
                            {
                                //swich ready queue with process in CPU
                                if (readyQueueOne[0].RemainingTime < processingUnit[0].RemainingTime)
                                {
                                    processingUnit.Add(readyQueueOne[0]);
                                    Console.WriteLine(processingUnit.ToString());
                                    readyQueueOne.RemoveAt(0);
                                    Console.WriteLine(readyQueueOne.ToString());
                                    readyQueueOne.Add(processingUnit[0]);
                                    Console.WriteLine(readyQueueOne.ToString());
                                    processingUnit.RemoveAt(0);
                                    Console.WriteLine(processingUnit.ToString());
                                }
                            }
                            if (scheduling_AlgorithmQ1 == PriorityPreamtiveS)
                            {
                                Console.WriteLine("processising count = " + processingUnit.Count);
                                Console.WriteLine("ready queue count = " + readyQueueOne.Count);
                                if ((readyQueueOne.Count != 0) && (processingUnit.Count != 0))
                                    //swich ready queue with process in CPU
                                    if (readyQueueOne[0].Priority < processingUnit[0].Priority)
                                    {
                                        processingUnit.Add(readyQueueOne[0]);
                                        Console.WriteLine(processingUnit.ToString());
                                        readyQueueOne.RemoveAt(0);
                                        Console.WriteLine(readyQueueOne.ToString());
                                        readyQueueOne.Add(processingUnit[0]);
                                        Console.WriteLine(readyQueueOne.ToString());
                                        processingUnit.RemoveAt(0);
                                        Console.WriteLine(processingUnit.ToString());
                                    }
                            }
                            if (scheduling_AlgorithmQ1 == RoundRobinS)
                            {
                                if (processingUnit.Count != 0)
                                    if (processingUnit[0].RoundRobinTimer == 0)
                                    {
                                        //reset RoundRobin Timer for the process
                                        processingUnit[0].RoundRobinTimer = QuantomTime1;
                                        //if queue is not empty swich with process in CPU.
                                        if (readyQueueOne.Count != 0)
                                        {
                                            processingUnit.Add(readyQueueOne[0]);
                                            Console.WriteLine(processingUnit.ToString());
                                            readyQueueOne.RemoveAt(0);
                                            Console.WriteLine(readyQueueOne.ToString());
                                            readyQueueOne.Add(processingUnit[0]);
                                            Console.WriteLine(readyQueueOne.ToString());
                                            processingUnit.RemoveAt(0);
                                            Console.WriteLine(processingUnit.ToString());
                                        }

                                    }
                            }
                        }
                        //Else Swich Directly
                        else
                        {
                            //Swich Directly
                            processingUnit.Add(readyQueueOne[0]);
                            Console.WriteLine(processingUnit.ToString());
                            readyQueueOne.RemoveAt(0);
                            Console.WriteLine(readyQueueOne.ToString());
                            //add to the respective queue.
                            readyQueue(processingUnit[0].ReadyQueueNumber).Add(processingUnit[0]);
                            Console.WriteLine(readyQueueOne.ToString());
                            processingUnit.RemoveAt(0);
                            Console.WriteLine(processingUnit.ToString());
                        }
                    }
                    //ready queue 1 is empty check 2
                    else if (readyQueueTwo.Count != 0)
                    {
                        if (processingUnit[0].ReadyQueueNumber == 1)
                        {
                            //Do Nothing...
                        }
                        else if (processingUnit[0].ReadyQueueNumber == 2)
                        {
                            //if process level 1 compare according to algrtm\
                            if (scheduling_AlgorithmQ2 == SRTF)
                            {
                                //swich ready queue with process in CPU
                                if (readyQueueTwo[0].RemainingTime < processingUnit[0].RemainingTime)
                                {
                                    processingUnit.Add(readyQueueTwo[0]);
                                    Console.WriteLine(processingUnit.ToString());
                                    readyQueueTwo.RemoveAt(0);
                                    Console.WriteLine(readyQueueTwo.ToString());
                                    readyQueueTwo.Add(processingUnit[0]);
                                    Console.WriteLine(readyQueueTwo.ToString());
                                    processingUnit.RemoveAt(0);
                                    Console.WriteLine(processingUnit.ToString());
                                }
                            }
                            if (scheduling_AlgorithmQ2 == PriorityPreamtiveS)
                            {
                                Console.WriteLine("processising count = " + processingUnit.Count);
                                Console.WriteLine("ready queue count = " + readyQueueTwo.Count);
                                if ((readyQueueTwo.Count != 0) && (processingUnit.Count != 0))
                                    //swich ready queue with process in CPU
                                    if (readyQueueTwo[0].Priority < processingUnit[0].Priority)
                                    {
                                        processingUnit.Add(readyQueueTwo[0]);
                                        Console.WriteLine(processingUnit.ToString());
                                        readyQueueTwo.RemoveAt(0);
                                        Console.WriteLine(readyQueueTwo.ToString());
                                        readyQueueTwo.Add(processingUnit[0]);
                                        Console.WriteLine(readyQueueTwo.ToString());
                                        processingUnit.RemoveAt(0);
                                        Console.WriteLine(processingUnit.ToString());
                                    }
                            }
                            if (scheduling_AlgorithmQ2 == RoundRobinS)
                            {
                                if (processingUnit.Count != 0)
                                    if (processingUnit[0].RoundRobinTimer == 0)
                                    {
                                        //reset RoundRobin Timer for the process
                                        processingUnit[0].RoundRobinTimer = QuantomTime2;
                                        //if queue is not empty swich with process in CPU.
                                        if (readyQueueTwo.Count != 0)
                                        {
                                            processingUnit.Add(readyQueueTwo[0]);
                                            Console.WriteLine(processingUnit.ToString());
                                            readyQueueTwo.RemoveAt(0);
                                            Console.WriteLine(readyQueueTwo.ToString());
                                            readyQueueTwo.Add(processingUnit[0]);
                                            Console.WriteLine(readyQueueTwo.ToString());
                                            processingUnit.RemoveAt(0);
                                            Console.WriteLine(processingUnit.ToString());
                                        }

                                    }
                            }
                        }
                        else
                        {
                            //Swich Directly
                            processingUnit.Add(readyQueueTwo[0]);
                            Console.WriteLine(processingUnit.ToString());
                            readyQueueTwo.RemoveAt(0);
                            Console.WriteLine(readyQueueTwo.ToString());
                            readyQueue(processingUnit[0].ReadyQueueNumber).Add(processingUnit[0]);
                            Console.WriteLine(readyQueueTwo.ToString());
                            processingUnit.RemoveAt(0);
                            Console.WriteLine(processingUnit.ToString());
                        }
                        
                    }
                    //ready queue 2 is empty check 3
                    else if (readyQueueThree.Count != 0)
                    {
                        if (processingUnit[0].ReadyQueueNumber == 1)
                        {
                            //Do Nothing...
                        }
                        else if (processingUnit[0].ReadyQueueNumber == 2)
                        {
                            //Do Nothing...
                        }
                        else if (processingUnit[0].ReadyQueueNumber == 3)
                        {
                            //if process level 1 compare according to algrtm\
                            if (scheduling_AlgorithmQ3 == SRTF)
                            {
                                //swich ready queue with process in CPU
                                if (readyQueueThree[0].RemainingTime < processingUnit[0].RemainingTime)
                                {
                                    processingUnit.Add(readyQueueThree[0]);
                                    Console.WriteLine(processingUnit.ToString());
                                    readyQueueThree.RemoveAt(0);
                                    Console.WriteLine(readyQueueThree.ToString());
                                    readyQueueThree.Add(processingUnit[0]);
                                    Console.WriteLine(readyQueueThree.ToString());
                                    processingUnit.RemoveAt(0);
                                    Console.WriteLine(processingUnit.ToString());
                                }
                            }
                            if (scheduling_AlgorithmQ3 == PriorityPreamtiveS)
                            {
                                Console.WriteLine("processising count = " + processingUnit.Count);
                                Console.WriteLine("ready queue count = " + readyQueueThree.Count);
                                if ((readyQueueThree.Count != 0) && (processingUnit.Count != 0))
                                    //swich ready queue with process in CPU
                                    if (readyQueueThree[0].Priority < processingUnit[0].Priority)
                                    {
                                        processingUnit.Add(readyQueueThree[0]);
                                        Console.WriteLine(processingUnit.ToString());
                                        readyQueueThree.RemoveAt(0);
                                        Console.WriteLine(readyQueueThree.ToString());
                                        readyQueueThree.Add(processingUnit[0]);
                                        Console.WriteLine(readyQueueThree.ToString());
                                        processingUnit.RemoveAt(0);
                                        Console.WriteLine(processingUnit.ToString());
                                    }
                            }
                            if (scheduling_AlgorithmQ3 == RoundRobinS)
                            {
                                if (processingUnit.Count != 0)
                                    if (processingUnit[0].RoundRobinTimer == 0)
                                    {
                                        //reset RoundRobin Timer for the process
                                        processingUnit[0].RoundRobinTimer = QuantomTime3;
                                        //if queue is not empty swich with process in CPU.
                                        if (readyQueueThree.Count != 0)
                                        {
                                            processingUnit.Add(readyQueueThree[0]);
                                            Console.WriteLine(processingUnit.ToString());
                                            readyQueueThree.RemoveAt(0);
                                            Console.WriteLine(readyQueueThree.ToString());
                                            readyQueueThree.Add(processingUnit[0]);
                                            Console.WriteLine(readyQueueThree.ToString());
                                            processingUnit.RemoveAt(0);
                                            Console.WriteLine(processingUnit.ToString());
                                        }

                                    }
                            }
                        }

                    }


                }
            }
            //add a process to the processsising unit if its empty and rq1 is not empty
            if (processingUnit.Count == 0)
            {
                if (readyQueueOne.Count != 0)
                {
                    processingUnit.Add(readyQueueOne[0]);
                    readyQueueOne.RemoveAt(0);
                    //TODO update method gives the queue algrtm of a given process
                    //TODO update method gives the quantom time of a given process
                    if (scheduling_AlgorithmQ1 == RoundRobinS)
                        processingUnit[0].RoundRobinTimer = QuantomTime1;
                    //start the process
                    if (!processingUnit[0].Started) { processingUnit[0].Update_StartTime(currentTime); }
                }else if (readyQueueTwo.Count != 0)
                {
                    processingUnit.Add(readyQueueTwo[0]);
                    readyQueueTwo.RemoveAt(0);
                    //TODO update method gives the queue algrtm of a given process
                    //TODO update method gives the quantom time of a given process
                    if (scheduling_AlgorithmQ2 == RoundRobinS)
                        processingUnit[0].RoundRobinTimer = QuantomTime2;
                    //start the process
                    if (!processingUnit[0].Started) { processingUnit[0].Update_StartTime(currentTime); }
                }
                else if (readyQueueThree.Count != 0)
                {
                    processingUnit.Add(readyQueueThree[0]);
                    readyQueueThree.RemoveAt(0);
                    //TODO update method gives the queue algrtm of a given process
                    //TODO update method gives the quantom time of a given process
                    if (scheduling_AlgorithmQ3 == RoundRobinS)
                        processingUnit[0].RoundRobinTimer = QuantomTime3;
                    //start the process
                    if (!processingUnit[0].Started) { processingUnit[0].Update_StartTime(currentTime); }
                }
            }
            update_UI();
            currentTime++;
        }

        private void update_UI()
        {
            for (int queue_number = 1; queue_number <= 3; queue_number++)
            {
                //update rq UI
                if (readyQueue(queue_number).Count == 0)
                    foreach (TextBox textBox in readyQueueUI(queue_number))
                        textBox.Text = "";
                int i = 0;
                foreach (Process process in readyQueue(queue_number))
                { readyQueueUI(queue_number)[i].Text = process.Name; i++; }
                while (i < readyQueueUI(queue_number).Length)
                {
                    readyQueueUI(queue_number)[i].Text = "";
                    i++;
                }
            }
            //update processising unit UI...
            if (processingUnit.Count == 0) processising_Unit_Box.Text = "";
            foreach (Process process in processingUnit)
            { processising_Unit_Box.Text = process.Name;
                if (scheduling_AlgorithmQ1 == RoundRobinS)
                    RRTimerLabel.Text = "Timer : "+processingUnit[0].RoundRobinTimer;
            }

            //Update info Label...
            info_label.Text = "";
            //add Scheduling Algorithm...
            if(Q1SchedulingAlgUI.Enabled)
            info_label.Text += "=>Scheduling Algrthm for Q1: " + scheduling_Algorithms[scheduling_AlgorithmQ1] + "\n\n";
            if(Q2SchedulingAlgUI.Enabled)
            info_label.Text += "=>Scheduling Algrthm for Q2: " + scheduling_Algorithms[scheduling_AlgorithmQ2] + "\n\n";
            if(Q3SchedulingAlgUI.Enabled)
            info_label.Text += "=>Scheduling Algrthm for Q3: " + scheduling_Algorithms[scheduling_AlgorithmQ3] + "\n\n";

            //add Current Time...
            info_label.Text += "=>CurrentTime : " + currentTime+ "\n";
            //print remaining processes list...
            info_label.Text += "\nremaining processes:\n";
            if (processes_List.Count != 0)
                foreach (Process process in processes_List)
                {
                    info_label.Text += Generate_Process_Details(process) + "\n";
                    Update_Process_UI(process);
                }
            for (int queue_number = 1; queue_number <= 3; queue_number++)
            {
                //print ready queues processes list...
                info_label.Text += "\nRQ"+queue_number+" processes:\n";
                if (readyQueue(queue_number).Count != 0)
                    foreach (Process process in readyQueue(queue_number))
                    {
                        info_label.Text += Generate_Process_Details(process) + "\n";
                        Update_Process_UI(process);
                    }
                else info_label.Text += "none\n";
            }
            //print Executing process...
            info_label.Text += "\nProcess being Executed: \n";
            if (processingUnit.Count != 0)
                foreach (Process process in processingUnit)
                {
                    info_label.Text += Generate_Process_Details(process) + "\n";
                    Update_Process_UI(process);
                }
            else info_label.Text += "none\n";
            //print Finished process...
            info_label.Text += "\nFinished Process: \n";
            if (finished_processes_List.Count != 0)
                foreach (Process process in finished_processes_List)
                {
                    info_label.Text += Generate_Process_Details(process) + "\n";
                    Update_Process_UI(process);
                }
            else info_label.Text += "none\n";
            //Check if all lists are empty then calculate average turnaround time and avg waiting time...
            if ( (processes_List.Count + readyQueueOne.Count + readyQueueTwo.Count 
                + readyQueueThree.Count + processingUnit.Count) == 0 )
            {
                if (finished_processes_List.Count != 0)
                {
                    decimal average_TAT, average_WT, sumofTAT = 0, sumofWT = 0;
                    foreach (Process process in finished_processes_List)
                    {
                        sumofTAT += process.TurnArroundTime;
                        sumofWT += process.WaitTime;
                    }
                    average_TAT = sumofTAT / finished_processes_List.Count;
                    average_WT = sumofWT / finished_processes_List.Count;
                    RRTimerLabel.Text = "Done!\n";
                    info_label.Text += "\n\n\nAverage Turarround Time is " + (double)average_TAT
                        + "\n\nAverage Waiting Time is " + (double)average_WT;
                }
            }

            
        }
        public String Generate_Process_Details(Process process)
        {
            /*
            String s = process.Name +": Strtd=" + process.Started.ToString() 
                + " ,DT=" + process.Duration + " ,RT=" + process.RemainingTime 
                + " ,CT=" + process.CompeletionTime + " ,TAT="+ process.TurnArroundTime 
                + " ,WT=" + process.WaitTime + " ,finished=" + process.Finished.ToString();
            */
            String s = process.Name;
            return s;
        }
        private void Update_Process_UI(Process process)
        {
            process_Params_UI[4, process.Id-1].Value = process.CompeletionTime;
            process_Params_UI[5, process.Id-1].Value = process.TurnArroundTime;
            process_Params_UI[6, process.Id-1].Value = process.WaitTime;
            process_Params_UI[7, process.Id-1].Value = process.StartTime;
            process_Params_UI[8, process.Id - 1].Value = process.RemainingTime;
        }

        private void QtUIValueChanged(object sender, EventArgs e)
        {
            QuantomTime1 = QT1UI.Value;
            QuantomTime2 = QT2UI.Value;
            QuantomTime3 = QT3UI.Value;
        }

        private void CheckBoxQ1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxQ1.Checked)
            {
                checkBoxQ1.Enabled = true;
                foreach (TextBox textBox in readyQueueOneUI)
                    textBox.Enabled = true;
                Q1SchedulingAlgUI.Enabled = true;
            }
            else {
                checkBoxQ1.Enabled = false;
                foreach (TextBox textBox in readyQueueOneUI)
                    textBox.Enabled = false;
                Q1SchedulingAlgUI.Enabled = false;
            }
        }

        private void CheckBoxQ2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxQ2.Checked)
            {
                checkBoxQ2.Enabled = true;
                foreach (TextBox textBox in readyQueueTwoUI)
                    textBox.Enabled = true;
                Q2SchedulingAlgUI.Enabled = true;
                if (scheduling_AlgorithmQ2 == RoundRobinS)
                {
                    labelQt2.Enabled = true;
                    QT2UI.Enabled = true;
                }
                else { labelQt2.Enabled = false; QT2UI.Enabled = false; }
            }
            else
            {
                checkBoxQ2.Enabled = false;
                foreach (TextBox textBox in readyQueueTwoUI)
                    textBox.Enabled = false;
                Q2SchedulingAlgUI.Enabled = false;
            }
        }

        private void CheckBoxQ3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxQ3.Checked)
            {
                checkBoxQ3.Enabled = true;
                foreach (TextBox textBox in readyQueueThreeUI)
                    textBox.Enabled = true;
                Q3SchedulingAlgUI.Enabled = true;
                if (scheduling_AlgorithmQ3 == RoundRobinS)
                {
                    labelQt3.Enabled = true;
                    QT3UI.Enabled = true;
                }
                else { labelQt3.Enabled = false; QT3UI.Enabled = false; }
                
            }
            else
            {
                checkBoxQ3.Enabled = false;
                foreach (TextBox textBox in readyQueueThreeUI)
                    textBox.Enabled = false;
                Q3SchedulingAlgUI.Enabled = false;
            }
        }

        private void SchedulingAlgForQ1(object sender, EventArgs e)
        {
            scheduling_AlgorithmQ1 = Q1SchedulingAlgUI.SelectedIndex;
            info_label.Text += "\n\nScheduling Alg for Q1: " + Q1SchedulingAlgUI.SelectedIndex;
            if (scheduling_AlgorithmQ1 == RoundRobinS)
            {
                labelQt1.Enabled = true;
                QT1UI.Enabled = true;
            }
            else { labelQt1.Enabled = false; QT1UI.Enabled = false; }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            initializeApplication();
            generate_Random_Processes(5);
            processingUnitUI.Text = "";
            //FCFS_Scheduling(processes_List);
        }

        private void Q2SchedulingAlgUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            scheduling_AlgorithmQ2 = Q2SchedulingAlgUI.SelectedIndex;
            info_label.Text += "\n\nScheduling Alg for Q2: " + Q2SchedulingAlgUI.SelectedIndex;
            if (scheduling_AlgorithmQ2 == RoundRobinS)
            {
                labelQt2.Enabled = true;
                QT2UI.Enabled = true;
            }
            else { labelQt2.Enabled = false; QT2UI.Enabled = false; }
        }
        private void Q3SchedulingAlgUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            scheduling_AlgorithmQ3 = Q3SchedulingAlgUI.SelectedIndex;
            info_label.Text += "\n\nScheduling Alg for Q3: " + Q3SchedulingAlgUI.SelectedIndex;
            if (scheduling_AlgorithmQ3 == RoundRobinS)
            {
                labelQt3.Enabled = true;
                QT3UI.Enabled = true;
            }
            else { labelQt3.Enabled = false; QT3UI.Enabled = false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for DailyPlanner.xaml
    /// </summary>
    public partial class DailyPlanner : UserControl
    {
        public event Action GameExit;
        public event Action Death;
        public event Action NewDayCompleted;

        public List<Task> Appointments { get; private set; }
        private double _moneyAtStartOfDay;

        public DailyPlanner()
        {
            InitializeComponent();
            Translate();
            Appointments = new List<Task>();
            NewDay();
        }

        private void Translate()
        {
            txtAddTask.Text = EnglishStrings.AddTask.Translate();
        }

        private void AppointmentBlock_Click(object sender, EventArgs e)
        {
            var appointmentBlock = sender as AppointmentBlock;
            if (appointmentBlock != null && appointmentBlock.Task != null)
            {
                //Special: quit job
                if (appointmentBlock.Task.TaskType == TaskType.Job)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.QuitJobHeader.Translate(), EnglishStrings.QuitJobText.Translate(), MessagePicture.Work, (result) =>
                    {
                        if (result == true)
                        {
                            Player.Current.QuitJob = true;
                            Appointments.RemoveAll(a => a.TaskType == TaskType.Job);
                            Update();
                        }
                    });
                }
                else if (appointmentBlock.Task.TaskType == TaskType.BowToRobotRulers)
                {
                    double cost = 10000;
                    if (Player.Current.Money < cost)
                    {
                        CustomMessageBox.ShowDialog(EnglishStrings.RiseUp.Translate(), String.Format(EnglishStrings.RebellionCashRequired.Translate(), cost.ToCurrencyString()), MessagePicture.Robot);
                    }
                    else
                    {
                        int chance =  Player.Current.ChallengeMode ? 50 : 75;
                        CustomMessageBox.ShowDialog(EnglishStrings.RiseUp.Translate(), String.Format(EnglishStrings.RebellionStart.Translate(), chance), MessagePicture.Robot, (result) =>
                        {
                            if (result == true)
                            {
                                if (RandomHelpers.Chance(chance))
                                {
                                    if (!TrophyManager.HasTrophy(Trophy.RebelLeader))
                                        TrophyManager.UnlockTrophy(Trophy.RebelLeader);

                                    CustomMessageBox.ShowDialog(EnglishStrings.Freedom.Translate(), EnglishStrings.RebellionSuccess.Translate(), MessagePicture.Robot);
                                    Player.Current.Money -= 10000;
                                    Player.Current.RobotRulers = false;
                                    Appointments.RemoveAll(a => a.TaskType == TaskType.BowToRobotRulers);
                                    Update();
                                }
                                else
                                {
                                    if (Death != null)
                                        Death();
                                }
                            }
                        });
                    }
                }
                else if (appointmentBlock.Task != null)
                {
                    RemoveAppointment(appointmentBlock.Task, false);
                }
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskDialog dialog = new AddTaskDialog();
            dialog.TaskClick += (t) =>
            {
                AddTask(t);
            };
            dialog.ShowDialog();
        }

        private void AddTask(Task task)
        {
            foreach (var checkTask in Player.Current.TasksInProgress)
            {
                if (checkTask == task)
                    return;
            }

            Player.Current.TasksInProgress.Add(task);
            Update();
        }

        private void AddAppointment(Task task)
        {
            if (Appointments.Count < 15)
            {
                task.HoursPutIn++;
                Appointments.Add(task);
                Update();
            }
        }

        private void RemoveAppointment(Task task, bool removeTodoTask)
        {
            //Special case
            var shootVideoTask = task as ShootVideo;
            if (shootVideoTask != null)
            {
                foreach (var currentTask in Player.Current.TasksInProgress)
                {
                    var editVideoTask = currentTask as EditVideo;
                    if (editVideoTask != null && editVideoTask.Video == shootVideoTask.Video)
                    {
                        RemoveAppointment(currentTask, true);
                        break;
                    }
                }
            }

            if (removeTodoTask)
            {
                Appointments.RemoveAll(a => a == task);
                Player.Current.TasksInProgress.Remove(task);
            }
            else
            {
                Appointments.Remove(task);
                task.HoursPutIn--;
            }

            Update();
        }

        public void Update()
        {
            //Todo Tasks
            tasksPanel.Children.Clear();
            foreach (var task in Player.Current.TasksInProgress)
            {
                TodoTask appointmentTask = new TodoTask(task);
                appointmentTask.TaskClick += (s, ev) =>
                {
                    if (!task.IsCompleted)
                    {
                        AddAppointment(task);
                    }
                };
                appointmentTask.CancelTaskClick += (s, ev) =>
                {
                    bool isStudy = task is Study;
                    CustomMessageBox.ShowDialog(EnglishStrings.RemoveTask.Translate(), String.Format("{0} {1}", EnglishStrings.AreYouSure.Translate(), isStudy ? String.Format("\n{0}", EnglishStrings.NoRefunds.Translate()) : ""), MessagePicture.Question, (result) =>
                    {
                        if (result == true)
                        {
                            RemoveAppointment(task, true);
                        }
                    });
                };
                tasksPanel.Children.Add(appointmentTask);
                appointmentTask.UpdateText();
            }

            //Appointments
            foreach (AppointmentBlock appointment in appointmentPanel.Children)
            {
                appointment.Task = null;
            }

            for (int i = 0; i < Appointments.Count; i++)
            {
                (appointmentPanel.Children[i] as AppointmentBlock).Task = Appointments[i];
            }
        }

        private void CleanTodoList()
        {
            foreach (Task task in Player.Current.TasksInProgress)
            {
                if (task.IsCompleted)
                {
                    var studyTask = task as Study;
                    if (studyTask != null)
                    {
                        switch (studyTask.SkillModifierType)
                        {
                            case (SkillModifierType.Shooting):
                                Player.Current.ShootingSkill += studyTask.SkillModifier;
                                break;

                            case (SkillModifierType.PostProduction):
                                Player.Current.PostProductionSkill += studyTask.SkillModifier;
                                break;

                            case (SkillModifierType.VideoAttribute):
                                Player.Current.VideoAttributePoints += studyTask.SkillModifier;
                                break;

                            case (SkillModifierType.ViewQuality):
                                Player.Current.CanViewQualityBeforeUpload = true;
                                break;
                        }
                    }

                    var shootTask = task as ShootVideo;
                    if (shootTask != null)
                    {
                        Player.Current.Videos.Add(shootTask.Video);
                    }

                    var editTask = task as EditVideo;
                    if (editTask != null)
                    {
                        editTask.Video.HasBeenEdited = true;
                    }
                }
            }

            Player.Current.TasksInProgress.RemoveAll(t => t.IsCompleted);
        }

        public void NewDay()
        {
            _moneyAtStartOfDay = Player.Current.Money;

            Player.Current.Money -= 50; //Living Expenses
            Player.Current.Money -= Math.Max(0, Player.Current.CostOfLivingExtra);

            if(StoreItems.Current.Loan.Purchased)
            {
                Player.Current.LoanPayOff -= StoreItems.Current.Loan.AdditionalCost;
                Player.Current.Money -= StoreItems.Current.Loan.AdditionalCost;

                if (Player.Current.LoanPayOff <= 0)
                {
                    StoreItems.Current.Loan.Purchased = false;
                }
            }

            Appointments = new List<Task>();
            if (Player.Current.RobotRulers)
            {
                var robots = new BowToRobotRulers();
                AddAppointment(robots);
                AddAppointment(robots);
                AddAppointment(robots);
            }
            if (!Player.Current.QuitJob)
            {
                Player.Current.Money += 100;
                var job = new Job();
                for (int i = 0; i <= job.HoursToComplete; i++)
                {
                    AddAppointment(job);
                }
                if (Player.Current.HasPromotion)
                {
                    Player.Current.Money += 50;
                    AddAppointment(job);
                    AddAppointment(job);
                }
                if (!Player.Current.QuitJob && Player.Current.Overtime)
                {
                    AddAppointment(job);
                    AddAppointment(job);
                    AddAppointment(job);
                    AddAppointment(job);
                    Player.Current.Overtime = false;
                }
            }
            CleanTodoList();
            Update();

            RunIterations();
        }

        private void RunIterations()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, ea) =>
            {
                foreach (var channel in Player.Current.Channels)
                {
                    if (channel != Channel.UnreleasedVideos)
                    {
                        double dailyIncome = 0;
                        double dailyExpenses = 0;
                        foreach (var video in channel.Videos)
                        {
                            VideoViewer.Iteration(channel, video, ref dailyIncome, ref dailyExpenses);
                            if (video.Iterations == 1)
                            {
                                dailyExpenses += video.Cost;
                                dailyExpenses += video.OnceOffCost;
                            }
                        }

                        //Channel stats
                        channel.Income += dailyIncome;
                        channel.SubscribersOverTime.Add(channel.Subscribers);
                        channel.IncomeOverTime.Add(dailyIncome);
                        channel.ExpensesOverTime.Add(dailyExpenses);

                        //Player costs
                        Player.Current.Money += (dailyIncome - dailyExpenses);
                    }
                }

                double rivalDailyIncome = 0;
                double refDailyExpenses = 0;
                foreach (var rival in Rivals.Current.All)
                {
                    if (rival.Channel != null && rival.Channel.Videos != null)
                    {
                        foreach (var video in rival.Channel.Videos.ToList())
                        {
                            VideoViewer.Iteration(rival.Channel, video, ref rivalDailyIncome, ref refDailyExpenses);
                        }
                    }
                }
            };
            worker.RunWorkerCompleted += (s, ea) =>
            {
                if (Player.Current.Money < 0)
                {
                    if (GameExit != null)
                        GameExit();
                }

                if (Player.Current.Money - _moneyAtStartOfDay >= 1000)
                {
                    TrophyManager.UnlockTrophy(Trophy.Bunsen);
                }

                Player.Current.Iterations++;

                if (NewDayCompleted != null)
                    NewDayCompleted();
            };
            worker.RunWorkerAsync();
        }
    }
}
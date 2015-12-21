using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for AddTaskDialog.xaml
    /// </summary>
    public partial class AddTaskDialog : ChildWindow
    {
        public event Action<Task> TaskClick;

        public AddTaskDialog()
        {
            InitializeComponent();
            Translate();
        }

        private void Translate()
        {
            Caption = EnglishStrings.AddTask.Translate();
            txtShootVideo.Text = EnglishStrings.ShootVideo.Translate();
            txtEditVideo.Text = EnglishStrings.EditVideo.Translate();
            txtStudy.Text = EnglishStrings.Study.Translate();
        }

        private void AddTask_VideoShoot(object sender, RoutedEventArgs e)
        {
            if (TaskClick != null)
            {
                AddVideoDialog videoDialog = new AddVideoDialog();
                videoDialog.ShowDialog(() =>
                    {
                        if (videoDialog.Video != null)
                        {
                            AddVideoDialog2 videoDialog2 = new AddVideoDialog2(videoDialog.Video);
                            videoDialog2.ShowDialog(() =>
                                {
                                    if (videoDialog2.DialogResult == true)
                                    {
                                        var shootVideoTask = new ShootVideo(videoDialog.Video);
                                        shootVideoTask.ExtraHours = videoDialog.Video.ExtraShootingHours;
                                        TaskClick(shootVideoTask);
                                    }
                                });
                        }
                    });
            }
            DialogResult = true;
        }

        private void AddTask_VideoEdit(object sender, RoutedEventArgs e)
        {
            if (TaskClick != null)
            {
                List<Video> completedVideos = new List<Video>();
                foreach (var currentTask in Player.Current.TasksInProgress)
                {
                    var shootTask = currentTask as ShootVideo;
                    if (shootTask != null && !shootTask.Video.HasBeenEdited && currentTask.IsCompleted)
                    {
                        completedVideos.Add(shootTask.Video);
                    }
                }

                foreach (var video in Player.Current.Videos)
                {
                    if (!video.HasBeenEdited && !completedVideos.Contains(video))
                    {
                        completedVideos.Add(video);
                    }
                }

                foreach (var currentTask in Player.Current.TasksInProgress)
                {
                    var editTask = currentTask as EditVideo;
                    if (editTask != null)
                    {
                        completedVideos.Remove(editTask.Video);
                    }
                }

                if (completedVideos.Count == 0)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.TooSoon.Translate(), EnglishStrings.NoVideosToEdit.Translate(), MessagePicture.Puzzle);
                    return;
                }

                EditVideoDialog editDialog = new EditVideoDialog(completedVideos);
                editDialog.ShowDialog(() =>
                    {
                        if (editDialog.Video != null)
                        {
                            var editVideoTask = new EditVideo(editDialog.Video);
                            editVideoTask.ExtraHours = editDialog.Video.ExtraEditingHours;
                            editVideoTask.Episodes = editDialog.Episodes;
                            TaskClick(editVideoTask);
                        }
                    });
            }
            DialogResult = true;
        }

        private void AddTask_Study(object sender, RoutedEventArgs e)
        {
            if (TaskClick != null)
            {
                StudySelectionDialog studyDialog = new StudySelectionDialog();
                studyDialog.ShowDialog(() =>
                    {
                        if (studyDialog.ChosenStudy != null)
                            TaskClick(studyDialog.ChosenStudy);
                    });
            }
            DialogResult = true;
        }

        private void AddTask_VideoRender(object sender, RoutedEventArgs e)
        {
            if (TaskClick != null)
            {
                List<Video> editedVideos = new List<Video>();
                foreach (var currentTask in Player.Current.TasksInProgress)
                {
                    var editTask = currentTask as EditVideo;
                    if (editTask != null && !editTask.Video.HasBeenRendered && currentTask.IsCompleted && editTask.Episodes == 1)
                    {
                        editedVideos.Add(editTask.Video);
                    }
                }

                foreach (var video in Player.Current.Videos)
                {
                    if (!video.HasBeenRendered && !editedVideos.Contains(video) && video.HasBeenEdited)
                    {
                        editedVideos.Add(video);
                    }
                }

                foreach (var currentTask in Player.Current.TasksInProgress)
                {
                    var renderTask = currentTask as RenderVideo;
                    if (renderTask != null)
                    {
                        editedVideos.Remove(renderTask.Video);
                    }
                }

                if (editedVideos.Count == 0)
                {
                    CustomMessageBox.ShowDialog(EnglishStrings.TooSoon.Translate(), EnglishStrings.NoVideosToEdit.Translate(), MessagePicture.Puzzle);
                    return;
                }

                RenderVideoDialog renderDialog = new RenderVideoDialog(editedVideos);
                renderDialog.ShowDialog(() =>
                {
                    if (renderDialog.Video != null)
                    {
                        var renderVideoTask = new RenderVideo(renderDialog.Video);
                        renderVideoTask.ExtraHours = renderDialog.Video.ExtraRenderHours;
                        TaskClick(renderVideoTask);
                    }
                });
            }
            DialogResult = true;
        }
    }
}
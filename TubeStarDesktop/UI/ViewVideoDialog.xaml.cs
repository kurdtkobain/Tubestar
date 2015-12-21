using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for ViewVideoDialog.xaml
    /// </summary>
    public partial class ViewVideoDialog : ChildWindow
    {
        private Video _video;

        public ViewVideoDialog(Video video)
        {
            InitializeComponent();
            _video = video;
            this.AwebView.BeginInit();

            Translate();
            if (video.GetCommentsCount() == 0)
            {
                lblComments.Visibility = commentStack.Visibility = Visibility.Collapsed;
            }
            Populate(video);

            string id = video.YouTubeVideoId;

            if (DateTime.Now.Day == 1 && DateTime.Now.Month == 4)
            {
                id = "oHg5SJYRHA0";
            }

            this.AwebView.Address = String.Format("http://www.youtube.com/embed/{0}?rel=0", id);
        }

        private void Translate()
        {
            Caption = EnglishStrings.ViewVideo.Translate();
            lblComments.Text = String.Format("{0} ({1})", EnglishStrings.Comments.Translate(), _video.GetCommentsCount().ToNumberString());
            btnOk.Content = EnglishStrings.Ok.Translate();
        }

        private void Populate(Video video)
        {
            List<VideoComment> orderedComments = new List<VideoComment>();
            foreach (var item in video.Comments)
            {
                VideoComment likedComment = new VideoComment(item.Comment, item.Likes);
                orderedComments.Add(likedComment);
            }

            foreach (var comment in orderedComments)
            {
                CommentBlock block = new CommentBlock(comment.Comment, comment.Likes);
                commentStack.Children.Insert(0, block);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.AwebView.Address = "about:blank";
            this.AwebView.Dispose();
            this.DialogResult = true;
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            this.AwebView.Address = "about:blank";
            this.AwebView.Dispose();
        }

        private void AwebView_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            string script = "document.body.style.overflow ='hidden'";
            Dispatcher.Invoke(new Action(() =>
            {            
            this.AwebView.WebBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(script);
            }));

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TubeStar
{
    /// <summary>
    /// Interaction logic for CommentBlock.xaml
    /// </summary>
    public partial class CommentBlock : UserControl
    {
        public string Comment { get; private set; }
        public int Likes { get; private set; }

        public CommentBlock()
        { }

        public CommentBlock(string comment, int likes)
        {
            InitializeComponent();

            Comment = comment;
            Likes = likes;

            txtComment.Text = comment;
            txtLikes.Text = likes > 0 ? String.Format("+ {0}", likes) : String.Empty;
        }
    }
}
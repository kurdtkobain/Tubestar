using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace TubeStar
{
    public class VideoComment
    {
        public string Comment { get; set; }
        public int Likes { get; set; }

        public VideoComment()
        { }

        public VideoComment(string comment, int likes)
        {
            Comment = comment;
            Likes = likes;
        }

        public override bool Equals(object obj)
        {
            var other = obj as VideoComment;
            if (other != null)
            {
                return this.Comment.Equals(other.Comment);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Comment.GetHashCode();
        }
    }
}
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace TubeStar
{
    public static class UIHelpers
    {
        public static void PlayAnimation(this FrameworkElement element, Action onCompleted)
        {
            var storyboard = element.FindResource("OnClickAnimation") as Storyboard;
            if (storyboard != null)
            {
                var clone = storyboard.Clone();
                clone.Completed += (s, e) => onCompleted();
                clone.Begin(element);
            }
        }
    }
}
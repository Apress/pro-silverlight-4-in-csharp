using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MultiplePagesTransition
{
    public abstract class PageTransitionBase
    {
        // For better encapsulation these could be private fields wrapped by protected properties.
        protected Storyboard storyboard = new Storyboard();
        protected UserControl oldPage;
        protected UserControl newPage;

        public PageTransitionBase()
        {
            storyboard.Completed += TransitionCompleted;
        }

        public void Navigate(UserControl newPage)
        {
            // Set the pages.
            this.newPage = newPage;
            Grid grid = (Grid)Application.Current.RootVisual;
            oldPage = (UserControl)grid.Children[0];

            // Insert the new page first (so it lies "behind" the old page).
            grid.Children.Insert(0, newPage);

            // Prepared the animation.
            PrepareStoryboard();            

            // Perform the animation.            
            storyboard.Begin();
        }

        private void TransitionCompleted(object sender, EventArgs e)
        {
            // This is the place to perform clean up.
            // In this example the animation acts on the old page,
            // which is discarded after the navigation. Thus, there's no
            // need to explicitly remove the storyboard from the Resources
            // collection of the page.

            // Remove the old page, which is not needed any longer.
            Grid grid = (Grid)Application.Current.RootVisual;
            grid.Children.Remove(oldPage);
        }
        
        // Derived classes override this method to create and configure the animations.
        protected abstract void PrepareStoryboard();
    }

   

    public class WipeTransition : PageTransitionBase
    {              
        protected override void PrepareStoryboard()
        {
            // Create the opacity mask.
            LinearGradientBrush mask = new LinearGradientBrush();
            mask.StartPoint = new Point(0,0);
            mask.EndPoint = new Point(1,0);

            GradientStop transparentStop = new GradientStop();
            transparentStop.Color = Colors.Transparent;
            transparentStop.Offset = 0;            
            mask.GradientStops.Add(transparentStop);
            GradientStop visibleStop = new GradientStop();
            visibleStop.Color = Colors.Black;
            visibleStop.Offset = 0;            
            mask.GradientStops.Add(visibleStop);                  
            
            oldPage.OpacityMask = mask;

            // Create the animations for the opacity mask.          
            DoubleAnimation visibleStopAnimation = new DoubleAnimation();
            Storyboard.SetTarget(visibleStopAnimation, visibleStop);
            Storyboard.SetTargetProperty(visibleStopAnimation, new PropertyPath("Offset"));
            visibleStopAnimation.Duration = TimeSpan.FromSeconds(1.2);
            visibleStopAnimation.From = 0;
            visibleStopAnimation.To = 1.2;

            DoubleAnimation transparentStopAnimation = new DoubleAnimation();
            Storyboard.SetTarget(transparentStopAnimation, transparentStop);
            Storyboard.SetTargetProperty(transparentStopAnimation, new PropertyPath("Offset"));
            transparentStopAnimation.BeginTime = TimeSpan.FromSeconds(0.2);
            transparentStopAnimation.From = 0;
            transparentStopAnimation.To = 1;
            transparentStopAnimation.Duration = TimeSpan.FromSeconds(1);

            // Add the animations to the storyboard.
            storyboard.Children.Add(transparentStopAnimation);
            storyboard.Children.Add(visibleStopAnimation);                           
        }
    }
}

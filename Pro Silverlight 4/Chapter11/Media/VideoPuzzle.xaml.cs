using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Media
{
    public partial class VideoPuzzle : UserControl
    {
        public VideoPuzzle()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private Rectangle previousRectangle;

        // Move the clicked square.
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the square and attach the animation to it.
            Rectangle rectangle = (Rectangle)sender;
            
            // You need to stop the storyboard because the same storyboard is being reused for each piece.
            // The alternative is to dynamically create a new storyboard after each click.
            if (previousRectangle != null) MoveToNewPosition(previousRectangle, squareMoveStoryboard);
            
            Storyboard.SetTarget(squareMoveStoryboard, rectangle);
            
            // Choose a random direction and movement amount.
            Random rand = new Random();
            int sign = 1;
            if (rand.Next(0, 2) == 0) sign = -1;
            leftAnimation.To = Canvas.GetLeft(rectangle) + rand.Next(60,150) * sign;
            topAnimation.To = Canvas.GetTop(rectangle) + rand.Next(60, 150) * sign;

            previousRectangle = rectangle;

            // Start the animation.
            squareMoveStoryboard.Begin();
        }

        // Make the video loop.
        private void videoClip_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoClip.Stop();
            videoClip.Play();
        }

        private void cmdGeneratePuzzle_Click(object sender, RoutedEventArgs e)
        {
            // Get the requested dimensions.
            int rows; int cols;
            Int32.TryParse(txtRows.Text, out rows);
            Int32.TryParse(txtCols.Text, out cols);
            
            if ((rows < 1) || (cols <1))
                return;

            // Clear the surface.
            puzzleSurface.Children.Clear();

            // Determine the rectangle size.
            double squareWidth = puzzleSurface.ActualWidth / cols;
            double squareHeight = puzzleSurface.ActualHeight / rows;
                                    
            // Create the brush for the MediaElement named videoClip.
            VideoBrush brush = new VideoBrush();
            brush.SetSource(videoClip);

            // Create the rectangles.
            double top = 0; double left = 0;
            for (int row = 0; row < rows; row++) 
            {
                for (int col = 0; col < cols; col++)
                {
                    // Create the rectangle. Every rectangle is sized to match the Canvas.
                    Rectangle rect = new Rectangle();                    
                    rect.Width = puzzleSurface.ActualWidth;
                    rect.Height = puzzleSurface.ActualHeight;

                    rect.Fill = brush;
                    SolidColorBrush rectBrush = new SolidColorBrush(Colors.DarkGray);
                    rect.StrokeThickness = 3;
                    rect.Stroke = rectBrush;                                      

                    // Clip the rectangle to fit its portion of the puzzle.
                    RectangleGeometry r = new RectangleGeometry();
                    // 1-pixel correction factor ensures we never get lines in between.
                    r.Rect = new Rect(left, top, squareWidth+1, squareHeight+1);
                    rect.Clip = r;

                    // Handle rectangle clicks.
                    rect.MouseLeftButtonDown += rect_MouseLeftButtonDown;
                                        
                    puzzleSurface.Children.Add(rect);

                    // Go to the next column.
                    left += squareWidth;
                }
                // Go to the next row.
                left = 0;
                top += squareHeight;
            }

            // (Video is on autostart, and is already playing. If not, play here.)
            //videoClip.Play();
        }

        private void MoveToNewPosition(UIElement rectangle, Storyboard squareMoveStoryboard)
        {
            double left = Canvas.GetLeft(rectangle);
            double top = Canvas.GetTop(rectangle);
            squareMoveStoryboard.Stop();
            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);
        }
    }

  
}

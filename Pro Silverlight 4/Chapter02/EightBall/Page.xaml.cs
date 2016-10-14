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

namespace EightBall
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        private void cmdAnswer_Click(object sender, RoutedEventArgs e)
        {
            AnswerGenerator generator = new AnswerGenerator();
            txtAnswer.Text = generator.GetRandomAnswer(txtQuestion.Text);

            LinearGradientBrush brush = new LinearGradientBrush();

            GradientStop gradientStop1 = new GradientStop();
            gradientStop1.Offset = 0;
            gradientStop1.Color = Colors.Yellow;
            brush.GradientStops.Add(gradientStop1);
            
            GradientStop gradientStop2 = new GradientStop();
            gradientStop2.Offset = 0.5;
            gradientStop2.Color = Colors.White;
            brush.GradientStops.Add(gradientStop2);

            GradientStop gradientStop3 = new GradientStop();
            gradientStop3.Offset = 1;
            gradientStop3.Color = Colors.Purple;
            brush.GradientStops.Add(gradientStop3);
            
            grid1.Background = brush;
        }
    }
}

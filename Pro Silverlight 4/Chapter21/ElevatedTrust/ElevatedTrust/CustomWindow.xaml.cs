using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ElevatedTrust
{
    public partial class CustomWindow : UserControl
    {
        public CustomWindow()
        {
            InitializeComponent();
        }

        private void titleBar_MouseLeftButtonDown(object sender,
          System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }

        private void cmdMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void cmdMaximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void cmdClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void rect_Resize(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == rect_TopLeftCorner)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.TopLeft);
            }
            else if (sender == rect_TopEdge)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.Top);
            }
            else if (sender == rect_TopRightCorner)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.TopRight);
            }
            else if (sender == rect_LeftEdge)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.Left);
            }
            else if (sender == rect_RightEdge)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.Right);
            }
            else if (sender == rect_BottomLeftCorner)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomLeft);
            }
            else if (sender == rect_BottomEdge)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.Bottom);
            }
            else if (sender == rect_BottomRightCorner)
            {
                Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomRight);
            }
        }

    }

}

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
using System.Windows.Navigation;

namespace RoutedEvents
{
    public partial class Commands : UserControl
    {
        public Commands()
        {
            InitializeComponent();
        }

    }

    public class PrintTextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool canExecute;
        public bool CanExecute(object parameter)
        {
            // Check if the command can execute.
            // In order to be executable, it must have non-blank text in the command parameter.
            bool canExecuteNow = (parameter != null) && (parameter.ToString() != "");

            // Determine if the CanExecuteChanged event should be raised.
            if (canExecute != canExecuteNow)
            {
                canExecute = canExecuteNow;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return canExecute;
        }        

        public void Execute(object parameter)
        {
            MessageBox.Show("Printing: " + parameter);
        }
    }
}

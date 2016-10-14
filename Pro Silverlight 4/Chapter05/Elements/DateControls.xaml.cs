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

namespace Elements
{
    public partial class DateControls : UserControl
    {
        public DateControls()
        {
            InitializeComponent();
        }

        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            lblError.Text = "'" + e.Text +
                "' is not a valid value because " + e.Exception.Message;            
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {            
            // Check all the newly added items.
            foreach (DateTime selectedDate in e.AddedItems)
            {
                if ((selectedDate.DayOfWeek == DayOfWeek.Saturday) ||
                    (selectedDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    lblError.Text = "Weekends are not allowed";

                    ((Calendar)sender).SelectedDates.Remove(selectedDate);
                }
            }

        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Multithreading
{
    public class FindPrimesInput
    {
        public int To
        { get; set; }

        public int From
        { get; set; }

        public FindPrimesInput(int from, int to)
        {
            To = to;
            From = from;
        }

    }
}

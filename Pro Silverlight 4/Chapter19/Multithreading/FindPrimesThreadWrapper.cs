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
    
    public class FindPrimesThreadWrapper : ThreadWrapperBase
    {
        public event EventHandler<FindPrimesCompletedEventArgs> Completed;

        // Store the input and output information.
        private int fromNumber, toNumber;
        private int[] primeList;

        public FindPrimesThreadWrapper(int from, int to)
        {
            this.fromNumber = from;
            this.toNumber = to;
        }

        protected override void DoTask()
        {
            int[] list = new int[toNumber - fromNumber];

            // Create an array containing all integers between the two specified numbers.
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = fromNumber;
                fromNumber += 1;
            }


            //find out the module for each item in list, divided by each d, where
            //d is < or == to sqrt(to)
            //if the remainder is 0, the nubmer is a composite, and thus
            //we mark its position with 0 in the marks array,
            //otherwise the number is a prime, and thus mark it with 1
            int maxDiv = (int)Math.Floor(Math.Sqrt(toNumber));

            int[] mark = new int[list.Length];

            int lastPrime;

            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 2; j <= maxDiv; j++)
                {

                    if ((list[i] != j) && (list[i] % j == 0))
                    {
                        mark[i] = 1;
                        lastPrime = list[i];
                    }

                }


                int iteration = list.Length / 100;
                if (i % iteration == 0)
                {
                    if (CancelRequested)
                    {
                        // Return without doing any more work.
                        return;
                    }                    
                }

            }

            //create new array that contains only the primes, and return that array
            int primes = 0;
            for (int i = 0; i < mark.Length; i++)
            {
                if (mark[i] == 0) primes += 1;

            }

            int[] ret = new int[primes];
            int curs = 0;
            for (int i = 0; i < mark.Length; i++)
            {
                if (mark[i] == 0)
                {
                    ret[curs] = list[i];
                    curs += 1;
                }
            }

            primeList = ret;
            OnCompleted();
        }

        protected override void OnCompleted()
        {
            // Signal that the operation is complete.
            if (Completed != null)
                Completed(this,
                new FindPrimesCompletedEventArgs(fromNumber, toNumber, primeList));
        }

        // Allow access to the result once the task is finished.
        public int[] GetResultOfLastTask()
        {            
            if (Status == StatusState.Completed)
                return primeList;
            else
                return null;
        }
    }


    public class FindPrimesCompletedEventArgs : EventArgs
    {
        private int[] primeList;
        public int[] PrimeList
        {
            get { return primeList; }
            set { primeList = value; }
        }

        private int from;
        public int From
        {
            get { return from; }
            set { from = value; }
        }

        private int to;
        public int To
        {
            get { return to; }
            set { to = value; }
        }

        public FindPrimesCompletedEventArgs(int from, int to, int[] primeList)
        {
            From = from;
            To = to;
            PrimeList = primeList;
        }
    }
}

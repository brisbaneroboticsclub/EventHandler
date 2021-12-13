using System;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Program starts here.");

            Console.WriteLine($"Initiate a new class called counter as c, create new entry as random number.");
            Counter c = new Counter(new Random().Next(10));

            Console.WriteLine($"Maybe Run void c_ThresholdReached.");
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increase total");

            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");

                Console.WriteLine("Call void Add.");
                c.Add(1);
            }
        }

        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine($"Start static void c_ThresholdReached.");

            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);

            Console.WriteLine($"Now exit Environment with 0.");
            Environment.Exit(0);

            Console.WriteLine($"End static void c_ThresholdReached.");
        }
    }

    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            Console.WriteLine($"Start public Counter");

            threshold = passedThreshold;

            Console.WriteLine($"Initiate passedThreshold.{passedThreshold}");

            Console.WriteLine($"End public Counter");
        }

        public void Add(int x)
        {
            Console.WriteLine($"Start public void Add.");

            Console.WriteLine($"Add.{x}");

            total += x;

            Console.WriteLine($"Total.{total}");
            Console.WriteLine($"Threshold.{threshold}");

            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
            Console.WriteLine("End public void Add.");
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set;}

}
}
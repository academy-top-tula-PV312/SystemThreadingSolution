using System.Threading;

namespace ThreadingApp
{
    internal class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
            {
                Thread thread = new(IncCount);
                thread.Name = "Thread " + (i + 1).ToString();
                thread.Start();
            }

            
            Console.WriteLine(count);
        }

        static void IncCount()
        {
            for (int i = 0; i < 10; i++)
            {
                count++;
                Console.WriteLine($"{Thread.CurrentThread.Name}: {count}");
                Thread.Sleep(300);
            }
                
        }
    }
}

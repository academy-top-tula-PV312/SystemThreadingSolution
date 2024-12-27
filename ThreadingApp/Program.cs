using System.Threading;

namespace ThreadingApp
{
    public class Program
    {
        
         
        static void Main(string[] args)
        {
            for(int i = 0; i < 10;  i++)
            {
                Car car = new(i + 1);
            }
        }
    }

    public class Car
    {
        int count = 5;
        Thread carThread;

        static Semaphore semaphore = new Semaphore(4, 4);
        static Random random = new Random();

        public Car(int id)
        {
            carThread = new(Parcking);
            carThread.Name = $"Car {id}";
            carThread.Start();
        }

        public void Parcking()
        {
            while(count > 0)
            {
                semaphore.WaitOne();
                Console.WriteLine($"{Thread.CurrentThread.Name} IN parking");

                Thread.Sleep(random.Next(200, 400));

                Console.WriteLine($"{Thread.CurrentThread.Name} OUT parking");
                semaphore.Release();

                count--;

                Thread.Sleep(random.Next(200, 400));
            }
        }
    }
}

using System.Threading.Channels;
using System.Threading.Tasks;

namespace TaskApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(GaussTask(1000));

            Task<int> task2 = new(GaussAmount, 2000);
            task2.Start();

            Console.WriteLine(task2.Result);
        }

        static int GaussTask(int n)
        {
            Task<int> amountTask = new(
                () =>
                {
                    return GaussAmount(n);
                });

            amountTask.Start();

            int result = amountTask.Result;
            return result;
        }

        static int GaussAmount(object? maxObj)
        {
            int maxNumber = (int)maxObj!;
            int result = 0;
            for (int i = 1; i <= maxNumber; i++)
                result += i;

            //Thread.Sleep(1000);
            return result;
        }

         
    }
}

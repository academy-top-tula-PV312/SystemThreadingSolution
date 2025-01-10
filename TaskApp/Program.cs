using System.Threading.Channels;
using System.Threading.Tasks;

namespace TaskApp
{
    internal class Program
    {
        static Random random = new Random();
        
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //Task task = new Task(() =>
            //{
            //    int i = 0;

            //    token.Register(() =>
            //    {
            //        Console.WriteLine("Task cancel");
            //        i = 50;
            //    });

            //    // Task loop
            //    for (; i < 50; i++)
            //    {
            //        // Non exception cancelling
            //        //if (token.IsCancellationRequested)
            //        //{
            //        //    Console.WriteLine("Task cancel");
            //        //    return;
            //        //}

            //        //if(token.IsCancellationRequested)
            //        //{
            //        //    Console.WriteLine("Task cancel");
            //        //    token.ThrowIfCancellationRequested();
            //        //}



            //        Console.WriteLine($"\tTask {i} -> {i * i}");
            //        Thread.Sleep(200);
            //    }
            //}, token);

            Task task = new Task(() => LoopPrint(token), token);

            task.Start();

            // Main loop
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Main {i} -> {i * i}");
                Thread.Sleep(200);
                if(i == 20)
                    cts.Cancel();
            }

            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cts.Dispose();
            }
            

            Console.WriteLine($"Status task: {task.Status}");
        }

        static void LoopPrint(CancellationToken token)
        {
            for(int i = 0; i < 100; i++)
            {
                if(token.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancel");
                    return;
                }
                Console.WriteLine($"{i} -> {i * i}");
                Thread.Sleep(100);
            }
        }

        

         
    }
}

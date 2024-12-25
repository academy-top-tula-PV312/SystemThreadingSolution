using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApp
{
    record class Args(int max, int sleep);
    public static class Examples
    {
        public static void ThreadWelcomeExample()
        {
            Thread thread = Thread.CurrentThread;
            thread.Name = "Main";

            Console.WriteLine($"Thread Name: {thread.Name}");
            Console.WriteLine($"Thread Domain: {Thread.GetDomain().FriendlyName}");
            Console.WriteLine($"Thread Priority: {thread.Priority}");
            Console.WriteLine($"Thread State: {thread.ThreadState}");

            Thread threadLoop = new Thread(Loop);
            Thread threadLoop2 = new Thread(Loop);
            threadLoop.Start(new Args(10, 500));
            threadLoop2.Start(new Args(15, 400));

            int id = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread {id}: {i}");
                Thread.Sleep(500);
            }
        }

        static void Loop(object? argsObj)
        {
            if (argsObj is Args args)
            {
                int id = Thread.CurrentThread.ManagedThreadId;

                for (int i = 0; i < args.max; i++)
                {
                    Console.WriteLine($"Thread {id}: {i}");
                    Thread.Sleep(args.sleep);
                }
            }
        }
    }
}

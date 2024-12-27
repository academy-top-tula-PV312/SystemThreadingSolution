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
        static int count = 0;
        static object locker = new object();
        static Mutex mutex = new Mutex();
        static AutoResetEvent resetEvent = new AutoResetEvent(true);
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

        public static void LockersExample()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 5; i++)
            {
                Thread thread = new(IncCount4);
                threads.Add(thread);
                thread.Name = "Thread " + (i + 1).ToString();
                thread.Start();
            }

            foreach (Thread thread in threads)
                thread.Join();


            Console.WriteLine(count);
        }

        static void IncCount()
        {
            for (int i = 0; i < 10000; i++)
            {
                lock (locker)
                {
                    count++;
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {count}");
                }
                //Thread.Sleep(200);
            }
        }

        static void IncCount2()
        {

            for (int i = 0; i < 10000; i++)
            {
                bool exitLock = false;
                try
                {
                    Monitor.Enter(locker, ref exitLock);
                    count++;
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {count}");
                }
                finally
                {
                    if (exitLock)
                        Monitor.Exit(locker);
                }
                //Thread.Sleep(200);
            }
        }

        static void IncCount3()
        {

            for (int i = 0; i < 10000; i++)
            {

                mutex.WaitOne();
                count++;
                Console.WriteLine($"{Thread.CurrentThread.Name}: {count}");
                mutex.ReleaseMutex();

                //Thread.Sleep(200);
            }
        }

        static void IncCount4()
        {

            for (int i = 0; i < 10000; i++)
            {

                resetEvent.WaitOne();
                count++;
                Console.WriteLine($"{Thread.CurrentThread.Name}: {count}");
                resetEvent.Set();

                //Thread.Sleep(200);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp
{
    internal static class Examples
    {
        public static void TaskWelcomeExample()
        {
            Task task1 = new Task(SayHello);
            Task task2 = new Task(() => Console.WriteLine($"{Task.CurrentId} Good by world"));
            task1.RunSynchronously();
            task2.RunSynchronously();

            Task task3 = Task.Factory.StartNew(() => Console.WriteLine($"{Task.CurrentId} Hello people"));

            Task task4 = Task.Run(() => Console.WriteLine($"{Task.CurrentId} Good by people"));


            task1.Wait();
            task2.Wait();
            task3.Wait();
            task4.Wait();
        }

        static void SayHello() => Console.WriteLine($"{Task.CurrentId} Hello world");

        public static void OuterInnerTaskExample()
        {
            Task outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task starting");

                Task inner = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inner task starting");
                    Thread.Sleep(1000);
                    Console.WriteLine("Inner task finished");
                }, TaskCreationOptions.AttachedToParent);

                inner.Wait();
                Console.WriteLine("Outer task finished");
            });

            outer.Wait();
            Console.WriteLine("Main finished");
        }

        public static void TaskArrayExample()
        {
            List<Task> tasks = new List<Task>();
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                Task task = new(() =>
                {
                    Console.WriteLine($"Task {Task.CurrentId} starting");
                    Thread.Sleep(random.Next(400, 1000));
                    Console.WriteLine($"Task {Task.CurrentId} finished");
                });
                tasks.Add(task);
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
            //Task.WaitAny(tasks.ToArray());
            Console.WriteLine("Main finished");
        }
    }
}

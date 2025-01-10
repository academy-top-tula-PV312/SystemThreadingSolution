using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp
{
    internal static class Examples
    {
        static Random random = new Random();

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

        public static void TaskREsultExaqmple()
        {
            //Console.WriteLine(GaussTask(1000));

            Task<int> task2 = new Task<int>(GaussAmount, 2000);
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

        public static void ParallelExample()
        {
            List<Action> actionList = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                actionList.Add(() => TaskPrint(random.Next(8, 15)));
            }

            //Parallel.Invoke(actionList.ToArray());

            //Parallel.For(8, 15, TaskPrint);

            List<int> rndList = new();
            for (int i = 0; i < 5; i++)
                rndList.Add(random.Next(8, 15));
            for (int i = 0; i < rndList.Count; i++)
                Console.Write($"{rndList[i]} ");
            Console.WriteLine();

            Parallel.ForEach(rndList, TaskPrint);
        }

        static void TaskPrint(int n)
        {

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Task Print {n} - {i}");
                Thread.Sleep(random.Next(300, 600));
            }
        }
    }
}

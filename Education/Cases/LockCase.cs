using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases {
    public class LockCase : ICase {
        private object _lockObj = new object();
        private readonly List<int> _shareableCollection = new List<int> { 1 };

        public async Task RunAsync() {
            object lock1 = new object();
            object lock2 = new object();
            Console.WriteLine("Starting...");
            var task1 = Task.Run(() =>
            {
                lock (lock1) {
                    Thread.Sleep(1000);
                    lock (lock2) {
                        Console.WriteLine("Finished Thread 1");
                    }
                }
            });

            var task2 = Task.Run(() =>
            {
                lock (lock2) {
                    Thread.Sleep(1000);
                    lock (lock1) {
                        Console.WriteLine("Finished Thread 2");
                    }
                }
            });

            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished...");
        }

        private Task AddNumberAsync(int number) {
            return Task.Run(() => {
                Thread.Sleep(1000);
                _shareableCollection.Add(number);
            });
        }

        private Task RemoveNumberAsync(int number) {
            return Task.Run(async () => {
                await Task.Delay(200);
                _shareableCollection.Remove(number);
            });
        }

        private void ShowCollectionMembers(IEnumerable<int> data) {
            foreach (var item in data) {
                Console.Write($"{item} ");
            }
        }

        private Task ShowCollectionMembersAsync(IEnumerable<int> data, int times) {
            if (times <= 0 || data == null) {
                throw new Exception();
            }

            return Task.Run(() => {
                var passedTimes = 0;

                while (passedTimes != times) {
                    lock (_lockObj) {
                        Thread.Sleep(1000);
                        ShowCollectionMembers(data);
                    }
                }
            });
        }
    }
}


using Education.Core;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    internal class SyncCase : ICase
    {
        private readonly object syncObj = new object();

        public async Task RunAsync() {
            // await InvokeWithListAsync();
            await InvokeWithConcCollectionAsync();
        }

        private async Task InvokeWithConcCollectionAsync() {
            var concBag = new ConcurrentBag<int>();
            var counter = 0; // common counter to share by threads.
            var firstTask = Task.Run(() => {
                for (var i = 0; i < 5; i++) {
                    concBag.Add(i);
                    lock (syncObj) {
                        counter++;
                    }
                    Interlocked.Increment(ref counter);
                }
            });
            var secondTask = Task.Run(() => {
                for (var i = 5; i < 10; i++) {
                    concBag.Add(i);
                    Interlocked.Increment(ref counter);
                }
            });
            await Task.WhenAll(firstTask, secondTask);

            foreach (var item in concBag) {
                await Console.Out.WriteLineAsync(item.ToString());
            }
        }

        private async Task InvokeWithListAsync() {
            var list = new List<int>();
            await AddAsync(list);
            foreach (var item in list) {
                await Console.Out.WriteLineAsync(item.ToString());
            }
        }

        private async Task AddAsync(List<int> list) {
            var firstTask = Task.Run(() => {
                for (var i = 0; i < 5; i++) {
                    AddItem(i, list);
                }
            });
            var secondTask = Task.Run(() => {
                for (var i = 5; i < 10; i++) {
                    AddItem(i, list);
                }
            });

            await Task.WhenAll(firstTask, secondTask);
        }

        private void AddItem(int item, List<int> list) {
            lock (syncObj) { // without lock different threads can get access to same pieces of memory overwriting it
                list.Add(item);
            }
        }
    }
}

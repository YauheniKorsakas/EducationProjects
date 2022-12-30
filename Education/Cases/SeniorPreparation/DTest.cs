using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.SeniorPreparation
{
    class DTest : ICase
    {
        public async Task RunAsync() {
            await Invoke();
            Console.WriteLine("Run Async");
        }

        private async Task Invoke() {
            var task = new Task(() => { Thread.Sleep(1000); Console.WriteLine("From executed task"); });
            task.Start();
            Console.WriteLine("Task is processed");
            task.Wait();
            Console.WriteLine("After processing");
            await Task.Delay(1);
        }
    }


    public class LoadOperation : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            Console.WriteLine("From dispose");
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LoadOperation()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~LoadOperation() {
            Console.WriteLine("From finalizer");
        }
    }
}

using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases
{
    class AsyncDisposableCase : ICase
    {
        public async Task RunAsync() {
            await using var person = new DisposablePerson();
        }
    }

    public class DisposablePerson : IAsyncDisposable // IDisposable should be implemented too.
    {
        public async ValueTask DisposeAsync() {
            await Task.Delay(1000);
            Console.WriteLine("Resources have been cleaned up.");
        }
    }

    public class RegularDisposable : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
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
        // ~RegularDisposable()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

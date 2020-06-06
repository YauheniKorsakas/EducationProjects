using Education.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases {
    public class Entity : IDisposable {

        #region IDisposable Support
        private static Timer timer;
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~Entity() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // Finalizer is in what desstructor compiles.
        }

        public void StartTimer() {
            new Timer((state) => {
                Console.WriteLine("Timer invocation.");
            }, null, 0, 1000);
        }
        #endregion

    }

    public class InheritedEntity : Entity {

    }

    public class DisposeCase : ICase {
        public async Task RunAsync() {
            InitObject();
            GC.Collect();
            await Task.Delay(2000);
        }

        private void InitObject() {
            var obj = new Entity();
            obj.StartTimer();
        }

        private void InitObjectAndDispose() {
            using var obj = new Entity();
        }
        private void InitInheritedEntity() {
            var obj = new InheritedEntity();
        }

    }
}

using System;
using System.Threading.Tasks;

namespace Education.Core {
    public static class CaseRunner {
        public static async Task RunCaseAsync<T>() where T : ICase, new() {
            try {
                await Activator.CreateInstance<T>().RunAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } finally {
                Console.WriteLine("Runner invocation has been completed.");
            }
        }
    }
}

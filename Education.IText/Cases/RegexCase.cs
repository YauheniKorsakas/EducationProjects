using Education.Core;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Education.IText.Cases {
    public class RegexCase : ICase {
        private string _source;
        private Regex _regex;

        public async Task RunAsync() {
            Test1();
        }

        private void Test1() {
            _source = "some string data before $80255 Total Cost";
            _regex = new Regex(@"[$].*(?=Total Cost)");

            var result = _regex.Match(_source).Value.Trim();
        }
    }
}

using Education.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class RegexpCase : ICase
    {
        const string Number = "123-123-444444";
        const string Sentence = "I am bigger 123 than you";
        public Task RunAsync()
        {
            InvokeRegularExpression();

            return Task.CompletedTask;
        }

        private void InvokeRegularExpression()
        {
            var sentenceRegExp = new Regex(@"\w*");
            var numberRegExp = new Regex(@"\d{3}-\d{3}-\d{4}");

            var sentenceResult = sentenceRegExp.Matches(Sentence);
            var numberResult = numberRegExp.IsMatch(Number);
        }


    }
}

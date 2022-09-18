using Education.Core;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Problems
{
    // распарсить строку, смапить результаты подстрок в арабские цифры и скложить строки. Потом ту инт
    // парсинг: 
    // . Описать три правила расположения символов.
    // . получая каждый новый символ из строки, сравнивать его с одним предыдущим и 3 последующими. Сгруппировать символы строки по этим правилам.


    public class RomanToInteger : ICase
    {
        private Dictionary<char, int> map = new Dictionary<char, int> {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };

        private Tuple<char, char, char>[] rules = new Tuple<char, char, char>[] { // В приоритет поставить 3 значение.
                new Tuple<char, char, char>('V', 'X', 'I'),
                new Tuple<char, char, char>('L', 'C', 'X'),
                new Tuple<char, char, char>('D', 'M', 'C')
            };

        public async Task RunAsync() {
            var input = "MCMXCIV";
            var result = RomanToInt(input);

            Console.WriteLine(result);
        }

        private int RomanToInt(string input) {
            var result = 0;

            for (var i = input.Length - 1; i >= 0; i--) {
                var (validArabicNumber, nextIndex) = FindNearestArabicNumber(i, input);
                result += validArabicNumber;

                if (nextIndex < 0) return result;

                i = nextIndex + 1;
            }

            return result;
        }

        private (int, int) FindNearestArabicNumber(int currentIndex, string input) {
            var maxStepBack = 1;
            int result = 0;

            foreach (var rule in rules) {
                if (input[currentIndex] == rule.Item3) { // Проверка текущего и слева от него на 3
                    var nextIndex = currentIndex - 1;
                    result = map[rule.Item3];

                    while (nextIndex != -1) {
                        var nextItem = input[nextIndex];

                        if (nextItem == rule.Item1 || nextItem == rule.Item2) {
                            result += map[nextItem];
                            var newIndex = --nextIndex;

                            return (result, newIndex);
                        }

                        result += map[input[nextIndex]];
                        nextIndex--;
                    }
                }

                if (input[currentIndex] == rule.Item1 || input[currentIndex] == rule.Item2) { // Проверка текущего и слева от него на 1.
                    var nextIndex = currentIndex - maxStepBack;

                    if (nextIndex >= 0 && input[nextIndex] == rule.Item3) {
                        var newIndex = nextIndex--;

                        return (map[input[currentIndex]] - map[input[nextIndex]], newIndex);
                    }

                    return (map[input[currentIndex]], nextIndex);
                }
            }

            return (result, -1);
        }
    }
}

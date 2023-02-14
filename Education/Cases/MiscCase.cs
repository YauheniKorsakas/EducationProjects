using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            // 500 500 1000
            var result = GetResult();
            //var (firstNode, secondNode) = GetNodes();
            //Console.WriteLine(GetStringNumberFromNode(firstNode));

            // var res = GetNodeFromString("500");
        }

        private ListNode GetResult() {
            // 500 500 1000
            var (firstNode, secondNode) = GetNodes();
            var firstNumber = nuint.Parse(GetStringNumberFromNode(firstNode)); // заменить на лист
            var secondNumber = nuint.Parse(GetStringNumberFromNode(secondNode)); // заменить на лист
            var sum = firstNumber + secondNumber; // алгоритм сложения с конца поэлементно; получаем новый лист элементов
            var reversedNumAsString = string.Join("", sum.ToString().Reverse()); // парсим полученный лист в строку
            var result = GetNodeFromString(reversedNumAsString); // получаем ноду из полученной строки

            return result;
        }

        private ListNode GetNodeFromString(string source, int currentIndex = -1) {
            currentIndex++;
            var result = new ListNode {
                val = int.Parse(source.ElementAt(currentIndex).ToString()),
                next = currentIndex == source.Length -1 ? null : GetNodeFromString(source, currentIndex)
            };

            return result;
        }

        private string GetStringNumberFromNode(ListNode node, string currentString = "") {
            if (node.next is not null) {
                currentString += GetStringNumberFromNode(node.next, currentString);
            }
            currentString += node.val.ToString();

            return currentString;
        }

        private (ListNode, ListNode) GetNodes() {
            var firstNumbersNode = new ListNode {
                val = 9,
                next = null
            };

            var secondNumbersNode = new ListNode {
                val = 1,
                next = new ListNode {
                    val = 9,
                    next = new ListNode {
                        val = 9,
                        next = new ListNode {
                            val = 9,
                            next = new ListNode {
                                val = 9,
                                next = new ListNode {
                                    val = 9,
                                    next = new ListNode {
                                        val = 9,
                                        next = new ListNode {
                                            val = 9,
                                            next = new ListNode {
                                                val = 9,
                                                next = new ListNode {
                                                    val = 9,
                                                    next = null
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return (firstNumbersNode, secondNumbersNode);
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null) {
            this.val = val;
            this.next = next;
        }
    }
}
//using Education.Core;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System;
//using System.Linq;

//namespace Education.Cases {
//    public class Node {
//        public string Name { set; get; }
//        public List<Node> Childs { set; get; }
//    }

//    public class GraphCase : ICase {
//        public async Task RunAsync() {
//            var graph = GetGraph();
//            BFS(graph);
//        }

//        private static List<Node> processedNodes = new List<Node>();

//        private void BFS(Node graph) {
//            if (!processedNodes.Contains(graph)) {
//                Console.WriteLine(graph.Name);
//                processedNodes.Add(graph);
//            }

//            var names = graph?.Childs?.Select(s => s.Name) ?? Enumerable.Empty<string>();

//            foreach (var childName in names) {
//                Console.Write($"{childName} ");
//            }
//            Console.WriteLine();

//            graph?.Childs?.ForEach(item => {
//                BFS(item);
//            });
//        }

//        private Node GetGraph() {
//            return new Node {
//                Name = "1",
//                Childs = new List<Node> {
//                    new Node {
//                        Name = "1.1",
//                        Childs = new List<Node> {
//                            new Node {
//                                Name = "1.1.1"
//                            }
//                        }
//                    },
//                    new Node {
//                        Name = "1.2"
//                    }
//                }
//            };
//        }
//    }
//}

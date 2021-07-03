using System.Collections.Generic;
using System.Linq;
using System;

namespace Education.Cases.Data
{
    public class Node
    {
        public List<Node> Nodes { get; set; } = new List<Node>(); 
        public string Value { get; set; }
        public bool IsVisited { set; get; }

        public Node(string value)
        {
            Value = value;
        }

        public Node()
        {

        }

        public static void ShowTree(Node node)
        {
            Console.WriteLine($"Current value: {node.Value}");
            Console.WriteLine("Its children: ");

            foreach (var currentNode in node.Nodes)
            {
                Console.WriteLine(node.Value);

                ShowTree(node);
            }

            Console.WriteLine("\n\n");
        }
    }
}

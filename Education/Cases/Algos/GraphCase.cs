using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Cases.Data;

namespace Education.Cases.Algos
{
    public class GraphCase : ICase
    {
        public Task RunAsync()
        {
            ExecuteCase();

            return Task.CompletedTask;
        }

        private void ExecuteCase()
        {
            var node = GetNode();
            BFS(node);
        }

        private Node GetNode()
        {
            var zheka = new Node("Zheka");
            var serega = new Node("serega");
            var tolyan = new Node("tolyan");
            var art = new Node("art");

            zheka.Nodes.Add(serega);
            zheka.Nodes.Add(art);
            zheka.Nodes.Add(tolyan);

            serega.Nodes.Add(zheka);
            serega.Nodes.Add(art);

            art.Nodes.Add(zheka);
            art.Nodes.Add(serega);

            tolyan.Nodes.Add(zheka);

            return zheka;
        }

        private void BFS(Node node)
        {
            var searched = new Queue<Node>();
            var searchQueue = new Queue<Node>();
            searchQueue.Enqueue(node);

            while (searchQueue.Any())
            {
                var currentNode = searchQueue.Dequeue();
                if (!searched.Contains(currentNode))
                {
                    Console.WriteLine(currentNode.Value);
                    searched.Enqueue(currentNode);

                    currentNode.Nodes.ForEach(item => searchQueue.Enqueue(item));
                }
            }
        }
    }
}

// поиск кратчайшего пути - это поиск в ширину.
// существует ли путь от а до б - это тоже поиск в ширину.
// Граф - набор связей.
// Если не знаете как упорядочить дела - можно сделать граф зависимостей.
// граф у которого нет ребер, указывающих в обратном направлении это дерево
// BFS - обход в ширину. Количество ребер ищет.
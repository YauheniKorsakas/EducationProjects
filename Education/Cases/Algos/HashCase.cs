using Education.Core;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Education.Cases.Algos
{
    public class HashCase : ICase
    {
        public Task RunAsync()
        {
            HashFunctions();

            return Task.CompletedTask;
        }

        private void HashFunctions()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 5, 6, 6 };
            var hashset = new HashSet<int>(list);
            foreach (var item in hashset)
            {
                Console.WriteLine(item);
            }
        }

    }
}

// Hash function должна быть идемпотентной - возвращать один результат на то же входное значение.
// hash tables определяют место хранения элементов в памяти с помощью хэш функций.
// hashvalue - это индекс для ячейки в памяти, которая находится под капотом хеш таблицы. Это значение генерится хеш функцией. Колизия эт когда это валюе одинаковое для двух ключей.
/*The hash table contains a list of buckets, and in those buckets it stores one or more values. A hash function is used to compute an index based on the key.
 * When we insert an item into our container, it will be added to the bucket designated by the calculated index.*/
/*
 One of the most important concepts in the implementation of a hash map is the way how we handle when two different keys end up
having the same hash value, thus their index will point to the same bucket. There are many different approaches, but arguably two of them are the most prominent.*/
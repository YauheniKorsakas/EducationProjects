using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class DynamicProgrammingCase : ICase
    {
        public Task RunAsync() {

            return Task.CompletedTask;
        }

        private void Execute() {

        }
    }
}

// Динамическое программирование - решение задач путем их разбиения на мелкие.
// В динамическом программировании нет возможности дробить предметы.
// Работает только тогда когда подзадачи не зависят друг от друга.
// надо рисовать таблицу.
// ДП применяется для оптимизации некоторой характеристики.
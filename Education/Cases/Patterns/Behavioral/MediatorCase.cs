using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Mediator
{
    // like observer, but mediator decides how what and who will receive a message
    // whereas obserable itself contains a logic to provide state update
    // the more objects to interract the harder it is to handle relationships between them properly
    public class MediatorCase : ICase
    {
        public async Task RunAsync() {
            IEmployeeGroup group = new EmployeeGroup();
            var zheka = new Employee(1, "zheka", group);
            var sergey = new Employee(2, "sergey", group);
            group.RegisterEmployee(zheka);
            group.RegisterEmployee(sergey);

            zheka.ReceiveMoneyFromSideServices(1000);
            group.ShowAllEarnedMoneyByAllEmployees();
            sergey.ReceiveMoneyFromSideServices(2000);
            group.ShowAllEarnedMoneyByAllEmployees();
        }

        public class Employee
        {
            public int Id { get; }
            public string Name { get; }
            public double EarnedMoney { get; private set; }
            private readonly IEmployeeGroup employeeGroup;

            public Employee(int id, string name, IEmployeeGroup employeeGroup) {
                this.employeeGroup = employeeGroup;
                this.Id = id;
                this.Name = name;
            }

            public void ReceiveMoneyFromSideServices(double money) {
                employeeGroup.IssueMoneyToGroup(money);
            }

            public void ReceiveMoneyFromGroup(double money) {
                if (money < 0)
                    throw new ArgumentException("Invalid amount of money.");

                EarnedMoney += money;
            }
        }

        public interface IEmployeeGroup
        {
            void RegisterEmployee(Employee employee);
            void IssueMoneyToGroup(double money);
            void ShowAllEarnedMoneyByAllEmployees();
        }

        public class EmployeeGroup : IEmployeeGroup
        {
            private const int MaxEmployeesCount = 10;
            private readonly Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

            public void RegisterEmployee(Employee employee) {
                if (employee is null || employees.ContainsKey(employee.Id)) {
                    throw new ArgumentException($"Cannot register invalid employee ({employee.Id} {employee.Name}).");
                }

                if (employees.Count == MaxEmployeesCount) {
                    throw new BusinessException("Max count of group has been reached.");
                }

                employees.Add(employee.Id, employee);
                Console.WriteLine($"Employee {employee.Id} {employee.Name} registered.");
            }

            public void IssueMoneyToGroup(double money) {
                if (money < 0) {
                    throw new BusinessException("invalid amount of money.");
                }

                var personalEarnings = money / employees.Count;

                foreach (var empl in employees) {
                    empl.Value.ReceiveMoneyFromGroup(personalEarnings);
                }
            }

            public void ShowAllEarnedMoneyByAllEmployees() {
                Console.WriteLine("\n");
                foreach (var empl in employees) {
                    Console.WriteLine($"{empl.Key} {empl.Value.Name} => {empl.Value.EarnedMoney}");
                }
            }
        }
    }
}

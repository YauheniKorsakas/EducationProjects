using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Structural.Decorator
{
    public class DecoratorCase : ICase
    {
        public async Task RunAsync()
        {
            IPayrollService payrollService = new PayrollService();
            IPayrollService bonusPayrollService = new BonusPayrollService(payrollService);
            var jun = new Employee { 
                Name = "oleg",
                Salary = 900,
                Experience = 2,
                Grade = Grade.Junior
            };
            var middle = new Employee {
                Name = "artsiom",
                Salary = 1000,
                Experience = 3,
                Grade = Grade.Middle
            };

            var junSalary = payrollService.RecalculateEmployeeSalary(jun);
            var junSalaryWithBonus = bonusPayrollService.RecalculateEmployeeSalary(jun);
        }

        public enum Grade
        {
            Junior = 1,
            Middle,
            Senior
        }

        public class Employee {
            public string Name { get; set; }
            public int Salary { get; set; }
            public int Experience { get; set; }
            public Grade Grade { get; set; }
        }

        public interface IPayrollService
        {
            int RecalculateEmployeeSalary(Employee employee);
        }

        public class PayrollService : IPayrollService
        {
            public int RecalculateEmployeeSalary(Employee employee) {
                var result = 1000 + employee.Experience * 200 + (int)employee.Grade * 200;

                return result;
            }
        }

        public class BonusPayrollService : IPayrollService
        {
            private readonly IPayrollService payrollService;

            public BonusPayrollService(IPayrollService payrollService)
            {
                this.payrollService = payrollService;
            }

            public int RecalculateEmployeeSalary(Employee employee) {
                var baseValue = payrollService.RecalculateEmployeeSalary(employee);

                return baseValue += GetBonus();
            }

            private int GetBonus() {
                // opeartions to get bonus from docs or from database
                return 500;
            }
        }
    }
}

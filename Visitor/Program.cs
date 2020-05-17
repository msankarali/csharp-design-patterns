using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager muharrem = new Manager { Name = "Muharrem" , Salary = 1000};
            Manager samet = new Manager { Name = "Samet" , Salary = 1200};

            Worker servet = new Worker{Name = "Servet", Salary = 900};
            Worker alihan = new Worker{Name = "Alihan", Salary = 950};

            samet.Subortinates.Add(alihan);
            samet.Subortinates.Add(servet);
            muharrem.Subortinates.Add(servet);

            OrganisationalStructure organisationalStructure = new OrganisationalStructure(samet);
            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();

            organisationalStructure.Accept(payrollVisitor);
            organisationalStructure.Accept(payriseVisitor);

            Console.ReadLine();
        }
    }

    class OrganisationalStructure
    {
        private EmployeeBase Employee;

        public OrganisationalStructure(EmployeeBase firstEmployee)
        {
            Employee = firstEmployee;
        }

        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }
    }

    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    class Manager : EmployeeBase
    {
        public Manager()
        {
            Subortinates = new List<EmployeeBase>();
        }
        public List<EmployeeBase> Subortinates { get; set; }

        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);

            foreach (var employee in Subortinates)
            {
                employee.Accept(visitor);
            }
        }
    }

    class Worker : EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }

    abstract class VisitorBase
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }

    class PayrollVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}", worker.Name, worker.Salary);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} paid {1}", manager.Name, manager.Salary);
        }
    }

    class PayriseVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to {1}", worker.Name, worker.Salary * (decimal)1.1);

        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased to {1}", manager.Name, manager.Salary * (decimal)1.2);

        }
    }
}

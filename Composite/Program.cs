using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee muharrem = new Employee
            {
                Name = "Muharrem Servet"
            };
            Employee alihan = new Employee
            {
                Name = "Alihan"
            };
            Employee ensar = new Employee
            {
                Name = "Ensar"
            };
            Employee hamza = new Employee
            {
                Name = "Hamza"
            };

            muharrem.AddSubordinate(alihan);
            muharrem.AddSubordinate(ensar);
            ensar.AddSubordinate(hamza);

            Contractor ali = new Contractor{Name = "Ali"};
            ensar.AddSubordinate(ali);

            Console.WriteLine(muharrem.Name);
            foreach (Employee manager in muharrem)
            {
                Console.WriteLine("--{0}", manager.Name);
                foreach (IPerson person in manager)
                {
                    Console.WriteLine("----{0}", person.Name);
                }
            }

            Console.ReadLine();
        }
    }

    interface IPerson
    {
        string Name { get; set; }
    }

    class Contractor:IPerson
    {
        public string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson>
    {
        List<IPerson> _subordinates = new List<IPerson>();

        public void AddSubordinate(IPerson person)
        {
            _subordinates.Add(person);
        }

        public void RemoveSubordinate(IPerson person)
        {
            _subordinates.Remove(person);
        }

        public IPerson GetSubordinate(int index)
        {
            return _subordinates[index];
        }

        public string Name { get; set; }
        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in _subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}

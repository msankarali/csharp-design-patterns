using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Save();
            Console.ReadLine();
        }
    }

    class Logging:ILogging
    {
        public void Log()
        {
            Console.WriteLine("Logged");
        }
    }

    internal interface ILogging
    {
        void Log();
    }

    class Caching:ICaching
    {
        public void Cache()
        {
            Console.WriteLine("Cached");
        }
    }

    internal interface ICaching
    {
        void Cache();
    }

    class Authorization : IAuthorization
    {
        public void CheckUser()
        {
            Console.WriteLine("User checked");
        }
    }

    internal interface IAuthorization
    {
        void CheckUser();
    }

    class Validation: IValidation
    {
        public void Validate()
        {
            Console.WriteLine("Validated");
        }
    }

    internal interface IValidation
    {
        void Validate();
    }

    class CustomerManager
    {
        private CrossCuttingConcernsFacade _concerns;
        public CustomerManager()
        {
            _concerns = new CrossCuttingConcernsFacade();
        }

        public void Save()
        {
            _concerns.Caching.Cache();
            _concerns.Authorization.CheckUser();
            _concerns.Logging.Log();
            _concerns.Validation.Validate();
            Console.WriteLine("Saved");
        }
    }

    class CrossCuttingConcernsFacade
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorization Authorization;
        public IValidation Validation;

        public CrossCuttingConcernsFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorization = new Authorization();
            Validation = new Validation();
        }
    }
}

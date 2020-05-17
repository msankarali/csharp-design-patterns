using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            ModifiedState modifiedState = new ModifiedState();
            modifiedState.DoAction(context);
            DeletedState deletedState = new DeletedState();
            deletedState.DoAction(context);

            Console.WriteLine(context.GetState().ToString());

            Console.ReadLine();
        }
    }

    interface IState
    {
        void DoAction(Context context);
    }

    class ModifiedState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: modified");
            context.SetState(this);
        }

        public override string ToString()
        {
            return "modified";
        }
    }

    class DeletedState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: deleted");
            context.SetState(this);
        }
        public override string ToString()
        {
            return "deleted";
        }
    }

    class AddedState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: added");
            context.SetState(this);
        }
        public override string ToString()
        {
            return "added";
        }
    }

    class Context
    {
        private IState _state;

        public void SetState(IState state)
        {
            _state = state;
        }

        public IState GetState()
        {
            return _state;
        }
    }
}

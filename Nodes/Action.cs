using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public abstract class Action<T>
    {

        protected readonly T actionTaker;
        public abstract void Run();

        public Action(T actionTaker)
        {
            this.actionTaker = actionTaker;
        }
    }
}

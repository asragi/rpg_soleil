using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    public interface IAggregate
    {
        IIterator Iterator();
    }
    public interface IIterator
    {
        bool HasNext();
        Object Next();
    }
}

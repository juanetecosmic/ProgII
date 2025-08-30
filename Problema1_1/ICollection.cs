using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_1
{
    public interface ICollection
    {
        bool IsEmpty();
        object Extract();
        object First();
        bool Add(object obj);
    }
}

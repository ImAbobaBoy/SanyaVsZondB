using SanyaVsZondB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.View
{
    public interface IObserver
    {
        void Update(IObservable observable);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public interface IMenuItem
    {
        string Title { get; }
        IList<IMenuItem> SubItems { get; }
        bool IsLeaf { get; }
        void Execute();
    }

    public interface IMenuAction
    {
        void Execute();
    }
}

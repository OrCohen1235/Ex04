using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{

    public class MenuItem : IMenuItem
    {
        private readonly string m_Title;
        private readonly IList<IMenuItem> m_SubItems;
        private readonly IMenuAction m_Action;

        public MenuItem(string i_Title, IList<IMenuItem> i_SubItems)
        {
            m_Title = i_Title ?? throw new ArgumentNullException(nameof(i_Title));
            m_SubItems = i_SubItems ?? new List<IMenuItem>();
            m_Action = null;
        }

        public MenuItem(string i_Title, IMenuAction i_Action)
        {
            m_Title = i_Title ?? throw new ArgumentNullException(nameof(i_Title));
            m_Action = i_Action ?? throw new ArgumentNullException(nameof(i_Action));
            m_SubItems = new List<IMenuItem>();
        }

        public string Title
        {
            get { return m_Title; }
        }

        public IList<IMenuItem> SubItems
        {
            get { return m_SubItems; }
        }

        public bool IsLeaf
        {
            get { return m_SubItems == null || m_SubItems.Count == 0; }
        }

        public void Execute()
        {
            if (IsLeaf)
            {
                m_Action.Execute();
            }
        }
    }
}
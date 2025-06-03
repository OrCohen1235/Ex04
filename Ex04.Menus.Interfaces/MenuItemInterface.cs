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
            if (i_Title == null)
            {
                throw new ArgumentNullException(nameof(i_Title), "Title cannot be null.");
            }
            if (i_SubItems == null)
            {
                throw new ArgumentNullException(nameof(i_SubItems), "SubItems cannot be null.");
            }

            m_Title = i_Title;
            m_SubItems = i_SubItems;
            m_Action = null;
        }

        public MenuItem(string i_Title, IMenuAction i_Action)
        {
            if (i_Title == null)
            {
                throw new ArgumentNullException(nameof(i_Title), "Title cannot be null.");
            }
            if (i_Action == null)
            {
                throw new ArgumentNullException(nameof(i_Action), "Action cannot be null.");
            }

            m_Title = i_Title;
            m_Action = i_Action;
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
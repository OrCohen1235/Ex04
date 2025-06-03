using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Events
{
    public class MenuItemEvents
    {
        private readonly string m_Title;
        private readonly IList<MenuItemEvents> m_SubItems;
        private readonly Action m_ActionToExecute;

        public MenuItemEvents(string i_Title, IList<MenuItemEvents> i_SubItems)
        {
            m_Title = i_Title ?? throw new ArgumentNullException(nameof(i_Title));
            m_SubItems = i_SubItems ?? new List<MenuItemEvents>();
            m_ActionToExecute = null;
        }

        public MenuItemEvents(string i_Title, Action i_ActionToExecute)
        {
            m_Title = i_Title ?? throw new ArgumentNullException(nameof(i_Title));
            m_ActionToExecute = i_ActionToExecute ?? throw new ArgumentNullException(nameof(i_ActionToExecute));
            m_SubItems = new List<MenuItemEvents>();
        }

        public string Title
        {
            get { return m_Title; }
        }

        public IList<MenuItemEvents> SubItems
        {
            get { return m_SubItems; }
        }

        public bool IsLeaf
        {
            get { return m_SubItems == null || m_SubItems.Count == 0; }
        }

        public void InvokeAction()
        {
            if (IsLeaf)
            {
                m_ActionToExecute.Invoke();
            }
        }
    }


}
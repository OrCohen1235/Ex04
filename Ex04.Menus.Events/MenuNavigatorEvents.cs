using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Events
{
    public class MenuNavigatorEvents
    {
        private readonly MenuItemEvents r_Root;
        private MenuItemEvents m_CurrentMenu;
        private readonly Stack<MenuItemEvents> m_MenuStack;

        public MenuNavigatorEvents(MenuItemEvents i_RootMenu)
        {
            if (i_RootMenu == null)
            {
                throw new ArgumentNullException(nameof(i_RootMenu), "Root menu cannot be null.");
            }

            r_Root = i_RootMenu;
            m_CurrentMenu = r_Root;
            m_MenuStack = new Stack<MenuItemEvents>();
        }

        public void Show()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                showMenuHeader(m_CurrentMenu);

                IList<MenuItemEvents> subItems = m_CurrentMenu.SubItems;
                for (int i = 0; i < subItems.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, subItems[i].Title);
                }

                if (m_CurrentMenu == r_Root)
                {
                    Console.WriteLine("0. Exit");
                }
                else
                {
                    Console.WriteLine("0. Back");
                }

                Console.Write("Please enter your choice (");
                if (subItems.Count > 0)
                {
                    Console.Write("1-{0}", subItems.Count);
                }

                Console.Write(" or 0 to ");
                Console.Write(m_CurrentMenu == r_Root ? "exit" : "go back");
                Console.Write("): ");

                string userInput = Console.ReadLine();
                bool parseSuccess = int.TryParse(userInput, out int choice);

                if (!parseSuccess || choice < 0 || choice > subItems.Count)
                {
                    Console.WriteLine("Invalid choice. Press Enter to try again...");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0)
                {
                    if (m_CurrentMenu == r_Root)
                    {
                        exitRequested = true;
                        continue;
                    }

                    m_CurrentMenu = m_MenuStack.Pop();
                    continue;
                }

                MenuItemEvents selectedItem = subItems[choice - 1];
                if (selectedItem.IsLeaf)
                {
                    Console.Clear();
                    selectedItem.InvokeAction();
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to return to \"{0}\" menu...", m_CurrentMenu.Title);
                    Console.ReadLine();
                }
                else
                {
                    m_MenuStack.Push(m_CurrentMenu);
                    m_CurrentMenu = selectedItem;
                }
            }
        }

        private void showMenuHeader(MenuItemEvents i_Menu)
        {
            Console.WriteLine("** {0} **", i_Menu.Title);
            Console.WriteLine(new string('-', i_Menu.Title.Length + 6));
        }
    }
}
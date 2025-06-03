using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuNavigator
    {
        private readonly IMenuItem r_Root;
        private IMenuItem m_CurrentMenu;
        private readonly Stack<IMenuItem> m_MenuStack;

        public MenuNavigator(IMenuItem i_RootMenu)
        {
            if (i_RootMenu == null)
            {
                throw new ArgumentNullException(nameof(i_RootMenu), "Root menu cannot be null.");
            }

            r_Root = i_RootMenu;
            m_CurrentMenu = r_Root;
            m_MenuStack = new Stack<IMenuItem>();
        }

        public void Show()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                showMenuHeader(m_CurrentMenu);

                IList<IMenuItem> subItems = m_CurrentMenu.SubItems;
                for (int i = 0; i < subItems.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, subItems[i].Title);
                }

                Console.WriteLine(m_CurrentMenu == r_Root ? "0. Exit" : "0. Back");
                Console.Write("Please enter your choice (1-{0} or 0): ", subItems.Count);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 0 || choice > subItems.Count)
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
                    }
                    else
                    {
                        m_CurrentMenu = m_MenuStack.Pop();
                    }
                }
                else
                {
                    IMenuItem selectedItem = subItems[choice - 1];
                    if (selectedItem.IsLeaf)
                    {
                        Console.Clear();
                        selectedItem.Execute();
                        Console.WriteLine("\nPress Enter to return...");
                        Console.ReadLine();
                    }
                    else
                    {
                        m_MenuStack.Push(m_CurrentMenu);
                        m_CurrentMenu = selectedItem;
                    }
                }
            }
        }

        private void showMenuHeader(IMenuItem i_Menu)
        {
            Console.WriteLine("** {0} **", i_Menu.Title);
            Console.WriteLine(new string('-', i_Menu.Title.Length + 6));
        }
    }
}
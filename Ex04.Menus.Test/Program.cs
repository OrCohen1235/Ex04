using System;
using System.Collections.Generic;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Events;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        private static void Main()
        {
            // Show the interface-based menu first
            Ex04.Menus.Interfaces.IMenuItem interfaceRootMenu = BuildInterfaceMenu();
            Console.WriteLine("** Interfaces Main Menu **\n");
            Ex04.Menus.Interfaces.MenuNavigator interfaceNavigator = new Ex04.Menus.Interfaces.MenuNavigator(interfaceRootMenu);
            interfaceNavigator.Show();

            // Then show the delegate-based menu
            MenuItemEvents delegateRootMenu = BuildDelegateMenu();
            Console.Clear();
            Console.WriteLine("** Delegates Main Menu **\n");
            Ex04.Menus.Events.MenuNavigatorEvents delegateNavigator = new Ex04.Menus.Events.MenuNavigatorEvents(delegateRootMenu);
            delegateNavigator.Show();
        }

        private static IMenuItem BuildInterfaceMenu()
        {
            IMenuItem showVersion = new MenuItem("Show Version", new ShowVersionAction());
            IMenuItem countLowercase = new MenuItem("Count Lowercase Letters", new CountLowercaseAction());

            IMenuItem lettersAndVersion = new MenuItem("Letters and Version", new List<IMenuItem>
            {
                showVersion,
                countLowercase
            });

            IMenuItem showDate = new MenuItem("Show Current Date", new ShowDateAction());
            IMenuItem showTime = new MenuItem("Show Current Time", new ShowTimeAction());

            IMenuItem dateTime = new MenuItem("Show Current Date/Time", new List<IMenuItem>
            {
                showDate,
                showTime
            });

            IMenuItem rootMenu = new MenuItem("Main Menu", new List<IMenuItem>
            {
                lettersAndVersion,
                dateTime
            });

            return rootMenu;
        }

        private static MenuItemEvents BuildDelegateMenu()
        {
            MenuItemEvents showVersion = new MenuItemEvents("Show Version", new Action(showVersionDelegate));
            MenuItemEvents countLowercase = new MenuItemEvents("Count Lowercase Letters", new Action(countLowercaseDelegate));

            MenuItemEvents lettersAndVersion = new MenuItemEvents("Letters and Version", new List<MenuItemEvents>
            {
                showVersion,
                countLowercase
            });

            MenuItemEvents showDate = new MenuItemEvents("Show Current Date", new Action(showDateDelegate));
            MenuItemEvents showTime = new MenuItemEvents("Show Current Time", new Action(showTimeDelegate));

            MenuItemEvents dateTime = new MenuItemEvents("Show Current Date/Time", new List<MenuItemEvents>
            {
                showDate,
                showTime
            });

            MenuItemEvents rootMenu = new MenuItemEvents("Delegates Main Menu", new List<MenuItemEvents>
            {
                lettersAndVersion,
                dateTime
            });

            return rootMenu;
        }

        #region Interface Actions

        private class ShowVersionAction : IMenuAction
        {
            public void Execute()
            {
                Console.WriteLine("App Version: 25.2.4.4480");
            }
        }

        private class CountLowercaseAction : IMenuAction
        {
            public void Execute()
            {
                Console.Write("Please enter a sentence: ");
                string sentence = Console.ReadLine();
                int lowercaseCount = 0;

                foreach (char c in sentence)
                {
                    if (char.IsLower(c))
                    {
                        lowercaseCount++;
                    }
                }

                Console.WriteLine("There are {0} lowercase letters in your text.", lowercaseCount);
            }
        }

        private class ShowDateAction : IMenuAction
        {
            public void Execute()
            {
                Console.WriteLine("Current Date is {0}", DateTime.Now.ToString("d"));
            }
        }

        private class ShowTimeAction : IMenuAction
        {
            public void Execute()
            {
                Console.WriteLine("Current Time is {0}", DateTime.Now.ToString("T"));
            }
        }

        #endregion

        #region Delegate Actions

        private static void showVersionDelegate()
        {
            Console.WriteLine("App Version: 25.2.4.4480");
        }

        private static void countLowercaseDelegate()
        {
            Console.Write("Please enter a sentence: ");
            string sentence = Console.ReadLine();
            int lowercaseCount = 0;

            foreach (char c in sentence)
            {
                if (char.IsLower(c))
                {
                    lowercaseCount++;
                }
            }

            Console.WriteLine("There are {0} lowercase letters in your text.", lowercaseCount);
        }

        private static void showDateDelegate()
        {
            Console.WriteLine("Current Date is {0}", DateTime.Now.ToString("d"));
        }

        private static void showTimeDelegate()
        {
            Console.WriteLine("Current Time is {0}", DateTime.Now.ToString("T"));
        }

        #endregion
    }
}

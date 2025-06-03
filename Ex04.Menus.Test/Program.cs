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
            IMenuItem showVersionItem = new MenuItem("Show Version", new ShowVersionAction());
            IMenuItem countLowercaseItem = new MenuItem("Count Lowercase Letters", new CountLowercaseAction());

            IMenuItem lettersAndVersionItem = new MenuItem("Letters and Version", new List<IMenuItem>
            {
                showVersionItem,
                countLowercaseItem
            });

            IMenuItem showDateItem = new MenuItem("Show Current Date", new ShowDateAction());
            IMenuItem showTimeItem = new MenuItem("Show Current Time", new ShowTimeAction());

            IMenuItem dateTimeItem = new MenuItem("Show Current Date/Time", new List<IMenuItem>
            {
                showDateItem,
                showTimeItem
            });

            IMenuItem rootMenuItem = new MenuItem("Main Menu", new List<IMenuItem>
            {
                lettersAndVersionItem,
                dateTimeItem
            });

            return rootMenuItem;
        }

        private static MenuItemEvents BuildDelegateMenu()
        {
            MenuItemEvents showVersionItem = new MenuItemEvents("Show Version", new Action(showVersion));
            MenuItemEvents countLowercaseItem = new MenuItemEvents("Count Lowercase Letters", new Action(countLowercase));

            MenuItemEvents lettersAndVersionItem = new MenuItemEvents("Letters and Version", new List<MenuItemEvents>
            {
                showVersionItem,
                countLowercaseItem
            });

            MenuItemEvents showDateItem = new MenuItemEvents("Show Current Date", new Action(showDate));
            MenuItemEvents showTimeItem = new MenuItemEvents("Show Current Time", new Action(showTime));

            MenuItemEvents dateTimeItem = new MenuItemEvents("Show Current Date/Time", new List<MenuItemEvents>
            {
                showDateItem,
                showTimeItem
            });

            MenuItemEvents rootMenuItem = new MenuItemEvents("Delegates Main Menu", new List<MenuItemEvents>
            {
                lettersAndVersionItem,
                dateTimeItem
            });

            return rootMenuItem;
        }

        private class ShowVersionAction : IMenuAction
        {
            public void Execute()
            {
                showVersion();
            }
        }

        private class CountLowercaseAction : IMenuAction
        {
            public void Execute()
            {
               countLowercase();
            }
        }

        private class ShowDateAction : IMenuAction
        {
            public void Execute()
            {
                showDate();
            }
        }

        private class ShowTimeAction : IMenuAction
        {
            public void Execute()
            {
                showTime();
            }
        }

        private static void showVersion()
        {
            Console.WriteLine("App Version: 25.2.4.4480");
        }

        private static void countLowercase()
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

        private static void showDate()
        {
            Console.WriteLine("Current Date is {0}", DateTime.Now.ToString("d"));
        }

        private static void showTime()
        {
            Console.WriteLine("Current Time is {0}", DateTime.Now.ToString("T"));
        }
    }
}
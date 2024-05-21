using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nInventory Management System Menu:");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. View Items");
                Console.WriteLine("3. Find Item by ID");
                Console.WriteLine("4. Update Item");
                Console.WriteLine("5. Delete Item");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Item.addItem();
                        break;
                    case 2:
                        Item.getAllItems();
                        break;
                    case 3:
                        Console.Write("Enter Item ID to search: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Item.findItemById(id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please enter a valid number.");
                        }
                        break;
                    case 4:
                        Item.updateItem();
                        break;
                    case 5:
                        Item.deleteItem();
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
        }
    }

}


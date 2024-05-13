using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        static List<string> list = new List<string>();

        public static void Main(string[] args)
        {
            bool exit = false;
            int choice;

            do
            {
                Console.WriteLine("\nTask List Menu:");
                Console.WriteLine("1. Create Task");
                Console.WriteLine("2. Read Tasks");
                Console.WriteLine("3. Update Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                //converts string input to int
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number."); //checks for valid choice number
                    continue; // Skip the rest of the loop iteration and start over
                }

                switch (choice)
                {
                    case 1:

                        //Create task
                        Console.WriteLine();
                        Console.Write("Enter the task: ");
                        string task = Console.ReadLine();
                        list.Add(task);
                        Console.WriteLine("Task added successfully.");
                        break;

                    case 2:

                        //Read all tasks
                        Console.WriteLine();
                        if (list.Count == 0) //checks whether task list is empty or not
                        {
                            Console.WriteLine("Task list is empty.");
                        }
                        else
                        {
                            Console.WriteLine("Task List:");
                            for (int i = 0; i < list.Count; i++) //iterates and prints tasks from the list
                            {
                                Console.WriteLine($"{i + 1}. {list[i]}");
                            }
                        }
                        break;

                    case 3:

                        //Update task
                        Console.WriteLine();
                        Console.WriteLine("Task List: ");
                        
                        for (int i = 0; i < list.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {list[i]}");
                        }

                        if (list.Count == 0)
                        {
                            return;
                        }

                        Console.Write("Enter the task number to update: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= list.Count)
                        {
                            Console.Write("Enter the updated task: ");
                            string updatedTask = Console.ReadLine();
                            list[index - 1] = updatedTask;
                            Console.WriteLine("Task updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid task number.");
                        }
                        break;

                    case 4:

                        //Delete Task
                        if (list.Count == 0)
                        {
                            return;
                        }

                        Console.WriteLine();
                        Console.Write("Enter the task number to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int indx) && indx >= 1 && indx <= list.Count) 
                        {
                            list.RemoveAt(indx - 1);
                            Console.WriteLine("Task deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid task number.");
                        }
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:

                        Console.WriteLine();
                        Console.WriteLine("Enter a valid choice from 1-5");
                        break;
                }

            } while (!exit);
        }


       
    }
}

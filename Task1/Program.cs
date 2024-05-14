using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        static List<string> tasks = new List<string>();

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
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                // Read the user's input as a string & convert it to an integer and assign it to a variable 'choice'
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue; // Skip the rest of the loop iteration and start over
                }

                switch (choice)
                {
                    case 1:
                        //Create task
                        CreateTask();
                        break;

                    case 2:
                        //Read all tasks
                        ReadTasks();
                        break;

                    case 3:
                        //Update task
                        UpdateTask();
                        break;

                    case 4:
                        //Delete Task
                        DeleteTask();
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }

            } while (!exit);
        }

        public static void CreateTask()
        {
            Console.WriteLine();
            Console.Write("Enter the task: ");
            string task = Console.ReadLine();
            tasks.Add(task);
            Console.WriteLine("Task added successfully.");
        }

        public static void ReadTasks()
        {
            if (tasks.Count == 0) //checks whether task list is empty or not
                {
                Console.WriteLine();
                Console.WriteLine("Task list is empty.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Task List:");
                for (int i = 0; i < tasks.Count; i++) //iterates and prints tasks from the list
                    {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
        }

        public static void UpdateTask()
        {
            ReadTasks();
            if (tasks.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Enter the task number to update: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
            {
                Console.WriteLine();
                Console.Write("Enter the updated task: ");
                string updatedTask = Console.ReadLine();
                tasks[index - 1] = updatedTask;
                Console.WriteLine("Task updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }

        public static void DeleteTask()
        {
            ReadTasks();
            if (tasks.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Enter the task number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= tasks.Count)
            {
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }
    }
}

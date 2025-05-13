using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListSav2File
{
    internal class ToDoService
    {
       
        private static string folderPath = "C:\\TestFolder\\example.csv";
        private static int taskCounter = 1;
       
        public static void DisplayMenu()
        {
            Console.WriteLine("\n--To-Do List Menu----");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark as completed");
            Console.WriteLine("4. Delete a Task");
            Console.WriteLine("5. Exit App...");
            Console.WriteLine("Enter an option:");
            Console.Beep();
        }

        public static void AddTask()
        {
            //creates directory

            string folderPath = "C:\\TestFolder\\example.csv";

            string directory = Path.GetDirectoryName(folderPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Folder created at: {directory}");
            }
            else
            {
                Console.WriteLine($"Folder already exists at: {directory}");
            }

               Console.Write("Enter Title:");
               string title = Console.ReadLine();

            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();

            ToDoTask newTask = new ToDoTask
            {
                Id = taskCounter++,
                Title = title,
                Description = description,
                IsCompleted = "Not completed",
            };
            //logic to write to file
            using (StreamWriter writer = new StreamWriter(folderPath, append: true))
            {
                //writer.WriteLine("ID", "Title", "Description", "Status");

                writer.WriteLine($"{newTask.Id},{newTask.Title},{newTask.Description},{newTask.IsCompleted}");
            }
            Console.WriteLine("Task added and saved to file successfully.");
           
        }

        public static void ViewTasks()
        {
            if (!File.Exists("C:\\TestFolder\\example.csv"))
            {
                Console.WriteLine("No tasks found.");
                return;
            }
            //Stream reader
            using (StreamReader reader = new StreamReader("C:\\TestFolder\\example.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //string status = task.IsCompleted ? "Completed" : "Pending";

                    string[] task = line.Split(',');
                    Console.WriteLine($"ID: {task[0]}, Title: {task[1]}, Description: {task[2]}, Completed: {task[3]}");
                }

                Console.WriteLine("\n--To-Do List--");
            }
        }

        public static void MarkTaskAsCompleted()
        {
            Console.Write("Enter the ID of the task to mark as completed: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                // Check if the file exists
                if (!File.Exists("C:\\TestFolder\\example.csv"))
                {
                    Console.WriteLine("No tasks found.");
                    return;
                }
                var tasks = new List<string>();
                bool taskFound = false;

                using (StreamReader reader = new StreamReader("C:\\TestFolder\\example.csv"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] taskParts = line.Split(',');
                        if (int.Parse(taskParts[0]) == taskId)
                        {
                            // Update the task as completed
                            taskParts[3] = "Completed";
                            taskFound = true;
                            Console.WriteLine($"Task ID {taskId} marked as completed.");
                        }
                        tasks.Add(string.Join(",", taskParts));
                    }
                }
                if (!taskFound)
                {
                    Console.WriteLine($"Task ID {taskId} not found.");
                    return;
                }

                // Write the updated tasks back to the file
                using (StreamWriter writer = new StreamWriter("C:\\TestFolder\\example.csv", false))

                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine(task);
                    }
                }

                Console.WriteLine("Tasks updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
            }
        }

        public static void DeleteTask()
        {
                Console.Write("Enter Task ID to delete: ");
                int taskId = int.Parse(Console.ReadLine());
                // File path for tasks
                string filePath = "C:\\TestFolder\\example.csv";

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Task file not found.");
                    return;
                }

                // List to store remaining tasks
                var tasks = new List<string>();
                bool taskFound = false;

                // Read tasks from the file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] taskParts = line.Split(',');
                        if (int.Parse(taskParts[0]) == taskId)
                        {
                            taskFound = true;
                            Console.WriteLine($"Task ID {taskId} has been deleted.");
                            continue; // Skip adding this task to the updated list
                        }
                        tasks.Add(line); // Add all other tasks to the list
                    }
                }

                // If no matching task is found
                if (!taskFound)
                {
                    Console.WriteLine($"Task with ID {taskId} was not found.");
                    return;
                }

                // Write the remaining tasks back to the file
                using (StreamWriter writer = new StreamWriter(filePath, false)) // Overwrite the file
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine(task);
                    }
                }

                Console.WriteLine("Task list updated successfully.");
            }
        }
    }


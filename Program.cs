using System.Security.Cryptography.X509Certificates;
using ToDoListSav2File;

namespace ToDoListModified
{
    internal class Program
    {


        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    ToDoService.DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ToDoService.AddTask();
                            break;
                        case "2":
                            ToDoService.ViewTasks();
                            break;
                        case "3":
                            ToDoService.MarkTaskAsCompleted();
                            break;
                        case "4":
                            ToDoService.DeleteTask();
                            break;
                        case "5":
                            Console.WriteLine("Exiting the application...");
                            return;
                        default:
                            throw new ArgumentOutOfRangeException((choice));

                            //Console.WriteLine("Invalid choice, please select a valid option.");
                            //   break;


                    }

                    ToDoService service = new ToDoService();
                    ToDoService.DisplayMenu();
                    ToDoService.AddTask();
                    ToDoService.ViewTasks();
                    ToDoService.MarkTaskAsCompleted();
                    ToDoService.DeleteTask();

                }
                catch (ArgumentOutOfRangeException)

                {
                    Console.WriteLine("Error:...Invalid Number entered");
                    Console.WriteLine("pls enter a valid number from the option");
                }
            }

        }
    }


}








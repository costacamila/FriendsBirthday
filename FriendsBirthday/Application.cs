using System;
using System.Linq;
using FriendsBirthday.Model;
using FriendsBirthday.Repository;
using FriendsBirthday.Calculator;
using System.Threading;

namespace FriendsBirthday
{
    public class Application
    {
        public static void Start()
        {
            var repository = new RepositoryDb();

            if (Today.Calculate().Count > 0)
            {
                Console.WriteLine("Today's birthdays:");
                foreach (var n in Today.Calculate())
                {
                    Console.WriteLine($"Name: {n.Name} {n.Surname}");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Which birthday action do you wanna make?");
            Console.WriteLine("1 - Search");
            Console.WriteLine("2 - Insert");
            Console.WriteLine("3 - Edit");
            Console.WriteLine("4 - Delete");
            Console.WriteLine("5 - Exit");
            Console.Write("\nAction: ");

            Archive.CloseTextFile();
            char operacao = Console.ReadLine().Trim().ToCharArray()[0];
            switch (operacao)
            {
                case '1':
                    Archive.ReadFile();
                    Search();
                    break;
                case '2':
                    Archive.ReadFile();
                    Insert();
                    break;
                case '3':
                    Archive.ReadFile();
                    Edit();
                    break;
                case '4':
                    Archive.ReadFile();
                    Delete();
                    break;
                case '5':
                    Archive.ReadFile();
                    Console.WriteLine("\nExit.");
                    Archive.CloseTextFile();
                    break;
                default:
                    Clean();
                    Header();
                    Console.WriteLine("\nInvalid option.\n");
                    Start();
                    break;
            }

            static void Header()
            {
                Console.WriteLine("\n* Friends' Birthday *\n");
            }

            static void Clean()
            {
                Console.Clear();
            }

            void Search()
            {
                Clean();
                Header();
                Console.WriteLine("Which friend do you wanna search?");
                Console.Write("\nName: ");
                string name = Console.ReadLine();
                Console.WriteLine("");
                if (repository.Search(name).Count() > 0)
                {
                    var searchedFriends = repository.Search(name);
                    int i = 0;
                    foreach (var n in searchedFriends)
                    {
                        Console.WriteLine($"{i} - Name: {n.Name} {n.Surname}");
                        i++;
                    }
                    Console.Write("\nChoose the friend you wanna see: ");
                    int id = Int32.Parse(Console.ReadLine());
                    if (id < searchedFriends.Count)
                    {
                        var f = searchedFriends[id];
                        Console.WriteLine($"\n<Searched friend>\nName: {f.Name} {f.Surname} | Birthday: {f.Birthday.ToShortDateString()}");
                        Console.WriteLine(RemainingDays.Calculate(f) + " remaining days to next birthday.\n");
                        Console.WriteLine("\nPress any key to return...");
                        Console.ReadKey();
                        Clean();
                        Header();
                        Start();
                    }
                    else
                    {
                        Clean();
                        Header();
                        Console.WriteLine("\nInvalid id.\n");
                        Search();
                    }
                }
                else
                {
                    Clean();
                    Header();
                    Console.WriteLine("There is no result to this search.\n");
                    Start();
                }
            }

            void Insert()
            {
                Clean();
                Header();
                Console.WriteLine("Who do you wanna insert?\n");
                Console.Write("Name: ");
                string name = Console.ReadLine().Trim();
                Console.Write("Surname: ");
                string surname = Console.ReadLine().Trim();
                Console.Write("Birthday (dd/MM/yyyy): ");
                var birthday = DateTime.Parse(Console.ReadLine());
                var newFriend = new Friend(name, surname, birthday);
                Console.WriteLine("");
                Console.WriteLine(repository.Save(newFriend));
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                Clean();
                Header();
                Start();
            }

            void Edit()
            {
                Clean();
                Header();
                Console.WriteLine("Which friend do you wanna edit?\n");
                int i = 0;
                foreach (var n in RepositoryDb.friends)
                {
                    Console.WriteLine($"{i} - Name: {n.Name} {n.Surname} | Birthday: {n.Birthday.ToShortDateString()}");
                    i++;
                }
                Console.Write("\nChoose the friend you wanna edit: ");
                int id = Int32.Parse(Console.ReadLine());
                if (id < RepositoryDb.friends.Count)
                {
                    var f = RepositoryDb.friends[id];
                    Console.WriteLine($"\n<Edited friend>\nName: {f.Name} {f.Surname} | Birthday: {f.Birthday.ToShortDateString()}");
                    Console.WriteLine("\nType the new data.\n");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Surname: ");
                    string surname = Console.ReadLine();
                    Console.Write("Birthday (dd/MM/yyyy): ");
                    var birthday = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("");
                    Console.WriteLine(repository.Edit(id, name, surname, birthday));
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    Clean();
                    Header();
                    Start();
                }
                else
                {
                    Clean();
                    Header();
                    Console.WriteLine("\nInvalid id.\n");
                    Edit();
                }
            }

            void Delete()
            {
                Clean();
                Header();
                Console.WriteLine("Which friend do you wanna delete?\n");
                int i = 0;
                foreach (var n in RepositoryDb.friends)
                {
                    Console.WriteLine($"{i} - Name: {n.Name} {n.Surname} | Birthday: {n.Birthday.ToShortDateString()}");
                    i++;
                }
                Console.Write("\nChoose the friend you wanna delete: ");
                int id = Int32.Parse(Console.ReadLine());
                if (id < RepositoryDb.friends.Count)
                {
                    var f = RepositoryDb.friends[id];
                    Console.WriteLine($"\n<Deleted friend>\nName: {f.Name} {f.Surname} | Birthday: {f.Birthday.ToShortDateString()}");
                    Console.WriteLine(repository.Delete(id));
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    Clean();
                    Header();
                    Start();
                }
                else
                {
                    Clean();
                    Header();
                    Console.WriteLine("\nInvalid id.\n");
                    Delete();
                }
            }
        }
    }
}

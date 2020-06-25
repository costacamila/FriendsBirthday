using FriendsBirthday.Repository;
using System;

namespace FriendsBirthday
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n* Friends' Birthday *\n");
            Archive.ReadFile();
            Application.Start();
        }
    }
}


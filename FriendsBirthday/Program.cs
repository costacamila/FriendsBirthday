using FriendsBirthday.Repository;
using System;

namespace FriendsBirthday
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("* Friend's Birthday *\n");
            Archive.ReadFile();
            Application.Start();
        }
    }
}


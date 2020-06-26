using System;
using System.Collections.Generic;
using System.Linq;
using FriendsBirthday.Model;

namespace FriendsBirthday.Repository
{
    public class RepositoryDb : IRepository
    {
        public static List<Friend> friends = new List<Friend>();

        public string Save(Friend friend)
        {
            if (!Check(friend).Any())
            {
                friends.Add(friend);
                return "Friend successfully saved!";
            }
            return "This friend already exists.";
        }

        public List<Friend> Search(string name)
        {
            return friends.Where(f => f.fullName().Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public string Edit(int id, string name, string surname, DateTime birthday)
        {
            if (!Check(new Friend(name, surname, birthday)).Any())
            {
                friends[id].Name = name;
                friends[id].Surname = surname;
                friends[id].Birthday = birthday;
                return "Friend successfully edited!";
            }
            return "This friend already exists.";
        }

        public string Delete(int id)
        {
            friends.RemoveAt(id);
            return "Friend successfully deleted!";
        }

        public IEnumerable<Friend> Check(Friend friend)
        {
            return from x in friends
                   where x.Name.Contains(friend.Name) && x.Surname.Contains(friend.Surname) && x.Birthday.Equals(friend.Birthday)
                   select x;
        }
    }
}
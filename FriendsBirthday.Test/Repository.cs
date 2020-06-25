using System;
using System.Collections.Generic;
using Xunit;
using FriendsBirthday.Model;
using System.Linq;

namespace FriendsBirthday.Test
{
    public class Repository
    {
        public List<Friend> friends = new List<Friend>();

        [Fact]
        public void Save()
        {
            var friend = new Friend("Name", "Surname", new DateTime(2020, 6, 25));
            Assert.Contains("successfully", saveFriend());

            string saveFriend()
            {
                if (!Check(friend).Any())
                {
                    friends.Add(friend);
                    return "Friend successfully saved!";
                }
                return "This friend already exists.";
            }
        }

        [Fact]
        public void Search()
        {
            var friend = new Friend("Name", "Surname", new DateTime(2020, 6, 25));
            friends.Add(friend);
            Assert.NotNull(SearchFriend(friend.Name));

            List<Friend> SearchFriend(string name)
            {
                return friends.Where(f => f.fullName().Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
        }

        [Fact]
        public void Edit()
        {
            var friend = new Friend("Name", "Surname", new DateTime(2020, 6, 25));
            friends.Add(friend);
            
            Assert.Contains("successfully", EditFriend(0,"NewName","NewSurname",new DateTime(2020,6,26)));

            string EditFriend(int id, string name, string surname, DateTime birthday)
            {
                friends[id].Name = name;
                friends[id].Surname = surname;
                friends[id].Birthday = birthday;
                return "Friend successfully edited!";
            }
        }

        [Fact]
        public void Delete() 
        {
            var friend = new Friend("Name", "Surname", new DateTime(2020, 6, 25));
            friends.Add(friend);
            Assert.Contains("successfully", DeleteFriend(0));
            string DeleteFriend(int id) 
            {
                friends.RemoveAt(id);
                return "Friend successfully deleted!";
            }
        }

        public IEnumerable<Friend> Check(Friend friend)
        {
            return from x in friends
                   where x.Name.Contains(friend.Name) && x.Surname.Contains(friend.Surname) && x.Birthday.Equals(friend.Birthday)
                   select x;
        }
    }
}

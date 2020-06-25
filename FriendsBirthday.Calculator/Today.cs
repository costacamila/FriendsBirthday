using System;
using System.Collections.Generic;
using System.Linq;
using FriendsBirthday.Model;
using FriendsBirthday.Repository;

namespace FriendsBirthday.Calculator
{
    public class Today
    {
        public static List<Friend> Calculate()
        {
            return RepositoryDb.friends.Where(f => f.Birthday.Day == DateTime.Now.Day && f.Birthday.Month == DateTime.Now.Month).ToList();
        }
    }
}

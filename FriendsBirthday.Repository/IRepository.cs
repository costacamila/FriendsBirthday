using System;
using System.Collections.Generic;
using FriendsBirthday.Model;

namespace FriendsBirthday.Repository
{
    public interface IRepository
    {
        string Save(Friend friend);
        List<Friend> Search(string name);
        string Edit(int id, string name, string surname, DateTime birthday);
        string Delete(int id);
    }
}

using System;

namespace FriendsBirthday.Model
{
    public class Friend
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

        public Friend(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
        }

        public string fullName()
        {
            return $"{Name} {Surname}";
        }
    }
}

using FriendsBirthday.Model;
using System;
using System.IO;

namespace FriendsBirthday.Repository
{
    public class Archive
    {
        public static string GetFileName()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\FriendsBirthday.txt";
        }

        public static void ReadFile()
        {
            string fileName = GetFileName();
            FileStream file;
            if (!File.Exists(fileName))
            {
                file = File.Create(fileName);
                file.Close();
            }
            string result = File.ReadAllText(fileName);
            string[] friends = result.Split(';');
            for (int i = 0; i < friends.Length - 1; i++)
            {
                string[] friendsData = friends[i].Split(',');
                string name = friendsData[0];
                string surname = friendsData[1];
                DateTime birthday = Convert.ToDateTime(friendsData[2]);
                Friend friend = new Friend(name, surname, birthday);
                RepositoryDb.friends.Add(friend);
            }
        }

        public static void CloseTextFile()
        {
            if (File.Exists(GetFileName()))
            {
                var input = String.Empty;
                foreach (var friend in RepositoryDb.friends)
                {
                    input += $"{friend.Name},{friend.Surname},{friend.Birthday.ToShortDateString()};";
                }
                File.WriteAllText(GetFileName(), String.Empty);
                File.WriteAllText(GetFileName(), input);
            }
            RepositoryDb.friends.Clear();
        }
    }
}

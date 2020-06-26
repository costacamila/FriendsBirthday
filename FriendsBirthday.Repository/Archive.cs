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

        public static bool ReadFile()
        {
            string fileName = GetFileName();
            FileStream file;
            if (!File.Exists(fileName))
            {
                try
                {
                    file = File.Create(fileName);
                    file.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
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
            return true;
        }

        public static bool CloseTextFile()
        {
            bool result = false;
            if (File.Exists(GetFileName()))
            {
                var input = String.Empty;
                foreach (var friend in RepositoryDb.friends)
                {
                    input += $"{friend.Name},{friend.Surname},{friend.Birthday.ToShortDateString()};";
                }
                File.WriteAllText(GetFileName(), String.Empty);
                File.WriteAllText(GetFileName(), input);
                result = true;
            }
            RepositoryDb.friends.Clear();
            return result;
        }
    }
}

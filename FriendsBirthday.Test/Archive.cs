using FriendsBirthday.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace FriendsBirthday.Test
{
    public class Archive
    {
        [Fact]
        public void GetFileName()
        {
            Assert.Contains("Test_FriendsBirthday.txt", FileName());
        }

        [Fact]
        public void OpenFile()
        {
            List<Friend> friendsList = new List<Friend>();
            Assert.True(ReadFile());
            bool ReadFile()
            {
                string fileName = FileName();
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
                    friendsList.Add(friend);
                }
                return true;
            }
        }

        [Fact]
        public void CloseFile()
        {
            List<Friend> friendsList = new List<Friend>();
            Friend[] objects = new Friend[] {
                new Friend("Name1","Surname1",new DateTime(2020,6,22)),
                new Friend("Name2","Surname2",new DateTime(2020,6,23)),
                new Friend("Name3","Surname3",new DateTime(2020,6,24)),
                new Friend("Name4","Surname4",new DateTime(2020,6,25)),
                new Friend("Name5","Surname5",new DateTime(2020,6,26))
            };
            for (var i = 0; i < objects.Length; i++)
            {
                friendsList.Add(objects[i]);
            }
            Assert.True(CloseTextFile());
            bool CloseTextFile()
            {
                bool result = false;
                if (File.Exists(FileName()))
                {
                    var input = String.Empty;
                    foreach (var friend in friendsList)
                    {
                        input += $"{friend.Name},{friend.Surname},{friend.Birthday.ToShortDateString()};";
                    }
                    File.WriteAllText(FileName(), String.Empty);
                    File.WriteAllText(FileName(), input);
                    result = true;
                }
                friendsList.Clear();
                return result;
            }
        }

        public static string FileName()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Test_FriendsBirthday.txt";
        }
    }
}

using System;
using FriendsBirthday.Model;

namespace FriendsBirthday.Calculator
{
    public class RemainingDays
    {
        public static int Calculate(Friend friend)
        {
            var nextBirthday = friend.Birthday.AddYears(DateTime.Today.Year - friend.Birthday.Year);
            if (nextBirthday < DateTime.Today)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }
            return (nextBirthday - DateTime.Today).Days;
        }
    }
}

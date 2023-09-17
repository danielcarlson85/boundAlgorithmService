using System;
using WorkoutData.Abstractions.Models;

namespace WorkoutData.Runtime
{
    public class User
    {
        public Guid Id = new Guid();

        public Device Device = new Device();

        public UserData UserData = new UserData();

        public override string ToString()
        {

            var test = base.ToString();

            return test;
        }

    }
}

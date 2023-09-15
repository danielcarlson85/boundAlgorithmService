using System;
using WorkoutData.Abstractions.Models;

namespace AlgorithmService.IoTHubFunctions.Entities
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

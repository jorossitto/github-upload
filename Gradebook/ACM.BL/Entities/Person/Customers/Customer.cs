﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Customer : Person, ILoggable, ICustomer
    {
        public List<Address> AddressList { get; set; }
        public int CustomerId { get; private set; }
        public int CustomerType { get; set; }

        public bool IsGold { get; set; }

        public Customer(): this(0) //chain calls constructors with the 0 customerId
        {
            
        }

        public decimal CalculatePercentOfGoalSteps(string goalSteps, string actualSteps)
        {
            decimal goalStepCount = 0;
            decimal actualStepCount = 0;

            if (string.IsNullOrWhiteSpace(goalSteps))
            {
                throw new ArgumentException("Goal must be entered", "goalSteps");
            }
            
            if (string.IsNullOrWhiteSpace(actualSteps))
            {
                throw new ArgumentException("Actual steps must be entered", "actualSteps");
            }


            if (!decimal.TryParse(goalSteps, out goalStepCount))
            {
                throw new ArgumentException("Goal must be numeric", "goalSteps");
            }
            
            if(!decimal.TryParse(actualSteps, out actualStepCount))
            {
                throw new ArgumentException("Actual steps be numeric", "actualSteps");
            }

            return CalculatePercentOfGoalSteps(goalStepCount, actualStepCount);
        }

        public decimal CalculatePercentOfGoalSteps(decimal goalStepCount, decimal actualStepCount)
        {
            if (goalStepCount <= 0)
            {
                throw new ArgumentException("Goal must be greater than 0", "goalStepCount");
            }

            return (actualStepCount / goalStepCount) * 100;
        }

        public Customer(int customerId)
        {
            CustomerId = customerId;
            AddressList = new List<Address>();
        }



        public string Log() => $"{CustomerId}: {FullName} Email: {EmailAddress} Status: {EntityState.ToString()}";
        


        public static Customer CreateDefaultCustomer()
        {
            return new Customer(1)
            {
                EmailAddress = "fbaggins@hobbiton.me",
                FirstName = "Frodo",
                LastName = "Baggins",
                AddressList = null
            };
        }

        internal void ValidateEmail()
        {
            throw new NotImplementedException();
        }
    }
}

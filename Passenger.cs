using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Security.AccessControl;

namespace SkyTourTicketCalculator
{
    // This class handles the input and output needed to calculate the ticket price for a
    // single passenger. Error checking is done to ensure erroneous input is not considered for calculation.
    // After successful processing of passenger information is done, an output is displayed for the passenger's
    // information.
    internal class Passenger
    {
        // Auto-implemented properties for each required piece of data for a single passenger
        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public DateTime Time { get; set; }


        // Gather passenger information
        public void CustomerInfo(Passenger customer, int i)
        {
            // Prompt the user for inforamtion on the pasenger at index i of the partySize array
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            // Prompt the user for the passenger's first name
            Console.WriteLine($"What is the first name of passenger {i + 1}?");
            customer.First = Console.ReadLine();
            // Prompt the user for the passenger's last name
            Console.WriteLine($"What is {First}'s last name?");
            customer.Last = Console.ReadLine();
            // Prompt the user for the passenger's age
            Console.WriteLine($"How old is {First}?");

            // Validate user's input for age
            int validateAge;
            while (!int.TryParse(Console.ReadLine(), out validateAge) || validateAge <= 1)
            {
                Console.WriteLine("Please enter a positive integer.\n" +
                        $"How old is {First}?");
            }
            customer.Age = validateAge;

            // Prompt the user for the passenger's weight in lbs
            Console.WriteLine($"What is {First}'s weight in pounds?");
            double validateWeight;
            while (!double.TryParse(Console.ReadLine(), out validateWeight) || validateWeight <= 1)
            {
                Console.WriteLine("Please enter a positive real number.\n" +
                        $"What is {First}'s weight in pounds?");
            }
            customer.Weight = validateWeight;
            Console.WriteLine("\n");
            // Sets the timestamp to the current time of data entry
            customer.Time = DateTime.Now;
        }


        // Expression-bodied method that converts passenger's weight from pounds to kilograms
        public double Kilo() => Math.Round((Weight / 2.205), 2);
        
        // Calculates the ticket cost for a single passenger based on weight in kg:
        // $1/kg for weights up to 100kg , then $1.5/kg for any weight greater than 100kg.
        public decimal Ticket()
        {
            decimal basePrice;
            decimal overPrice = 0;
            // Passenger's weight does not exceed 100kg
            if (Kilo() <= 100)
            {
                basePrice = (decimal)Kilo();
            }
            // Passenger's weight DOES exceed 100kg
            else
            {
                basePrice = 100.00m;
                overPrice = (decimal)(Kilo() - 100) * 1.50m;
            }
            return basePrice + overPrice;
        }


        // Overrides ToString method with a Ticket purchase report
        public override string ToString()
        {
            string str = string.Empty;
            str += "******************************************************************\n";
            str += $"Passenger Name: {First} {Last}\n";
            str += $"Passenger Age: {Age} years\n";
            str += $"Weight: {Math.Round(Weight,2)} lbs. ({Kilo()} kg) price {Ticket():C}\n";
            str += $"Report Timestamp: {Time}\n";
            str += "******************************************************************";
            return str;
        }

    }
}
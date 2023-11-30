///////////////////////////////////////////////////////
// TINFO 200 A, Winter 2023
// UWTacoma SET, Caleb Ghirmai and Ryan Enyeart-Youngblood
// 2023-01-18 - Cs1Sky - C# programming project - A Ticket Calculator App for Commercial Flight
// This program calculates the ticket price for a passenger according to their weight distribution.
// The program prompts the user for a number of passengers up to nine, then prompts each passenger
// for their information (first name, last name, age, and weight in pounds). With this information
// a calculation is done to produce the price needed for each passenger to ride in the aircraft,
// followed by the total cost and weight for the enitre party.
// Error checking is done to ensure that erroneous input is not considered.

///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ----- Description
// 2023-01-15 - Ryan Enyeart -- Initial creation of solution and pseudocode.
// 2023-01-15 - Ryan Enyeart -- Creation of Passenger class porperties.
// 2023-01-18 - Caleb Ghirmai - Creation of file to represent the driver Cs1Sky class.
// 2023-01-18 - Caleb Ghirmai - Creation of CustomerInfo method in the Passenger class to gather passenger data.
// 2023-01-18 - Ryan Enyeart -- Creation of Ticket method in the Passenger class to calulate cost based on weight.
// 2023-01-19 - Caleb Ghirmai - Renamed the main driver class from Cs1Sky.cs to Program.cs.
// 2023-01-19 - Caleb Ghirmai - Renamed variable names in program to satisfy C# naming conventions.
// 2023-01-19 - Caleb Ghirmai - Included a Math.round() function for displaying the weight in kilos to 2 decimals.
// 2023-01-19 - Caleb Ghirmai - Renamed the namespace name from Cs1Sky to SkyTourTicketCalculator.
// 2023-01-19 - Caleb Ghirmai - Changed the type for ticket price from double to decimal.
// 2023-01-19 - Ryan Enyeart -- Creation of expression-bodied method to convert pounds to kilos.
// 2023-01-19 - Caleb Ghirmai - Creation of the ToString override method to display tickets for each passenger.
// 2023-01-20 - Ryan Enyeart -- Creation of welcome message that explains program to user.
// 2023-01-20 - Ryan Enyeart -- Creation of prompt for party size.
// 2023-01-20 - Ryan Enyeart -- Creation of iterator that uses the CustomerInfo method to create and store
//                              multiple passenger objects in an object array.
// 2023-01-20 - Caleb Ghirmai - Creation of iterator that generates the final report for entire party.
// 2023-01-20 - Ryan Enyeart -- Added Math.Round() function to the combined weight of passengers to control digits displayed
// 2023-01-20 - Ryan Enyeart -- Creation of StreamWriter object for testing purposes
// 2023-01-20 - Caleb Ghirmai - Debugging of Passenger class
// 2023-01-20 - Ryan Enyeart -- Debugging of test driver


using System;
using System.IO;


namespace SkyTourTicketCalculator
{
    // This class welcomes the user and explains the pricing guidelines for the ticket purchase.
    // Then it prompts the user for their party size and creates an array of passenger objects to
    // hold each passenger's information. Finally, it calculates the totals and reports them after
    // displaying the ticket report for each passenger separately.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiates the static variables for the total price and weight for the entire party
            double totalLbs = 0.0;
            double totalKilos = 0.0;
            decimal totalCost = 0.00m;

            // Clears the commnad prompt screen if the program is executed from CLI, then dispalys
            // the welcome message.
            Console.Clear();
            Console.WriteLine(@"
                       *************************************************************************
                       ***************************** SkyTour, Inc. *****************************
                       **+-------------------------------------------------------------------+**
                       **|     Thank you for choosing SkyTour, Inc. to experience the        |**
                       **|     majestic peaks of Mt. Ranier! We offer tours for parties      |**
                       **|     of up to nine passengers. To proceed with your ticket         |**
                       **|     purchase, please answer the following questions for each      |**
                       **|     party member. Though your weight can be entered with          |**
                       **|     decimal values, please only use whole numbers for your age.   |**
                       **|                                                                   |**
                       **|     Our prices are as follows:                                    |**
                       **|     ~ Up to 100 kg - $1.00/kg                                     |**
                       **|     ~ Over 100 kg - $100.00 plus $1.50 for every kg over 100 kg.  |**
                       **|     (1 lb. is equal to 2.205 kg)                                  |**
                       **|                                                                   |**
                       **|     Enjoy your time in the sky, and please let us know if you     |**
                       **|     have any questions, comments, or concerns.                    |**
                       **+-------------------------------------------------------------------+**
                       *************************************************************************
            ");




            // Prompt the user for their party size
            Console.WriteLine("How many passengers are in your party?");
            int seats = -1;
            // Validates number of passengers is an integer between 1 and 9
            // Reminds the user to enter a party size acceptable for the trip if the enter incorrectly
            Console.WriteLine("Please enter a whole number between 1 and 9.");
            while (!int.TryParse(Console.ReadLine(), out seats) || seats < 1 || seats > 9)
            {
                Console.WriteLine("Please enter a whole number between 1 and 9.\n" +
                    "How many passengers are in your party?");
            }


            // Creates an object array where each index represents a passenger.
            // Then asks for the information of each passenger.
            Passenger[] partySize = new Passenger[seats];
            for (int i = 0; i < seats; i++)
            {
                partySize[i] = new Passenger();
                partySize[i].CustomerInfo(partySize[i], i);
            }

            // Clears the entry data from the screen
            //Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.Clear();
            Console.WriteLine("\n\n" +
                "************************** Final Report **************************\n" +
                "******************************************************************");

            // Displays the ticket report for each passenger, then
            // adds their wieght and cost to the totals report.
            for (int i = 0; i < partySize.Length; i++)
            {
                Console.WriteLine(partySize[i].ToString());
                totalLbs += partySize[i].Weight;
                totalKilos += partySize[i].Kilo();
                totalCost += partySize[i].Ticket();
            }

            // Displays the report for total weight and cost of a trip
            Console.WriteLine($"\nThe combined weight of all passengers is: {Math.Round(totalLbs,2)} lbs. ({totalKilos} kg)");
            Console.WriteLine($"The total cost for this party is: {totalCost:C}");
            Console.WriteLine("\nThank you for choosing SkyTour, Inc.\n" +
                "Have a great day!");
            Console.Write("\n\n\n");



            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // Creates a text file of the final report to print out if you like
            // Just uncomment this section to change destination path and create text in location
            
            /*using (StreamWriter writer = new StreamWriter("C:\\devl\\SkyTour.txt"))
            {
                writer.WriteLine("\n\n" +
                    "************************** Final Report **************************\n" +
                    "******************************************************************");

                // Displays the ticket report for each passenger, then
                // adds their wieght and cost to the totals report.
                for (int i = 0; i < partySize.Length; i++)
                {
                    writer.WriteLine(partySize[i].ToString());
                    totalLbs += partySize[i].Weight;
                    totalKilos += partySize[i].Kilo();
                    totalCost += partySize[i].Ticket();
                   
                }

                // Displays the report for total weight and cost of a trip
                writer.WriteLine($"\nThe combined weight of all passengers is: {Math.Round(totalLbs, 2)} lbs. ({totalKilos} kg)");
                writer.WriteLine($"The total cost for this party is: {totalCost:C}");
                writer.WriteLine("\nThank you for choosing SkyTour, Inc.\n" +
                    "Have a great day!");
                writer.Write("\n\n\n");
            }*/
        }

    }
}


using System;
using System.Threading.Tasks;

namespace TempConvert
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Create an instance of the web service client
                var tempConvertClient = new TempConvert.ServiceReference1.TempConvertSoapClient("TempConvertSoap");

                // Ask the user what they want to do
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1 — Convert from Fahrenheit to Celsius");
                Console.WriteLine("2 — Convert from Celsius to Fahrenheit");
                Console.Write("Your choice (1 or 2): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Get Fahrenheit temperature input from the user
                        Console.Write("Enter temperature in Fahrenheit: ");
                        if (double.TryParse(Console.ReadLine(), out double fahrenheit))
                        {
                            // Convert Fahrenheit to Celsius and display the result
                            string celsiusResult = await tempConvertClient.FahrenheitToCelsiusAsync(fahrenheit.ToString()).ConfigureAwait(false);
                            Console.WriteLine($"Temperature in Celsius: {celsiusResult}");
                        }
                        else
                        {
                            // Inform the user about incorrect Fahrenheit input
                            Console.WriteLine("Incorrect temperature value.");
                        }
                        break;

                    case "2":
                        // Get Celsius temperature input from the user
                        Console.Write("Enter temperature in Celsius: ");
                        if (double.TryParse(Console.ReadLine(), out double celsius))
                        {
                            // Convert Celsius to Fahrenheit and display the result
                            string fahrenheitResult = await tempConvertClient.CelsiusToFahrenheitAsync(celsius.ToString()).ConfigureAwait(false);
                            Console.WriteLine($"Temperature in Fahrenheit: {fahrenheitResult}");
                        }
                        else
                        {
                            // Inform the user about incorrect Celsius input
                            Console.WriteLine("Incorrect temperature value.");
                        }
                        break;

                    default:
                        // Inform the user about invalid menu choice
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        break;
                }
            }
            catch (System.ServiceModel.FaultException ex)
            {
                // Handle SOAP Fault exceptions
                Console.WriteLine($"SOAP Fault occurred: {ex.Message}");
                // Optionally display fault details: ex.Detail
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                // Keep the console window open
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}

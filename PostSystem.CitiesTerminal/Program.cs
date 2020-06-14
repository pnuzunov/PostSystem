using Microsoft.EntityFrameworkCore;
using PostSystem.Business.DTO;
using PostSystem.Business.Services;
using PostSystem.WebAPI.Controllers;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PostSystem.CitiesTerminal
{
    class Program
    {
        static string input = "";
        static CityService cityService = new CityService();
        static CitiesController citiesController = new CitiesController();

        static void Main(string[] args)
        {
            while(!input.Equals("exit"))
            {
                Console.Clear();
                Console.WriteLine("****************************************");
                Console.WriteLine("*     CITIES TERMINAL - MAIN MENU      *");
                Console.WriteLine("****************************************");
                Console.WriteLine("OPTIONS:");
                Console.WriteLine("1. Get city by post code");
                Console.WriteLine("2. Create city");
                Console.WriteLine("3. Update city");
                Console.WriteLine("4. Delete city");
                Console.WriteLine("5. Get all cities");
                Console.WriteLine("6. Delete all cities");
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("/> ");

                input = Console.ReadLine();

                switch(input)
                {
                    case "0": Environment.Exit(0); break;
                    case "1": GetCityByPostCode(); break;
                    case "2": CreateCity(); break;
                    case "3": UpdateCity(); break;
                    case "4": DeleteCity(); break;
                    case "5": GetAllCities(); break;
                    case "6": DeleteAllCities(); break;
                    default:
                        {
                            Console.WriteLine("Invalid input!");
                            Console.ReadLine();
                            break;
                        }
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }

        }

        private static void DeleteAllCities()
        {
            throw new NotImplementedException();
        }

        private static void GetAllCities()
        {
            CityService service = new CityService();
            Console.WriteLine("Connecting to service...");

            foreach(CityDto cityDto in service.GetAll())
            {
                Console.WriteLine("{0}, {1} (Express delivery {2})",
                    cityDto.City_Name,
                    cityDto.City_Post_Code,
                    cityDto.HasExpressDelivery ? "available" : "unavailable");
            }
        }

        private static void DeleteCity()
        {
            Console.Write("Enter post code: ");
            input = Console.ReadLine();
            short postCode = short.Parse(input);
            bool success = cityService.Delete(cityService.GetAll(postCode).FirstOrDefault().Id);
            if(success)
            {
                Console.WriteLine("City deleted.");
            }
            else Console.WriteLine("City not found!");
        }

        private static void UpdateCity()
        {
            CityDto cityDto = GetCityByPostCode();
            if (cityDto == null) return;

            cityDto.City_Name = GetNameFromInput();
            cityDto.City_Post_Code = GetPostCodeFromInput();
            cityDto.HasExpressDelivery = GetHasExpressFromInput();

            Console.WriteLine("Updating city...");

            if (cityService.Update(cityDto))
                Console.WriteLine("City updated.");
            else Console.WriteLine("Update failed.");
        }

        private static void CreateCity()
        {

            string cityName = GetNameFromInput();
            int postCode = GetPostCodeFromInput();
            bool hasExpressDelivery = GetHasExpressFromInput();

            Console.WriteLine("Creating city...");

            if (cityService.Create(new CityDto() { City_Name = cityName, City_Post_Code = postCode, HasExpressDelivery = hasExpressDelivery }))
                Console.WriteLine("City added.");
            else Console.WriteLine("Creation failed.");
        }

        private static CityDto GetCityByPostCode()
        {

            short postCode = GetPostCodeFromInput();

            Console.WriteLine("Searching database...");
            CityDto cityDto = citiesController.GetAll(postCode).FirstOrDefault();
            if(cityDto != null)
            {
                Console.WriteLine($"Match found : {cityDto.City_Name}, {cityDto.City_Post_Code}");
            }
            else Console.WriteLine("No matches found.");
            return cityDto;
        }

        private static string GetNameFromInput()
        {
            bool init = true;
            string _input = "";
            do
            {
                if (!init)
                {
                    Console.WriteLine("Invalid input!");
                    init = false;
                }
                Console.Write("Enter name: ");
                _input = Console.ReadLine();
            } while (_input.Equals("") || _input.Length > 50);
            return _input;
        }

        private static short GetPostCodeFromInput()
        {
            bool init = true;
            string _input = "";
            do
            {
                if (!init)
                {
                    Console.WriteLine("Invalid input!");
                    init = false;
                }
                Console.Write("Enter post code: ");
                _input = Console.ReadLine();
            } while (_input.Equals(""));
            return short.Parse(_input);
        }

        private static bool GetHasExpressFromInput()
        {
            bool init = true;
            string _input = "";
            do
            {
                if (!init)
                {
                    Console.WriteLine("Invalid input!");
                    init = false;
                }
                Console.WriteLine("Express delivery? (y/n)");
                _input = Console.ReadLine();
            } while (!_input.Equals("y") && !_input.Equals("n"));
            return _input.Equals("y");
        }

    }
}
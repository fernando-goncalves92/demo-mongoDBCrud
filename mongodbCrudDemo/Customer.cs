using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongodbCrudDemo
{
    class Customer
    {
        public const string MongoCollection = "Customers";

        [BsonId]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Register { get; private set; }
        public Address Address { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Guid....: {Id}");
            Console.WriteLine($"Customer: {Name} {Surname}, registered at {Register:dd/MM/yyyy HH:mm}");

            if (Address != null)
            {
                Console.WriteLine($"Street..: {Address.Street}");
                Console.WriteLine($"City....: {Address.City}");
                Console.WriteLine($"State...: {Address.State}");
                Console.WriteLine($"Zip Code: {Address.ZipCode}");
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------");
        }

        public class Factory
        {
            private static List<string> _names = new List<string>() 
            { 
                "Fernando", 
                "Carla",
                "Vera",
                "Fabiano",
                "Deolinda",
                "Emanuelle",
                "Alessandro",
                "Flavia",
                "Ana",
                "Rosali",
                "Felipe",
                "Juliana",
                "Edilson",
                "Anthony",
                "Luanny",
            };

            private static List<string> _surnames = new List<string>()
            {
                "Souza",
                "de Souza",
                "Silva",
                "da Silva",
                "Santos",
                "dos Santos",
                "Fantini",
                "de Moraes",
                "Rossetti",
                "Gonçalves",
                "Briotto",
                "Pereira",
                "Cavalcante",
                "Oliveira",
                "Matos",
            };

            public static Customer CreateRandomCustomer()
            {
                var random = new Random();

                return new Customer()
                {
                    Id = new Guid(), 
                    Name = _names[random.Next(1, 15)],
                    Surname = _surnames[random.Next(1, 15)],
                    Register = DateTime.Now,
                    Address = Address.Factory.CreateRandomAddress()
                };
            }

            public static Customer CreateCustomer(string name, string surname, Address address, Guid id = new Guid())
            {
                return new Customer()
                {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Register = DateTime.Now,
                    Address = address
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace MongodbCrudDemo
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public class Factory
        {
            private static List<Address> _addresses = new List<Address>()
            {
                new Address() { Street = "Rua Antônio Alves da Costa", City = "Campinas", State = "SP", ZipCode = "00000-123" },
                new Address() { Street = "Rua Alberto Fábio Silveira", City = "Guarulhos", State = "SP", ZipCode = "12345-000" },
                new Address() { Street = "Rua Doutor Gilmar Barroso", City = "Valadares", State = "MG", ZipCode = "99881-232" },                
            };

            public static Address CreateRandomAddress()
            {
                return _addresses[new Random().Next(1, 3)];
            }

            public static Address CreateAddress(string street, string city, string state, string zipCode)
            {
                return new Address()
                {
                    Street = street,
                    City = city,
                    State = state,
                    ZipCode = zipCode
                };
            }
        }
    }
}

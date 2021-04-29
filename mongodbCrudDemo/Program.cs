using System;

namespace MongodbCrudDemo
{
    class Program
    {
        private const string MainMenu = "0";
        private const string CreateRandomCustomer = "1";
        private const string CreateManuallyCustomer = "2";
        private const string UpdateRandomCustomerNames = "3";
        private const string DeleteCustomerId = "4";
        private const string GetCustomerId = "5";
        private const string SeeAllCustomers = "6";
        private const string GererateDataMass = "7";

        static void Main(string[] args)
        {
            try
            {
                var option = MainMenu;                 
                var mongo = new MongoCrud<Customer>(Customer.MongoCollection);

                switch (option)
                {
                    case MainMenu:
                        {
                            BuildMainMenu();

                            option = Console.ReadLine();

                            if (option.Equals(CreateRandomCustomer))
                                goto case CreateRandomCustomer;
                            else if (option.Equals(CreateManuallyCustomer))
                                goto case CreateManuallyCustomer;
                            else if (option.Equals(UpdateRandomCustomerNames))
                                goto case UpdateRandomCustomerNames;
                            else if (option.Equals(DeleteCustomerId))
                                goto case DeleteCustomerId;
                            else if (option.Equals(GetCustomerId))
                                goto case GetCustomerId;
                            else if (option.Equals(SeeAllCustomers))
                                goto case SeeAllCustomers;
                            else if (option.Equals(GererateDataMass))
                                goto case GererateDataMass;
                            else
                                goto default;

                        }
                    case CreateRandomCustomer:
                        {
                            var customer = Customer.Factory.CreateRandomCustomer();

                            mongo.CreateDocument(customer);
                            
                            CreateCustomerSuccessMessage(customer);

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                    case CreateManuallyCustomer:
                        {
                            Console.WriteLine("------------------------------------------------");
                            Console.WriteLine("Fill all fields bellow to create a new customer:");
                            Console.WriteLine("------------------------------------------------");

                            Console.Write("Name....: "); var name = Console.ReadLine();
                            Console.Write("Surname.: "); var surname = Console.ReadLine();
                            Console.Write("Street..: "); var street = Console.ReadLine();
                            Console.Write("City....: "); var city = Console.ReadLine();
                            Console.Write("State...: "); var state = Console.ReadLine();
                            Console.Write("Zip Code: "); var zipCode = Console.ReadLine();

                            var address = Address.Factory.CreateAddress(street, city, state, zipCode);
                            var customer = Customer.Factory.CreateCustomer(name, surname, address);

                            mongo.CreateDocument(customer);

                            CreateCustomerSuccessMessage(customer);

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                    case UpdateRandomCustomerNames:
                        {
                            Console.WriteLine();
                            Console.Write("Inform the customer id you want to update: ");

                            var customerId = Console.ReadLine();

                            if (string.IsNullOrEmpty(customerId))
                            {
                                InvalidCustomerIdMessage();
                            }
                            else
                            {
                                try
                                {
                                    var guid = new Guid(customerId);
                                    var customerToUpdate = mongo.LoadDocument(guid);

                                    if (customerToUpdate == null)
                                    {
                                        CustomerNotFoundMessage();
                                    }
                                    else
                                    {
                                        Console.WriteLine();

                                        customerToUpdate.ShowInfo();

                                        Console.WriteLine();
                                        Console.WriteLine("--------------------------------------------------------");
                                        Console.WriteLine("Fill all fields bellow to update customer's information:");
                                        Console.WriteLine("--------------------------------------------------------");

                                        Console.Write("Name....: "); var name = Console.ReadLine();
                                        Console.Write("Surname.: "); var surname = Console.ReadLine();
                                        Console.Write("Street..: "); var street = Console.ReadLine();
                                        Console.Write("City....: "); var city = Console.ReadLine();
                                        Console.Write("State...: "); var state = Console.ReadLine();
                                        Console.Write("Zip Code: "); var zipCode = Console.ReadLine();

                                        var newAddress = Address.Factory.CreateAddress(street, city, state, zipCode);
                                        var newCustomer = Customer.Factory.CreateCustomer(name, surname, newAddress, customerToUpdate.Id);

                                        mongo.UpdateDocument(newCustomer, customerToUpdate.Id);

                                        Console.WriteLine();
                                        Console.WriteLine("-------------------------------");
                                        Console.WriteLine($"Customer updated successfully!");
                                        Console.WriteLine("-------------------------------");
                                    }
                                }
                                catch
                                {
                                    InvalidCustomerIdMessage();
                                }
                            }

                            BackToMainMenuMessage();

                            goto case MainMenu;                            
                        }
                    case DeleteCustomerId:
                        {
                            Console.WriteLine();
                            Console.Write("Inform the customer id you want to delete: ");

                            var customerId = Console.ReadLine();

                            if (string.IsNullOrEmpty(customerId))
                            {
                                InvalidCustomerIdMessage();
                            }
                            else
                            {
                                try 
                                { 
                                    var guid = new Guid(customerId);
                                    var customer = mongo.LoadDocument(guid);

                                    if (customer == null)
                                    {
                                        CustomerNotFoundMessage();
                                    }
                                    else
                                    {
                                        mongo.DeleteDocument(customer.Id);

                                        Console.WriteLine("-------------------------------");
                                        Console.WriteLine($"Customer deleted successfully!");
                                        Console.WriteLine("-------------------------------");

                                        customer.ShowInfo();
                                    }
                                } 
                                catch
                                {
                                    InvalidCustomerIdMessage();
                                }
                            }

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }                    
                    case GetCustomerId:
                        {
                            Console.WriteLine();
                            Console.Write("Inform the customer id you want to find: ");

                            var customerId = Console.ReadLine();

                            if (string.IsNullOrEmpty(customerId))
                            {
                                InvalidCustomerIdMessage();
                            }
                            else
                            {
                                try
                                {
                                    var guid = new Guid(customerId);
                                    var customer = mongo.LoadDocument(guid);

                                    if (customer == null)
                                    {
                                        CustomerNotFoundMessage();
                                    }
                                    else
                                    {
                                        Console.WriteLine();

                                        customer.ShowInfo();
                                    }
                                }
                                catch
                                {
                                    InvalidCustomerIdMessage();
                                }
                            }

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                    case SeeAllCustomers:
                        {
                            var customers = mongo.LoadCollection();

                            Console.WriteLine();

                            foreach (var customer in customers)
                            {
                                customer.ShowInfo();
                            }

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                    case GererateDataMass:
                        {
                            Console.WriteLine();
                            Console.WriteLine("-------------------");
                            Console.WriteLine($"Generating data...");
                            Console.WriteLine("-------------------");

                            for (int count = 0; count < 1000; count++)
                            {
                                mongo.CreateDocument(Customer.Factory.CreateRandomCustomer());
                            }

                            Console.WriteLine("-----------------------------");
                            Console.WriteLine($"Data generated successfully!");
                            Console.WriteLine("-----------------------------");

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                    default:
                        {
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine($"The option \"{option}\" does not exists!");
                            Console.WriteLine("-----------------------------------------");

                            BackToMainMenuMessage();

                            goto case MainMenu;
                        }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
        }

        private static void CreateCustomerSuccessMessage(Customer customer)
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"Customer {customer.Name} {customer.Surname} created successfully!");
            Console.WriteLine("------------------------------------------------------------------");
        }

        private static void BuildMainMenu()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(" Choose an operation you'd like to perform ");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1. Create a new random customer");
            Console.WriteLine("2. Create a new manually customer");
            Console.WriteLine("3. Update customer by id");
            Console.WriteLine("4. Delete customer by id");
            Console.WriteLine("5. Get customer by id");
            Console.WriteLine("6. See all regitered customers");
            Console.WriteLine("7. Generate a data mass");
            Console.WriteLine();

            Console.Write("Option: ");
        }

        private static void BackToMainMenuMessage()
        {   
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Press any key to back to main menu...");
            Console.WriteLine("-------------------------------------");
            Console.ReadKey();
        }

        private static void InvalidCustomerIdMessage()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine("Invalid customer id!");
            Console.WriteLine("--------------------");
        }

        private static void CustomerNotFoundMessage()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------");
            Console.WriteLine("Customer not found!");
            Console.WriteLine("-------------------");
        }
    }
}

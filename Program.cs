using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SuperMarketCashier
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, decimal price, int qty)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = qty;
        }

        public void Show()
        {
            Console.WriteLine($"{Id}. {Name} | Price: {Price} ILS | Stock: {Quantity}");
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>()
        {
            new Product(1, "Cola", 5, 50),
            new Product(2, "Chips", 4, 100),
            new Product(3, "Chocolate", 7, 30),
            new Product(4, "Bread", 3, 80),
            new Product(5, "Milk", 9, 20),
        };

        static decimal totalSales = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== SuperMarket - Cashier =====");
                Console.WriteLine($"Today Sales: {totalSales} ILS");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("1. Show Products");
                Console.WriteLine("2. Sell Product (Invoice)");
                Console.WriteLine("3. Add New Product");
                Console.WriteLine("4. Search Product");
                Console.WriteLine("5. Save Sales Report");
                Console.WriteLine("0. Exit");
                Console.Write("\nChoose: ");
                string choice = Console.ReadLine();

                if (choice == "1") ShowProducts();
                else if (choice == "2") Sell();
                else if (choice == "3") AddProduct();
                else if (choice == "4") Search();
                else if (choice == "5") SaveReport();
                else if (choice == "0") break;
            }
        }

        static void ShowProducts()
        {
            Console.Clear();
            Console.WriteLine("--- Products ---");
            foreach (var p in products) p.Show();
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }

        static void Sell()
        {
            Console.Clear();
            Console.WriteLine("--- New Sale ---");
            foreach (var p in products) p.Show();

            Console.Write("\nEnter Product ID: ");
            int id = int.Parse(Console.ReadLine());
            var prod = products.FirstOrDefault(p => p.Id == id);
            if (prod == null) { Console.WriteLine("Not Found!"); Console.ReadKey(); return; }

            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            if (qty > prod.Quantity) { Console.WriteLine("Not enough stock!"); Console.ReadKey(); return; }

            decimal total = prod.Price * qty;
            prod.Quantity -= qty;
            totalSales += total;

            Console.WriteLine("\n===== INVOICE =====");
            Console.WriteLine($"Product: {prod.Name}");
            Console.WriteLine($"Qty: {qty} | Price: {prod.Price}");
            Console.WriteLine($"Total: {total} ILS");
            Console.WriteLine("Thank you!");
            Console.ReadKey();
        }

        static void AddProduct()
        {
            Console.Clear();
            Console.Write("Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            int newId = products.Max(p => p.Id) + 1;
            products.Add(new Product(newId, name, price, qty));
            Console.WriteLine("Added Successfully!");
            Console.ReadKey();
        }

        static void Search()
        {
            Console.Clear();
            Console.Write("Enter Product Name: ");
            string s = Console.ReadLine();
            var found = products.Where(p => p.Name.ToLower().Contains(s.ToLower())).ToList();
            if (found.Count == 0) Console.WriteLine("Nothing found");
            else foreach (var p in found) p.Show();
            Console.ReadKey();
        }

        static void SaveReport()
        {
            string report = $"Report {DateTime.Now} - Sales: {totalSales} ILS\n";
            File.AppendAllText("report.txt", report);
            Console.WriteLine("Report saved in report.txt");
            Console.WriteLine($"Total Sales: {totalSales}");
            Console.ReadKey();
        }
    }
}
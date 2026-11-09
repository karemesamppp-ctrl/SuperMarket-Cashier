using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("  SuperMarket Cashier - PRO v2    ");
        Console.WriteLine("  Kareem Store - Deir Al Balah   ");
        Console.WriteLine("=================================");
        Console.WriteLine();

        double total = 0;
        List<string> fatoura = new List<string>();

        // قاعدة بيانات صغيرة للباركود
        Dictionary<string, string> products = new Dictionary<string, string>();
        products.Add("123", "Cola - 5");
        products.Add("456", "Bread - 3");
        products.Add("789", "Milk - 8");

        while (true)
        {
            Console.Write("Enter barcode OR product name (or done): ");
            string input = Console.ReadLine();

            if (input == "done")
            {
                break;
            }

            string name = input;
            double price = 0;

            // لو دخل باركود موجود، جيب السعر لحاله
            if (products.ContainsKey(input))
            {
                string[] parts = products[input].Split('-');
                name = parts[0].Trim();
                price = Convert.ToDouble(parts[1].Trim());
                Console.WriteLine("Found: " + name + " Price: " + price);
            }
            else
            {
                Console.Write("Enter price: ");
                price = Convert.ToDouble(Console.ReadLine());
            }

            Console.Write("Enter quantity: ");
            int qty = Convert.ToInt32(Console.ReadLine());

            double sum = price * qty;
            total += sum;

            fatoura.Add(name + " - " + price + " * " + qty + " = " + sum);
            Console.WriteLine("Added! Current total: " + total);
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine();
        Console.WriteLine("========== RECEIPT ==========");
        Console.WriteLine("Date: " + DateTime.Now);
        Console.WriteLine("Store: Kareem Store");
        Console.WriteLine("-----------------------------");
        foreach (string item in fatoura)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Total: " + total);

        double discount = 0;
        if (total >= 100)
        {
            discount = total * 0.10;
            Console.WriteLine("Discount 10%: -" + discount);
            total = total - discount;
            Console.WriteLine("Total after discount: " + total);
        }

        Console.WriteLine("-----------------------------");
        Console.Write("Enter amount paid: ");
        double paid = Convert.ToDouble(Console.ReadLine());

        double rest = paid - total;
        Console.WriteLine("Change: " + rest);
        Console.WriteLine("=============================");
        Console.WriteLine("Thank you for visiting!");

        // حفظ البيعة في ملف
        string saleLine = DateTime.Now + " | Total: " + total + " | Paid: " + paid;
        File.AppendAllText("sales.txt", saleLine + Environment.NewLine);
        Console.WriteLine("Sale saved to sales.txt!");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
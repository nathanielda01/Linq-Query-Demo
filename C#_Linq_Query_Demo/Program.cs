// Created By: Nathaniel Anderton
// Purpose: To present linq querys to demonstrate familiarity with them
// ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Query_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] odds = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };

            int[] evens = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18 };

            var data = new[]
            {
                new { ID = 100, Name = "Grumpy", Job = "Miner" },
                new { ID = 100, Name = "Sleepy", Job = "Mattress Salesman" },
                new { ID = 200, Name = "Snizzy", Job = "Allergist" },
                new { ID = 300, Name = "Doc", Job = "M.D." },
                new { ID = 400, Name = "Happy", Job = "Professor" },
                new { ID = 500, Name = "Dopey", Job = "Student" },
                new { ID = 600, Name = "Bashful", Job = "Programmer" },
                new { ID = 220, Name = "Snow White", Job = "Princess" },
                new { ID = 150, Name = "Prince Charming", Job = "Prince" },
                new { ID = 175, Name = "Wicked Witch", Job = "Queen" }
            };

            LinqBuild linqBuild = new LinqBuild();

            /****************QUERY_0*******************/
            {
                Console.WriteLine("Query0");
                IEnumerable<int> query0 = odds
                    .Where(n => n % 3 == 0);
                foreach (var q in query0)
                {
                    Console.Write(q + " ");
                }
                Console.WriteLine("");
            }
            /****************QUERY_1*******************/
            {
                Console.WriteLine("Query1");
                foreach (var o in odds)
                {
                    Console.Write(o + ": ");
                    IEnumerable<int> query1 = evens
                        .Where(e => e > 2 * o);
                    foreach (var e in query1)
                    {
                        Console.Write(e + " ");
                    }
                    Console.WriteLine("");
                }
            }
            /****************QUERY_2*******************/
            {
                Console.WriteLine("Query2");
                IEnumerable<object> query2 = data
                        .Where(emp => (emp.ID > 100 && emp.ID < 500) && (emp.Name.Contains("y") || emp.Job.EndsWith("e") || emp.Job.Contains("e") || emp.Job.Equals("Student") || emp.Job.Equals("Programmer")));
                foreach (var emp in query2)
                {
                    Console.WriteLine(emp);
                }
                Console.WriteLine("");
            }
            /****************QUERY_3*******************/
            {
                Console.WriteLine("Query3");
                IEnumerable<LinqBuild.Product> query3 = linqBuild.GetProductList()
                        .Where(p => p.ProductID > 10 && p.ProductID < 66)
                        .OrderBy(p => p.UnitsInStock).ToList();
                foreach (var p in query3)
                {
                    Console.WriteLine(p.ProductName + " " + p.UnitsInStock);
                }
                Console.WriteLine("");
            }
            /****************QUERY_4*******************/
            {
                Console.WriteLine("Query4");
                var query4 = from p in linqBuild.GetProductList()
                             let categories = p.Category
                             let names = p.ProductName
                             where (p.UnitPrice > 10.0000M || p.ProductID < 66) && !names.StartsWith("G") && names.Length < 20
                             group p by categories into nameCats
                             orderby nameCats.Key
                             select nameCats;



                foreach (IGrouping<string, LinqBuild.Product> productGroup in query4)
                {
                    Console.WriteLine(productGroup.Key);
                    Console.WriteLine("{0,0}{1,7}{2,21}{3,11}", "ID", "Name", "Price", "Stock");

                    foreach (var product in productGroup)
                    {
                        Console.WriteLine($"{product.ProductID,2}" +
                            $"   {product.ProductName,-20}" +
                            $"{product.UnitPrice,6:C}" +
                            $"{product.UnitsInStock,10}");
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");


            Console.ReadKey();


        }
    }
}

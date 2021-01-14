using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ShipCSV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ships = new List<Ship>();
            using (var sr = new StreamReader(@"C:\Users\GET Academy\source\repos\ShipCSV\ShipCSV\ShipTest1.txt"))
            {
                sr.ReadLine();
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;
                    var row = line.Split(',');
                    var shipYard = new ShipYard(row);
                    var ship = shipYard.CreateShip();
                    var owner = shipYard.CreateOwner(ship);
                    var countedOwner = owner.CountShipsPerOwner();
                    ship.Owner = countedOwner; //two-way object reference set for print/WriteLine
                    if (countedOwner.ShipCount == 2) // when there're two instances in Owners:
                    {
                        ships.Add(countedOwner.Ship); //add current/first ship instance
                        countedOwner.RemoveOwner(); //remove to isolate remaining instance
                        var secondShip = countedOwner.AddSecondShip();
                        ships.Add(secondShip); //adds remaining ship
                        countedOwner.AddOwner(); //re-adds owner to maintain count/algorithm
                    }
                    if (countedOwner.ShipCount > 2) ships.Add(countedOwner.Ship);
                    // more than two instances means the owner has been treated, and can be added directly,
                    // bypassing the steps above which are designed for handling the first occurance of serial-ownership 
                }
            }
            foreach (var ship in ships)
            {
                Console.WriteLine($"{ship.Id} {ship.Name} {ship.Owner.Name} {ship.Owner.ShipCount}");
            }
        }
    }
}

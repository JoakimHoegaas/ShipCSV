﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ShipCSV
{
    class Program
    {
        static void Main(string[] args)
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
                    if (countedOwner.ShipCount > 2) ships.Add(countedOwner.Ship);
                    if (countedOwner.ShipCount == 2) // if > 2 - bypass algorithm below
                    {
                        ships.Add(countedOwner.Ship); //add current/first ship instance
                        countedOwner.RemoveOwner(); //remove to isolate remaining instance
                        var secondShip = countedOwner.AddSecondShip();
                        ships.Add(secondShip); //adds remaining ship
                        countedOwner.AddOwner(); //re-adds owner to maintain algorithm

                    }
                }
            }
            Console.WriteLine("yeeeee");
            foreach (var ship in ships)
            {
                Console.WriteLine($"{ship.Name}");
            }
        }
    }
}
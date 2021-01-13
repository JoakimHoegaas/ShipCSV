using System;
using System.Collections.Generic;
using System.Text;

namespace ShipCSV
{
    public class Owner
    {
        public string Name;
        public int ShipCount;
        public Ship Ship;
        public Ship SecondShip;
        public List<Owner> Owners;

        public Owner(string ownerName, Ship ship)
        {
            Name = ownerName;
            Ship = ship;
            Owners = new List<Owner>() { this };
            ShipCount = 0;

        }

        public Owner CountShipsPerOwner()
        {
            var i = 0;
            foreach (var owner in Owners)
            {
                if (owner.Name == Name) i++;
            }
            ShipCount = i;
            return this;
        }

        public void AddOwner()
        {
            Owners.Add(this);
        }

        public void RemoveOwner()
        {
            Owners.Remove(this);
        }

        public Ship AddSecondShip()
        {
            foreach (var owner in Owners)
            {
                if (Name == owner.Name)
                {
                    SecondShip = owner.Ship;
                }
            }
            return SecondShip;
        }
    }
}

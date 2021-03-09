using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BeerInfo
{
    public static class Beers
    {
        public static List<Beer> BeerList = new List<Beer>()
        {
            new Beer {Id = 1, Name = "Carlsberg", Abv = 5, Price = 6},
            new Beer {Id = 2, Name = "Tuborg", Abv = 5, Price = 6}
        };

        public static List<Beer> GetAll()
        {
            return BeerList;
        }

        public static Beer GetById(int id)
        {
            return BeerList.Find(x => x.Id == id);
        }

        public static void AddBeer(Beer beer)
        {
            BeerList.Add(beer);
        }
    }
}

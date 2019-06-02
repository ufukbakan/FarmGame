using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyFirstGame
{
    public class Seed
    {
        public string name;
        public string listName;
        public int count = 0;
        public int price = 0;

        public Seed(string setName)
        {
            name = setName;
            listName = name.Replace("Seed", "");
            listName = Char.ToUpper(listName[0]) + listName.Substring(1) + " Seed";
            /*if(name == "cornSeed")
            {
                listName = "Corn Seed";
            }
            else if(name == "pumpkinSeed")
            {
                listName = "Pumpkin Seed";
            }*/
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public class Seed
    {
        public string name;
        public int count = 0;
        public bool isGrownUp = false;

        public Seed(string setName)
        {
            name = setName;
        }
    }
}

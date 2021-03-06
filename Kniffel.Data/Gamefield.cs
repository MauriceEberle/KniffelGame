using System;
using System.Collections.Generic;
using System.Text;

namespace Kniffel.Data
{
    public class Gamefield
    {
        public int[] One { get; set; }
        public int [] Two { get; set; }
        public int[] Three { get; set; }
        public int[] Four { get; set; }
        public int[] Five { get; set; }
        public int[] Six { get; set; }
        public int[] ThreeOfAKind { get; set; }
        public int[] FourOfAkind { get; set; }
        public int[] SmallStreet { get; set; }
        public int[] BigStreet { get; set; }
        public int[] FullHouse { get; set; }
        public int[] Kniffel { get; set; }
        public int[] Chance { get; set; }


        public Gamefield(int games)
        {
            One = new int[games];
            Two = new int[games];
            Three = new int[games];
            Four = new int[games];
            Five = new int[games];
            Six = new int[games];
            ThreeOfAKind = new int[games];
            FourOfAkind = new int[games];
            SmallStreet = new int[games];
            BigStreet = new int[games];
            FullHouse = new int[games];
            Kniffel = new int[games];
            Chance = new int[games];

        }


    }
}

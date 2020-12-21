using System;
using System.Collections.Generic;
using System.Text;

namespace Kniffel.Data
{
    public class Player
    {
        public string Name { get; private set; }
        public int Points { get; set; }
        public int Movements { get; set; }
        private Cube[] cubes;
        private string hallo;

        public Player(string name)
        {
            Name = name;
            cubes = new Cube[5];
        }
    }
}

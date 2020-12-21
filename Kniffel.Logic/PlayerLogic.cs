using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kniffel.Data;

namespace Kniffel.Logic
{
    public class PlayerLogic
    {
        public Cube[] Cubes { get; set; }
        public List<Cube> TempChosenCubes { get; set; }
        public Cube[] ChosenCubes;

        public Cube[] ChooseCubes(Cube[] cubes)
        {
            TempChosenCubes = new List<Cube>();
            Cubes = cubes;
            for (int i = 1; i<= Cubes.Length; i++)
            {
                if (Cubes[i].IsChosen == true)
                {
                    TempChosenCubes.Add(cubes[i]);
                }
            }
            ChosenCubes = new Cube[TempChosenCubes.Count];
            for(var i = 1; i <= TempChosenCubes.Count; i++)
            {
                ChosenCubes[i] = TempChosenCubes[i];
            }
            return ChosenCubes;
        }

    }
}

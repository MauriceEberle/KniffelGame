using System;
using System.Collections.Generic;
using System.Data;
using Kniffel.Data;

namespace Kniffel.Logic
{
    public class CubeLogic
    {
        
        public Cube[] GenerateCubeNumber(Cube[] cubes)
        {

            Random randCube = new Random();
            for(int i = 1; i <= cubes.Length; i++)
            {
                cubes[i].cubeNumber = randCube.Next(1, 7);
            }
            return cubes;
        }

        //public Cube[] SortedCubeNumbers(Cube[] cubes)
        //{
            
        //}

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestHand
{
    class Program
    {
        static void Main(string[] args)
        {
            //var game = new BestHandGame();
            //game.StartGame();

            var game = new BestHandGameV2();
            game.StartGame();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

        }
    }
}

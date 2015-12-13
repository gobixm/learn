using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalApp.Models
{
    public class Player
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string Name { get; set; }

        public Player(string name, double x, double y)
        {
            X = x;
            Y = y;
            Name = name;
        }

        internal void Bound(double fieldWidth, double fieldHeight)
        {             
            X = X < 0 ? 0 : Math.Min(fieldWidth-5, X);
            Y = Y < 0 ? 0 : Math.Min(fieldHeight-5, Y);
        }
    }
}
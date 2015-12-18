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
        public string Color { get; set; }

        private static Random _random = new Random();

        public Player(string name, double x, double y)
        {
            X = x;
            Y = y;
            Name = name;
            Color = GetRandomColor();
        }

        private string GetRandomColor()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", _random.Next(255), _random.Next(255), _random.Next(255));
        }

        internal void Bound(double fieldWidth, double fieldHeight)
        {             
            X = X < 0 ? 0 : Math.Min(fieldWidth-5, X);
            Y = Y < 0 ? 0 : Math.Min(fieldHeight-5, Y);
        }
    }
}
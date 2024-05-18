using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Point
    {
        public double X {  get; set; }
        public double Y { get; set; }
        
        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point point, Point other) => new Point((int)(point.X + other.X), (int)(point.Y + other.Y));
    }
}

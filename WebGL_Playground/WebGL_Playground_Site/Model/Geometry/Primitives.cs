using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebGL_Playground_Site.Geometry {
    public class Point {
        public float x;
        public float y;

        public Point(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static IEnumerable<Point> FloatsToPoints(IEnumerable<float> values) {
            if ((values.Count() & 1) == 1) {
                return Enumerable.Empty<Point>();
            }

            return values.Zip(values.Skip(1)).Select(x => new Point(x.First, x.Second));
        }
    }
}
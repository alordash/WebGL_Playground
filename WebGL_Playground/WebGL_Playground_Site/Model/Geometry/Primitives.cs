using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WebGL_Playground_Site.WebGLWrapping;

namespace WebGL_Playground_Site.Geometry {
    public class Point {
        public float x;
        public float y;

        public Point(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static IEnumerable<Point> FloatsToPoints(IEnumerable<float> values) {
            return values.SelectTwo((x, y) => new Point(x, y));
        }

        public static IEnumerable<Point> GeneratePoints(int count) {
            return Point.FloatsToPoints(GLHelper.GenerateFloats(count * 2));
        }
    }
}
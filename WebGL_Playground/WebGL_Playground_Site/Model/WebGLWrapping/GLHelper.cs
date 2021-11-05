using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGL_Playground_Site.WebGLWrapping {
    [Flags]
    public enum ColorsGeneratorFlags {
        Default = 0,
        FixRed = 1 << 0,
        FixGreen = 1 << 1,
        FixBlue = 1 << 2,
        FixAlpha = 1 << 3,
    }

    public class TrianglesSet {
        public IEnumerable<float> vertices;
        public IEnumerable<byte> colors;
    }

    public static class GLHelper {
        private static Random random = new Random();

        public static IEnumerable<float> GenerateFloats(int count) {
            return Enumerable
                   .Range(0, count)
                   .Select(x => (float)(random.NextDouble() * 2.0 - 1.0));
        }

        public static IEnumerable<byte> GenerateColorSets(int count, ColorsGeneratorFlags colorOptions = ColorsGeneratorFlags.Default) {
            var v = Enumerable
                    .Range(0, count)
                    .SelectMany(x => new[] {
                        (byte)(colorOptions.HasFlag(ColorsGeneratorFlags.FixRed) ? 255 : random.Next(0, 255)),
                        (byte)(colorOptions.HasFlag(ColorsGeneratorFlags.FixGreen) ? 255 : random.Next(0, 255)),
                        (byte)(colorOptions.HasFlag(ColorsGeneratorFlags.FixBlue) ? 255 : random.Next(0, 255)),
                        (byte)(colorOptions.HasFlag(ColorsGeneratorFlags.FixAlpha) ? 255 : random.Next(0, 255))
                    });
            return v;
        }

        public static TrianglesSet GenerateTriangles(int count, ColorsGeneratorFlags colorOptions = ColorsGeneratorFlags.Default) {
            var vertices = GenerateFloats(6 * count);
            var colors = GenerateColorSets(3 * count, colorOptions);
            return new TrianglesSet { vertices = vertices, colors = colors };
        }

        public static float[] FillTrianglesVertices { get; } = {
            -1f, -1f,
            -1f, 1f,
            1f, -1f,
            -1f, 1f,
            1f, 1f,
            1f, -1f,
        };
    }
}
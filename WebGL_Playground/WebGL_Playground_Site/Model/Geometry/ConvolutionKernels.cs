using System;
using System.Collections.Generic;

namespace WebGL_Playground_Site.Geometry {
    public static class ConvolutionKernels {
        public static float[,] ArrayOfArraysToMatrix(float[] kernel, int width, int height) {
            if (kernel.Length < width * height) {
                throw new ArgumentOutOfRangeException();
            }
            float[,] matrix = new float[width, height];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    matrix[i, j] = kernel[j * height + i];
                }
            }

            return matrix;
        }

        public static readonly Dictionary<string, float[]> DefaultKernels = new Dictionary<string, float[]>() {
            {
                "normal",
                new float[] {
                    0, 0, 0,
                    0, 1, 0,
                    0, 0, 0
                }
            }, {
                "gaussianBlur",
                new float[] {
                    0.045f, 0.122f, 0.045f,
                    0.122f, 0.332f, 0.122f,
                    0.045f, 0.122f, 0.045f
                }
            }, {
                "gaussianBlur2",
                new float[] {
                    1, 2, 1,
                    2, 4, 2,
                    1, 2, 1
                }
            }, {
                "gaussianBlur3",
                new float[] {
                    0, 1, 0,
                    1, 1, 1,
                    0, 1, 0
                }
            }, {
                "unsharpen",
                new float[] {
                    -1, -1, -1,
                    -1,  9, -1,
                    -1, -1, -1
                }
            }, {
                "sharpness",
                new float[] {
                    0, -1, 0,
                    -1, 5, -1,
                    0, -1, 0
                }
            }, {
                "sharpen",
                new float[] {
                    -1, -1, -1,
                    -1, 16, -1,
                    -1, -1, -1
                }
            }, {
                "edgeDetect",
                new float[] {
                    -0.125f, -0.125f, -0.125f,
                    -0.125f,  1f,     -0.125f,
                    -0.125f, -0.125f, -0.125f
                }
            }, {
                "edgeDetect2",
                new float[] {
                    -1, -1, -1,
                    -1,  8, -1,
                    -1, -1, -1
                }
            }, {
                "edgeDetect3",
                new float[] {
                    -5, 0, 0,
                    0, 0, 0,
                    0, 0, 5
                }
            }, {
                "edgeDetect4",
                new float[] {
                    -1, -1, -1,
                    0,  0,  0,
                    1,  1,  1
                }
            }, {
                "edgeDetect5",
                new float[] {
                    -1, -1, -1,
                    2,  2,  2,
                    -1, -1, -1
                }
            }, {
                "edgeDetect6",
                new float[] {
                    -5, -5, -5,
                    -5, 39, -5,
                    -5, -5, -5
                }
            }, {
                "sobelHorizontal",
                new float[] {
                    1,  2,  1,
                    0,  0,  0,
                    -1, -2, -1
                }
            }, {
                "sobelVertical",
                new float[] {
                    1,  0, -1,
                    2,  0, -2,
                    1,  0, -1
                }
            }, {
                "previtHorizontal",
                new float[] {
                    1,  1,  1,
                    0,  0,  0,
                    -1, -1, -1
                }
            }, {
                "previtVertical",
                new float[] {
                    1,  0, -1,
                    1,  0, -1,
                    1,  0, -1
                }
            }, {
                "boxBlur",
                new float[] {
                    0.111f, 0.111f, 0.111f,
                    0.111f, 0.111f, 0.111f,
                    0.111f, 0.111f, 0.111f
                }
            }, {
                "triangleBlur",
                new float[] {
                    0.0625f, 0.125f, 0.0625f,
                    0.125f,  0.25f,  0.125f,
                    0.0625f, 0.125f, 0.0625f
                }
            }, {
                "emboss",
                new float[] {
                    -2, -1,  0,
                    -1,  1,  1,
                    0,  1,  2
                }
            }
        };

        public static readonly Dictionary<string, float[]> MyKernels = new Dictionary<string, float[]>() {
            {
                "v1",
                new float[] {
                    1,  2,  1,
                    0,  0,  0,
                    -1, -2, -1
                }
            }, {
                "v2",
                new float[] {
                    1,  0, -1,
                    2,  0, -2,
                    1,  0, -1
                }
            }, {
                "v3",
                new float[] {
                    -1, -2, -1,
                    0,  0,  0,
                    1,  2,  1,
                }
            }, {
                "v4",
                new float[] {
                    -1, 0,  1,
                    -2, 0,  2,
                    -1, 0,  1,
                }
            }, {
                "v4-1",
                new float[] {
                    1,  2,  1,
                    0,  0,  0,
                    -1, -2, -1
                }
            },
        };
    }
}
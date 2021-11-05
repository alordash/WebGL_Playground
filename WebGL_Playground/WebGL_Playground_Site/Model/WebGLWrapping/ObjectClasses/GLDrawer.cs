using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.WebGL;

namespace WebGL_Playground_Site.WebGLWrapping {
    public class GLDrawer : IGLObject {
        public GLProgram Program;
        public WebGLContext GL { get; set; }

        public GLDrawer(GLProgram glProgram) {
            GL = glProgram.GL;
            Program = glProgram;
        }

        public async Task ClearWithColor(float red, float green, float blue, float alpha) {
            await GL.ClearColorAsync(red, green, blue, alpha);
            await GL.ClearAsync(BufferBits.COLOR_BUFFER_BIT);
        }

        public async Task<uint> FillBuffer<T>(string locationName, int size, DataType dataType, bool normalize, T[] data) {
            var buffer = await GL.CreateBufferAsync();
            await GL.BindBufferAsync(BufferType.ARRAY_BUFFER, buffer);
            await GL.BufferDataAsync(BufferType.ARRAY_BUFFER, data, BufferUsageHint.STATIC_DRAW);

            var variableLocation = (uint)await GL.GetAttribLocationAsync(Program.Program, locationName);
            await GL.EnableVertexAttribArrayAsync(variableLocation);
            await GL.VertexAttribPointerAsync(variableLocation, size, dataType, normalize, 0, 0);
            return variableLocation;
        }

        public async Task DrawTriangles(float[] vertices, byte[] colors) {
            await FillBuffer("position", 2, DataType.FLOAT, false, vertices);

            await FillBuffer("color", 4, DataType.UNSIGNED_BYTE, true, colors);

            await GL.DrawArraysAsync(Primitive.TRIANGLES, 0, vertices.Length);
        }

        public async Task SetTime(float time) {
            var timeLoc = await GL.GetUniformLocationAsync(Program.Program, "u_time");
            await GL.UniformAsync(timeLoc, time);
        }

        public async Task InterpolateTriangles(float[] beginVertices, byte[] beginColors, float[] endVertices, byte[] endColors, float time) {
            if (beginVertices.Length != endVertices.Length || beginColors.Length != endColors.Length) {
                throw new Exception("Begin and end values have different lenght");
            }

            await FillBuffer("a_begin_position", 2, DataType.FLOAT, false, beginVertices);
            await FillBuffer("a_begin_color", 4, DataType.UNSIGNED_BYTE, true, beginColors);
            await FillBuffer("a_end_position", 2, DataType.FLOAT, false, endVertices);
            await FillBuffer("a_end_color", 4, DataType.UNSIGNED_BYTE, true, endColors);

            await SetTime(time);

            await GL.DrawArraysAsync(Primitive.TRIANGLES, 0, beginVertices.Length);
        }

        public async Task DrawVoronoiTesselation(float[] vertices, byte[] colors) {
            
        }
    }
}
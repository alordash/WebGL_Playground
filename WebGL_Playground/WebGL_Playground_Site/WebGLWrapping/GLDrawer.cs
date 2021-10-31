using System;
using System.Collections.Generic;
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

        public async Task DrawTriangles(float[] vertices, byte[] colors) {
            var positionBuffer = await GL.CreateBufferAsync();
            await GL.BindBufferAsync(BufferType.ARRAY_BUFFER, positionBuffer);
            await GL.BufferDataAsync(BufferType.ARRAY_BUFFER, vertices, BufferUsageHint.STATIC_DRAW);

            var positionLoc = (uint)await GL.GetAttribLocationAsync(Program.Program, "position");
            await GL.EnableVertexAttribArrayAsync(positionLoc);
            await GL.VertexAttribPointerAsync(positionLoc, 2, DataType.FLOAT, false, 0, 0);

            var colorBuffer = await GL.CreateBufferAsync();
            await GL.BindBufferAsync(BufferType.ARRAY_BUFFER, colorBuffer);
            await GL.BufferDataAsync(BufferType.ARRAY_BUFFER, colors, BufferUsageHint.STATIC_DRAW);

            var colorLoc = (uint)await GL.GetAttribLocationAsync(Program.Program, "color");
            await GL.EnableVertexAttribArrayAsync(colorLoc);
            await GL.VertexAttribPointerAsync(colorLoc, 4, DataType.UNSIGNED_BYTE, true, 0, 0);

            await GL.DrawArraysAsync(Primitive.TRIANGLES, 0, vertices.Length);

            await GL.DeleteBufferAsync(positionBuffer);
            await GL.DeleteBufferAsync(colorBuffer);
        }

        public async Task ClearWithColor(float red, float green, float blue, float alpha) {
            await GL.ClearColorAsync(red, green, blue, alpha);
            await GL.ClearAsync(BufferBits.COLOR_BUFFER_BIT);
		}
    }
}

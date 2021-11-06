using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.WebGL;
using Microsoft.JSInterop;
using DataType = Blazor.Extensions.Canvas.WebGL.DataType;

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

        public async Task<uint> FillBuffer<T>(string name, int size, DataType dataType, bool normalize, T[] data) {
            var buffer = await GL.CreateBufferAsync();
            await GL.BindBufferAsync(BufferType.ARRAY_BUFFER, buffer);
            await GL.BufferDataAsync(BufferType.ARRAY_BUFFER, data, BufferUsageHint.STATIC_DRAW);

            var variableLocation = (uint)await GL.GetAttribLocationAsync(Program.Program, name);
            await GL.EnableVertexAttribArrayAsync(variableLocation);
            await GL.VertexAttribPointerAsync(variableLocation, size, dataType, normalize, 0, 0);
            return variableLocation;
        }

        public async Task<WebGLUniformLocation> FillUniformF(string name, params float[] values) {
            var variableLocation = await GL.GetUniformLocationAsync(Program.Program, name);
            await GL.UniformAsync(variableLocation, values);
            return variableLocation;
        }

        public async Task<WebGLUniformLocation> FillUniformI(string name, params int[] data) {
            var variableLocation = await GL.GetUniformLocationAsync(Program.Program, name);
            await GL.UniformAsync(variableLocation, data);
            return variableLocation;
        }

        public async Task FillUniformArrayF(string name, float[] data, int step) {
            for (var i = 0; i < data.Length; i += step) {
                var values = data.Subsequence(i, step).ToArray();
                await FillUniformF($"{name}[{(i / step).ToString()}]", values);
            }
        }

        public async Task DrawBlankRectangle(bool prefill = true) {
            if (prefill) {
                await FillBuffer("a_position", 2, DataType.FLOAT, false, GLHelper.FillTrianglesVertices);
            }

            await GL.DrawArraysAsync(Primitive.TRIANGLES, 0, GLHelper.FillTrianglesVertices.Length);
        }

        public async Task DrawTriangles(float[] vertices, byte[] colors) {
            await FillBuffer("a_position", 2, DataType.FLOAT, false, vertices);

            await FillBuffer("a_color", 4, DataType.UNSIGNED_BYTE, true, colors);

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

        public async Task DrawVoronoiTesselation(MeshData begin, MeshData end, float time) {
            await SetTime(time);
            
            await FillUniformI("u_count", begin.Vertices.Length / 2);

            await FillUniformArrayF("u_begin_positions", begin.Vertices, 2);
            await FillUniformArrayF("u_end_positions", end.Vertices, 2);

            await FillUniformArrayF("u_colors", begin.Colors, 4);

            await DrawBlankRectangle();
        }

        // VIP
        public async Task DrawTexture(IJSRuntime JS, BECanvasComponent canvasReference) {
            await FillBuffer("a_texCoord", 2, DataType.FLOAT, false, GLHelper.FillTexturesVertices);
            var texture = await GL.CreateTextureAsync();
            await GL.BindTextureAsync(TextureType.TEXTURE_2D, texture);

            await GL.TexParameterAsync(TextureType.TEXTURE_2D, TextureParameter.TEXTURE_WRAP_S, (uint)TextureParameterValue.CLAMP_TO_EDGE);
            await GL.TexParameterAsync(TextureType.TEXTURE_2D, TextureParameter.TEXTURE_WRAP_T, (uint)TextureParameterValue.CLAMP_TO_EDGE);
            await GL.TexParameterAsync(TextureType.TEXTURE_2D, TextureParameter.TEXTURE_MIN_FILTER, (uint)TextureParameterValue.NEAREST);
            await GL.TexParameterAsync(TextureType.TEXTURE_2D, TextureParameter.TEXTURE_MAG_FILTER, (uint)TextureParameterValue.NEAREST);

            var values = new byte[] {
                255, 0, 0, 255,
                0, 255, 0, 255,
                0, 0, 255, 255,
                255, 255, 0, 255,
            };

            await GL.TexImage2DAsync(JS, canvasReference, Texture2DType.TEXTURE_2D, 0, PixelFormat.RGBA, 2, 2, 0, PixelFormat.RGBA, PixelType.UNSIGNED_BYTE, values);
        }
    }
}
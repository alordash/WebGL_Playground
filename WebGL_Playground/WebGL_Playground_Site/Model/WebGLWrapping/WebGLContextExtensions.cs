using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.WebGL;
using Microsoft.JSInterop;

namespace WebGL_Playground_Site.WebGLWrapping {
    public static class WebGLContextExtensions {
        public static async Task LinkProgramAsync(this WebGLContext gl, GLProgram glProgram) {
            await gl.LinkProgramAsync(glProgram.Program);
        }

        public static async Task UseProgramAsync(this WebGLContext gl, GLProgram glProgram) {
            await gl.UseProgramAsync(glProgram.Program);
        }

        public static async Task TexImage2DAsync<T>(this WebGLContext gl,
                                                    IJSRuntime js,
                                                    BECanvasComponent canvasReference,
                                                    Texture2DType target,
                                                    uint level,
                                                    PixelFormat internalFormat,
                                                    uint width,
                                                    uint height,
                                                    uint border,
                                                    PixelFormat format,
                                                    PixelType type,
                                                    T[] pixels)
            => await js.InvokeVoidAsync("glTexImage2D",
                                        canvasReference,
                                        target,
                                        level,
                                        internalFormat,
                                        width,
                                        height,
                                        border,
                                        format,
                                        type,
                                        pixels);

        public static async Task TexImage2DAsync<T>(this WebGLContext gl,
                                                    IJSRuntime js,
                                                    DotNetObjectReference<T> dotNetHelper,
                                                    BECanvasComponent canvasReference,
                                                    Texture2DType target,
                                                    uint level,
                                                    PixelFormat internalFormat,
                                                    PixelFormat format,
                                                    PixelType type,
                                                    string imagePath) where T : class
            => await js.InvokeVoidAsync("glImage",
                                        dotNetHelper,
                                        canvasReference,
                                        target,
                                        level,
                                        internalFormat,
                                        format,
                                        type,
                                        imagePath);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.WebGL;

namespace WebGL_Playground_Site.WebGLWrapping {
	public static class WebGLContextExtensions {
		public static async Task LinkProgramAsync(this WebGLContext gl, GLProgram glProgram) {
			await gl.LinkProgramAsync(glProgram.Program);
		}

		public static async Task UseProgramAsync(this WebGLContext gl, GLProgram glProgram) {
			await gl.UseProgramAsync(glProgram.Program);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.WebGL;

namespace WebGL_Playground_Site.WebGLWrapping {
	public class GLProgram : IGLObject {
		public WebGLProgram Program;
		public WebGLContext GL { get; set; }

		public static async Task<GLProgram> CreateProgram() {
			return new GLProgram(GLContext.GL, await GLContext.GL.CreateProgramAsync());
		}
		public static async Task<GLProgram> CreateProgram(GLShader glVertShader, GLShader glFragShader) {
			var Program = await CreateProgram();
			await Program.AttachShader(glVertShader);
			await Program.AttachShader(glFragShader);
			return Program;
		}

		private GLProgram(WebGLContext gl, WebGLProgram program) {
			GL = gl;
			Program = program;
		}

		public async Task AttachShader(GLShader glShader) {
			await GL.AttachShaderAsync(Program, glShader.Shader);
		}

		public async Task<T> GetParameter<T>(ProgramParameter programParameter) {
			return await GL.GetProgramParameterAsync<T>(Program, programParameter);
		}

		public async Task<bool> IsLinked() {
			return await GetParameter<bool>(ProgramParameter.LINK_STATUS);
		}

		public async Task<string> GetInfoLog() {
			return await GL.GetProgramInfoLogAsync(Program);
		}

		public async Task Delete() {
			await GL.DeleteProgramAsync(Program);
		}

		public static async Task<GLProgram> SetUpProgram(string vertShaderSource, string fragShaderSource) {
			var vertShader = await GLShader.SetUpShader(ShaderType.VERTEX_SHADER, vertShaderSource);
			var fragShader = await GLShader.SetUpShader(ShaderType.FRAGMENT_SHADER, fragShaderSource);

			var glProgram = await CreateProgram(vertShader, fragShader);
			await GLContext.GL.LinkProgramAsync(glProgram);
			if(await glProgram.IsLinked()) {
				return glProgram;
			}

			var infoLog = await glProgram.GetInfoLog();
			await glProgram.Delete();
			throw new Exception($"Error linking program, info: {infoLog}");
		}
	}
}

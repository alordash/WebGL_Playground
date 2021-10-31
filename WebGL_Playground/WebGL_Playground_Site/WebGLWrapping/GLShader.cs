using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.WebGL;

namespace WebGL_Playground_Site.WebGLWrapping {
	public class GLShader : IGLObject {
		public WebGLShader Shader;
		public WebGLContext GL { get; set; }

		public static async Task<GLShader> CreateShader(ShaderType type) {
			return new GLShader(await GLContext.GL.CreateShaderAsync(type));
		}
		public static async Task<GLShader> CreateShader(ShaderType type, string source) {
			var Shader = await CreateShader(type);
			await Shader.SetSource(source);
			return Shader;
		}

		private GLShader(WebGLShader shader) {
			GL = GLContext.GL;
			Shader = shader;
		}

		public async Task SetSource(string source) {
			await GL.ShaderSourceAsync(Shader, source);
		}

		public async Task Compile() {
			await GL.CompileShaderAsync(Shader);
		}

		public async Task<T> GetParameter<T>(ShaderParameter shaderParameter) {
			return await GL.GetShaderParameterAsync<T>(Shader, shaderParameter);
		}

		public async Task<bool> IsComplied() {
			return await GetParameter<bool>(ShaderParameter.COMPILE_STATUS);
		}

		public async Task<string> GetInfoLog() {
			return await GL.GetShaderInfoLogAsync(Shader);
		}

		public async Task Delete() {
			await GL.DeleteShaderAsync(Shader);
		}

		public static async Task<GLShader> SetUpShader(ShaderType type, string source) {
			var glShader = await CreateShader(type, source);
			await glShader.Compile();
			if(await glShader.IsComplied()) {
				return glShader;
			}

			var infoLog = await glShader.GetInfoLog();
			await glShader.Delete();
			throw new Exception($"Error compiling shader, info: {infoLog}");
		}
	}
}

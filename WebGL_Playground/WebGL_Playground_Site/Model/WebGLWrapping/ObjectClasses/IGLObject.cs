using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.WebGL;

namespace WebGL_Playground_Site.WebGLWrapping {
	interface IGLObject {
		public WebGLContext GL { get; set; }
	}
}

﻿precision highp float;

uniform sampler2D u_image;

varying vec2 v_texCoord;

//varying vec4 v_color;

void main() {
//    gl_FragColor = v_color;
	gl_FragColor = texture2D(u_image, v_texCoord);
}
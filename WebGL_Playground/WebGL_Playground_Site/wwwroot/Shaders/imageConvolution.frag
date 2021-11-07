precision mediump float;

uniform sampler2D u_image;
uniform vec2 u_textureSize;
uniform float u_kernel[9];
uniform float u_kernelWeight;

varying vec2 v_texCoord;

void main() {
    gl_FragColor = texture2D(u_image, v_texCoord);
}
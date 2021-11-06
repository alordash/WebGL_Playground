attribute vec4 a_position;
attribute vec2 a_texCoord;

varying vec4 v_color;

void main() {
    v_color = gl_Position = a_position;
}
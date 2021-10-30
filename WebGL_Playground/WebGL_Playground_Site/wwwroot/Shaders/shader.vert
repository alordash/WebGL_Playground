attribute vec4 position;
attribute vec4 color;

varying vec4 v_color;

void main() {
    gl_Position = position;
    v_color = color;
}
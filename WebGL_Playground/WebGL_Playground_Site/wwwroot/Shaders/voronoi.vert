attribute vec4 a_position;

varying vec4 v_position;

void main() {
    v_position = gl_Position = a_position;
}
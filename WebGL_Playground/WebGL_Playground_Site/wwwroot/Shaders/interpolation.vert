attribute vec4 a_begin_position;
attribute vec4 a_begin_color;
attribute vec4 a_end_position;
attribute vec4 a_end_color;

uniform float u_time;

varying vec4 v_color;

void main() {
    gl_Position = a_begin_position + (a_end_position - a_begin_position) * u_time;
    v_color = a_begin_color + (a_end_color - a_begin_color) * u_time;
}
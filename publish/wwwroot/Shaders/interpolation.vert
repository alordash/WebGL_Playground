attribute vec4 a_begin_position;
attribute vec4 a_begin_color;
attribute vec4 a_end_position;
attribute vec4 a_end_color;

uniform float u_time;

varying vec4 v_color;

vec2 Interpolate(vec2 begin, vec2 end, float t) {
    return begin + (end - begin) * t;
}

vec4 Interpolate(vec4 begin, vec4 end, float t) {
    return begin + (end - begin) * t;
}

void main() {
    gl_Position = Interpolate(a_begin_position, a_end_position, u_time);
    v_color = Interpolate(a_begin_color, a_end_color, u_time);
}
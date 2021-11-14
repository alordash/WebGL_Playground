precision highp float;

const int max_iters = 200;

uniform bool u_highlight;

uniform float u_time;

uniform int u_count;
uniform vec2 u_begin_positions[max_iters];
uniform vec2 u_end_positions[max_iters];
uniform vec4 u_colors[max_iters];

varying vec2 v_position;

const float threshold = 0.01;

vec4 HighlightColor(vec4 color) {
    return vec4(1. - color.r, 1. - color.g, 1. - color.b, color.a);
}

vec2 Interpolate(vec2 begin, vec2 end, float t) {
    return begin + (end - begin) * t;
}

void main() {
    bool highlight = false;
    vec4 color = u_colors[0];
    float minDst = 10033.;
    for(int i = 0; i < max_iters; i++) {
        if(i >= u_count) {
            break;
        }
        vec2 p = Interpolate(u_begin_positions[i], u_end_positions[i], u_time);
        float dst = distance(v_position, p);
        if(u_highlight && dst < threshold) {
            highlight = true;
        }
        if(dst < minDst) {
            minDst = dst;
            color = u_colors[i];
        }
    }

    if(highlight) {
        color = HighlightColor(color) * 3.;
    }
    gl_FragColor = color;
}
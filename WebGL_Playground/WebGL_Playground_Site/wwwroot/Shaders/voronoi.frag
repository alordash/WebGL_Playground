precision highp float;

const int max_iters = 200;

uniform bool u_highlight;

uniform int u_count;
uniform vec2 u_positions[max_iters];
uniform vec4 u_colors[max_iters];

varying vec4 v_position;

const float threshold = 0.01;

vec4 HighlightColor(vec4 color) {
//    float max = color.r;
//    if(color.g > max) {
//        max = color.y;
//    }
//    if(color.b > max) {
//        max = color.b;
//    }
//    if(color.r >= max) {
//        color.r = 0.;
//    }
//    if(color.g >= max) {
//        color.g = 0.;
//    }
//    if(color.b >= max) {
//        color.b = 0.;
//    }
//    return color;
    return vec4(1. - color.r, 1. - color.g, 1. - color.b, color.a);
}

void main() {
    bool highlight = false;
    vec4 color = u_colors[0];
    float minDst = 10033.;
    for(int i = 0; i < max_iters; i++) {
        if(i >= u_count) {
            break;
        }
        vec2 p = u_positions[i];
        float dst = distance(v_position.xy, p);
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
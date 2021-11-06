precision highp float;

const int max_iters = 200;

uniform int u_count;
uniform vec2 u_positions[max_iters];
uniform vec4 u_colors[max_iters];

varying vec4 v_position;

const float threshold = 0.01;

void main() {
    vec4 color = u_colors[0];
    float minDst = 10033.;
    for(int i = 0; i < max_iters; i++) {
        if(i >= u_count) {
            break;
        }
        vec2 p = u_positions[i];
        float dst = distance(v_position.xy, p);
        if(dst < threshold) {
            color = vec4(0., 0., 0., 1.);
            break;
        }
        if(dst < minDst) {
            minDst = dst;
            color = u_colors[i];
        }
    }

    gl_FragColor = color;
}
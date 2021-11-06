precision highp float;

const int max_iters = 200;

uniform int u_count;
uniform vec2 u_positions[max_iters];
uniform vec4 u_colors[max_iters];

float rand(vec2 co){
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}

void main() {
    gl_FragColor = vec4(rand(v_color.xy), rand(v_color.xy), 0.2, 1);
    /*
    int index = 0;
    float minDst = 100.;
    for(int i = 0; i < max_iters; i++) {
        if(i >= u_count) {
            break;
        }
        vec4 p = vec4(u_positions[i], 0., 0.);
        float dst = distance(a_position, p);
        if(dst < minDst) {
            minDst = dst;
            index = i;
        }
    }

    gl_Position = a_position;
    v_color = u_colors[index];*/
}
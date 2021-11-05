attribute vec4 a_position;
attribute vec2 a_texCoord;

varying vec2 v_texCoord;

const int max_iters = 200;

uniform int u_count;
uniform vec2 u_positions[max_iters];
uniform vec4 u_colors[max_iters];

varying vec4 v_color;

void main() {
    v_texCoord = a_texCoord;
    gl_Position = a_position;
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
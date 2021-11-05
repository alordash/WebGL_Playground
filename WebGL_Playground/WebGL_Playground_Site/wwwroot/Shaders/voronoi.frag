precision highp float;

uniform vec2 u_positions[8];
uniform vec4 u_colors[16];

void main() {
    gl_FragColor = vec4(0.5, 0.5, 0.5, 1) + vec4(u_positions[0], 4, 4) + u_colors[0];
}
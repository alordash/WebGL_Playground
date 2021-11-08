precision mediump float;

#define OFFSET i * 9

uniform sampler2D u_image;
uniform vec2 u_textureSize;

uniform float u_time;
uniform float u_k;
uniform bool u_showOriginal;

const int max_iter = 100;

uniform float u_kernels[9 * max_iter];

varying vec2 v_texCoord;

float Interpolate(int i) {
    return max(0., (1. / u_k) - abs((float(i) / u_k) - u_time)) * u_k;
}

void main() {
    if(u_showOriginal) {
        gl_FragColor = texture2D(u_image, v_texCoord);
        return;
    }
    vec2 onePixel = vec2(1.0, 1.0) / u_textureSize;
    vec4 colorSum = vec4(0., 0., 0., 0.);
    float v = u_time * u_k;
    int iMax = int(max(1., ceil(v)));
    int iMin = iMax - 1;
    for (int i = 0; i < max_iter; i++) {
        if(i < iMin) {
            continue;
        }
        if (i > iMax) {
            break;
        }
        float m = Interpolate(i);
        float v1 = u_kernels[OFFSET];
        float v2 = u_kernels[OFFSET + 1];
        float v3 = u_kernels[OFFSET + 2];
        float v4 = u_kernels[OFFSET + 3];
        float v5 = u_kernels[OFFSET + 4];
        float v6 = u_kernels[OFFSET + 5];
        float v7 = u_kernels[OFFSET + 6];
        float v8 = u_kernels[OFFSET + 7];
        float v9 = u_kernels[OFFSET + 8];
        vec4 iColorSum = 
        m * texture2D(u_image, v_texCoord + onePixel * vec2(-1, -1)) * v1 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(0, -1)) * v2 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(1, -1)) * v3 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(-1, 0)) * v4 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(0, 0)) * v5 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(1, 0)) * v6 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(-1, 1)) * v7 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(0, 1)) * v8 +
        m * texture2D(u_image, v_texCoord + onePixel * vec2(1, 1)) * v9;
        
        float kernelWeight = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9;
        if(kernelWeight <= 0.) {
            kernelWeight = 1.;
        }

        colorSum += iColorSum / kernelWeight;
    }

    gl_FragColor = vec4(colorSum.rgb, 1);
}
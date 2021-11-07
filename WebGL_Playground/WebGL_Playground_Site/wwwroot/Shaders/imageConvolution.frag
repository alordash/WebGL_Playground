precision mediump float;

#define OFFSET i * 9

uniform sampler2D u_image;
uniform vec2 u_textureSize;

uniform float u_time;

const int k = 9;
const float fk = float(k);
const int max_iter = 100;

uniform float u_kernels[k * max_iter];

varying vec2 v_texCoord;

float Interpolate(int i) {
    return max(0., (1. / fk) - abs((float(i) / fk) - u_time)) * fk;
}

void main() {
    vec2 onePixel = vec2(1.0, 1.0) / u_textureSize;
    vec4 colorSum = vec4(0., 0., 0., 0.);
    float v = u_time * fk;
    int iMin = int(floor(v));
    int iMax = int(max(1., ceil(v))) + 1;
    for (int i = 0; i < max_iter; i++) {
        if(i < iMin) {
            continue;
        }
        if (i >= iMax) {
            break;
        }
        float m = Interpolate(i);
        float v1 = m * u_kernels[OFFSET];
        float v2 = m * u_kernels[OFFSET + 1];
        float v3 = m * u_kernels[OFFSET + 2];
        float v4 = m * u_kernels[OFFSET + 3];
        float v5 = m * u_kernels[OFFSET + 4];
        float v6 = m * u_kernels[OFFSET + 5];
        float v7 = m * u_kernels[OFFSET + 6];
        float v8 = m * u_kernels[OFFSET + 7];
        float v9 = m * u_kernels[OFFSET + 8];
        vec4 iColorSum = 
        texture2D(u_image, v_texCoord + onePixel * vec2(-1, -1)) * v1 +
        texture2D(u_image, v_texCoord + onePixel * vec2(0, -1)) * v2 +
        texture2D(u_image, v_texCoord + onePixel * vec2(1, -1)) * v3 +
        texture2D(u_image, v_texCoord + onePixel * vec2(-1, 0)) * v4 +
        texture2D(u_image, v_texCoord + onePixel * vec2(0, 0)) * v5 +
        texture2D(u_image, v_texCoord + onePixel * vec2(1, 0)) * v6 +
        texture2D(u_image, v_texCoord + onePixel * vec2(-1, 1)) * v7 +
        texture2D(u_image, v_texCoord + onePixel * vec2(0, 1)) * v8 +
        texture2D(u_image, v_texCoord + onePixel * vec2(1, 1)) * v9;
        
        float kernelWeight = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9;
        if(kernelWeight <= 0.) {
            kernelWeight = 1.;
        }

        colorSum += iColorSum / kernelWeight;
    }

    gl_FragColor = vec4(colorSum.rgb, 1);
}
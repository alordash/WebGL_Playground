window.glTexImage2D = async (canvas, target, level, internalFormat, width, height, border, format, type, pixels) => {
    console.log(JSON.stringify(canvas));
    let gl = canvas.canvasReference.getContext("webgl");
    let values = new Uint8Array(pixels);
    await gl.texImage2D(target, level, internalFormat, width, height, border, format, type, values);
    return JSON.stringify(values);
};
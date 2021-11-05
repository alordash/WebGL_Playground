window.StringToUint8Array = (str) => {
    str = atob(str);
    console.log(`atob(str) = "${str}"`);
    let arr = [];
    for(let i = 0; i < str.length; i++) {
        arr[i] = str.charCodeAt(i);
    }
    return new Uint8Array(arr);
};

window.glTexImage2D = async (canvas, target, level, internalFormat, width, height, border, format, type, pixels) => {
    console.log(`pixels = ${JSON.stringify(pixels)}`);
    let gl = canvas.canvasReference.getContext("webgl");
    let values = typeof(pixels) == "string" ? StringToUint8Array(pixels) : new Uint8Array(pixels);
    await gl.texImage2D(target, level, internalFormat, width, height, border, format, type, values);
    console.log(`values = ${values}`);
};
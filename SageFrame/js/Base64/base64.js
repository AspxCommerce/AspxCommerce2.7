/*jslint indent: 2, maxlen: 80 */
"use strict";

var b64c = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

/**
* btoa(data): String
*
* Base64 encode a binary string (all char codes must be < 255).
*
* @param  {String} data The data to convert
* @return {String} The base64 binary
*/
function btoa(data) {
    /*jslint bitwise: true */
    var i, res = "", length = data.length;
    for (i = 0; i < length - 2; i += 3) {
        res += b64c[data.charCodeAt(i) >>> 2];
        res += b64c[((data.charCodeAt(i) & 3) << 4) |
                (data.charCodeAt(i + 1) >>> 4)];
        res += b64c[((data.charCodeAt(i + 1) & 15) << 2) |
                (data.charCodeAt(i + 2) >>> 6)];
        res += b64c[data.charCodeAt(i + 2) & 63];
    }
    if (length % 3 === 2) {
        res += b64c[data.charCodeAt(i) >>> 2];
        res += b64c[((data.charCodeAt(i) & 3) << 4) |
                (data.charCodeAt(i + 1) >>> 4)];
        res += b64c[((data.charCodeAt(i + 1) & 15) << 2)];
        res += "=";
    } else if (length % 3 === 1) {
        res += b64c[data.charCodeAt(i) >>> 2];
        res += b64c[((data.charCodeAt(i) & 3) << 4)];
        res += "==";
    }

    return res;
}


/**
* atob(data): Array
*
* Converts a base64 string into a binary string.
*
* @param  {String} data The data to convert
* @return {String} The clear binary
*/
function atob(data) {
    /*jslint bitwise: true */
    var i, res = "", buf = [], length = data.length;
    for (i = 0; i < length - 3; i += 4) {
        buf[0] = b64c.indexOf(data[i]);
        buf[1] = b64c.indexOf(data[i + 1]);
        buf[2] = b64c.indexOf(data[i + 2]);
        buf[3] = b64c.indexOf(data[i + 3]);

        res += String.fromCharCode((buf[0] << 2) | (buf[1] >>> 4));
        if (data[i + 2] !== "=") {
            res += String.fromCharCode(((buf[1] << 4) & 0xF0) |
                                 ((buf[2] >>> 2) & 0x0F));
        }
        if (data[i + 3] !== "=") {
            res += String.fromCharCode(((buf[2] << 6) & 0xC0) | buf[3]);
        }
    }

    return res;
}

//////////////////////////////////////////////////

/**
* btoa(data): String
*
* Stringifies an array of number into base64 string.
*
* @param  {Array} data The data to convert
* @return {String} The base64 binary
*/
function btoa(data) {
    /*jslint bitwise: true */
    var i, res = "", length = data.length;
    for (i = 0; i < length - 2; i += 3) {
        res += b64c[data[i] >>> 2];
        res += b64c[((data[i] & 3) << 4) | (data[i + 1] >>> 4)];
        res += b64c[((data[i + 1] & 15) << 2) | (data[i + 2] >>> 6)];
        res += b64c[data[i + 2] & 63];
    }
    if (length % 3 === 2) {
        res += b64c[data[i] >>> 2];
        res += b64c[((data[i] & 3) << 4) | (data[i + 1] >>> 4)];
        res += b64c[((data[i + 1] & 15) << 2)];
        res += "=";
    } else if (length % 3 === 1) {
        res += b64c[data[i] >>> 2];
        res += b64c[((data[i] & 3) << 4)];
        res += "==";
    }

    return res;
}

/**
* atob(data): Array
*
* Converts a base64 string into an array of number.
*
* @param  {String} data The data to convert
* @return {Array} The clear binary
*/
function atob(data) {
    /*jslint bitwise: true */
    var i, res = [], buf = [], length = data.length;
    for (i = 0; i < length - 3; i += 4) {
        buf[0] = b64c.indexOf(data[i]);
        buf[1] = b64c.indexOf(data[i + 1]);
        buf[2] = b64c.indexOf(data[i + 2]);
        buf[3] = b64c.indexOf(data[i + 3]);

        res[res.length] = (buf[0] << 2) | (buf[1] >>> 4);
        if (data[i + 2] !== "=") {
            res[res.length] = ((buf[1] << 4) & 0xF0) | ((buf[2] >>> 2) & 0x0F);
        }
        if (data[i + 3] !== "=") {
            res[res.length] = ((buf[2] << 6) & 0xC0) | buf[3];
        }
    }

    return res;
}
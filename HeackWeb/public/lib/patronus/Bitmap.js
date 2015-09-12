/*
* Bitmap
*
* Copyright (c) 2015 Mahardiansyah Kartika.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy 
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
* copies of the Software, and to permit persons to whom the Software is 
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

/**
 * @module Patronus
 **/

// namespace:
this.patronus = this.patronus || {};

(function() {
    "use strict";

    /** 
     * @class Bitmap
     * @constructor
     * @extends createjs.Bitmap
    **/
    var Bitmap = function(imageOrUri) {
        this.Bitmap_constructor(imageOrUri);

        this.anchorX = 0;
        this.anchorY = 0;
    }
    var p = createjs.extend(Bitmap, createjs.Bitmap);

    p.setPosition = function(x, y) {
        this.x = x;
        this.y = y;
    }

    p.getPosition = function() {
        return {x: this.x, y: this.y};
    }

    p.setScale = function(x, y) {
        this.scaleX = x;
        this.scaleY = y;
    }

    p.getScale = function() {
        return {x: this.scaleX, y: this.scaleY};
    }

    p.setAlpha = function(a) {
        this.alpha = a;
    }

    p.getAlpha = function() {
        return this.alpha;
    }

    p.setAnchorPoint = function(x, y) {
        this.regX = this.image.width * x;
        this.regY = this.image.height * y;
        this.anchorX = x;
        this.anchorY = y;
    }

    p.getAnchorPoint = function() {
        return {x: this.anchorX, y: this.anchorY};
    }

    patronus.Bitmap = createjs.promote(Bitmap, "Bitmap");
}());

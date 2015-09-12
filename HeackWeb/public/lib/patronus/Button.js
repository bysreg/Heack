/*
* Button
*
* Copyright (c) 2015 Mahardiansyah Kartika <mkartika@andrew.cmu.edu>.
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
     * @class Button
     * @constructor
     * @extends patnonus.Bitmap
    **/
    var Button = function(imageOrUri, imageOrUriHover) {
        this.patronusBitmap_constructor(imageOrUri);

        this.imageNormal = imageOrUri;
        this.imageHover = imageOrUriHover;

        //this.shadow = new createjs.Shadow("#000000", 6, 6, 10);

        this.hover = false;
        this.tween = null;
        this.cursor = "pointer";
        this.addEventListener("rollover", this);
        this.addEventListener("rollout", this);
    }
    var p = createjs.extend(Button, patronus.Bitmap);

    // set up the handlers for mouseover / out:
    p.handleEvent = function (evt) {
        this.hover = (evt.type == "rollover");

        if (this.hover) {
            this.onHover(evt);
        } else {
            this.onOut(evt);
        }            
    }

    p.onHover = function(evt) {
        this.image = this.imageHover;
    }

    p.onOut = function(evt) {
        this.image = this.imageNormal;
    }

    patronus.Button = createjs.promote(Button, "patronusBitmap");
}());
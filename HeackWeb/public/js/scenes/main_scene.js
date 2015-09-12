// namespace:
this.patronus = this.patronus || {};

(function() {
    "use strict";
    
    /** 
     * A Scene is a general container for display objects.
     * @constructor
     * @extends patronus.GameScene
    **/
	var main_scene = function() {
		this.GameScene_constructor();
		
		// ATTRIBUTES
		var _this = this;

		// left button
		this.leftBtn = new patronus.Button('img/arrow.png');
		this.leftBtn.image.onload = function() {_this.leftBtn.setAnchorPoint(.5, .5);};
		this.leftBtn.setPosition(canvas.width/2 - 450, canvas.height/2);
		this.leftBtn.setRotation(180);
        this.leftBtn.on("click", function(event) {
        	var msg = {
				"type": "move",
				"dir": "left"
			};
	        conn.sendMessage(msg, 0);
        }, this);
		this.addChild(this.leftBtn);

		// right button
		this.rightBtn = new patronus.Button('img/arrow.png');
		this.rightBtn.image.onload = function() {_this.rightBtn.setAnchorPoint(.5, .5);};
		this.rightBtn.setPosition(canvas.width/2 + 450, canvas.height/2);
        this.rightBtn.on("click", function(event) {
        	var msg = {
				"type": "move",
				"dir": "right"
			};
	        conn.sendMessage(msg, 0);
        }, this);
		this.addChild(this.rightBtn);

		this.generateCircle();
	}
	var p = createjs.extend(main_scene, patronus.GameScene);

	// FUNCTION & PROCEDURES
	p.generateCircle = function() {
		var circle = new createjs.Shape();
		circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, 50);
		circle.x = 100;
		circle.y = 100;

		this.addChild(circle);
	}

	patronus.main_scene = createjs.promote(main_scene, "GameScene");
}());
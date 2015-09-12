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
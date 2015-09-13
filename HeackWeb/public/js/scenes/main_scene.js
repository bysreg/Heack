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
		// background
        var background = new createjs.Shape();
        background.graphics.beginFill("#4b4b4b").drawRect(0, 0, canvas.width, canvas.height);
        this.addChild(background);

		// face
		var faceIdx = 1;
		var face = new patronus.Bitmap('img/face_icon/player' + faceIdx + '.png');
		face.image.onload = function() {
			face.setAnchorPoint(.5, .5)
			face.setPosition(canvas.width/2, canvas.height/2);
			face.setScale(15);
		}
		this.addChild(face);
	}
	var p = createjs.extend(main_scene, patronus.GameScene);

	// FUNCTION & PROCEDURES
    // override function
    p._tick = function(evtObj) {
        this.GameScene__tick(evtObj);

        // update player position
        if (isGoToEditor) {
        	isGoToEditor = false;
        	// go to editor
			getSceneManager().replace(new patronus.editor_scene(), new creatine.transitions.FadeIn(null, transitionTime));
        }
    }

	patronus.main_scene = createjs.promote(main_scene, "GameScene");
}());
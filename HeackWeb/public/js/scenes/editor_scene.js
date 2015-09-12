// namespace:
this.patronus = this.patronus || {};

(function() {
    "use strict";
    
    /** 
     * A Scene is a general container for display objects.
     * @constructor
     * @extends patronus.GameScene
    **/
	var editor_scene = function() {
		this.GameScene_constructor();
		
		// ATTRIBUTES
		this.timer = this.generateTimer();
		this.generateTiles();

		// start timer
		this.startTimer(30000);
	}
	var p = createjs.extend(editor_scene, patronus.GameScene);

	// FUNCTION & PROCEDURES
	p.generateTimer = function() {
		var timer = new patronus.Bitmap('img/timer_bar.png');
		timer.image.onload = function() {
			timer.setPosition(canvas.width/2 - timer.image.width/2, 40);
		}
		this.addChild(timer);

		return timer;
	}

	p.startTimer = function(totalTime) {
		createjs.Tween.get(this.timer, {loop: false})
			.to({scaleX: 0}, totalTime, createjs.Ease.linear)
			.call(function() {
				console.log("time is running out!");
			});
	}

	p.generateSingleTile = function(tile_name) {
		var tile = new patronus.Bitmap('img/tiles/' + tile_name + '.jpg');
		this.addChild(tile);

		return tile;
	}

	p.generateTiles = function() {
		var totalWidth = 16;
		var totalHeight = 10;
		var scale = 0.9;
		var firstPos = {"x": (canvas.width - (totalWidth * 100 * scale)) / 2, "y": canvas.height - 40 - (totalHeight * 100 * scale)}

		for (var i = 0; i < totalWidth; i++) {
			for (var j = 0; j < totalHeight; j++) {
				var no = 5;
				if (i == 0 && j == 0) no = 1;
				else if (i == totalWidth-1 && j == 0) no = 3;
				else if (i == 0 && j == totalHeight-1) no = 7;
				else if (i == totalWidth-1 && j == totalHeight-1) no = 9;
				else if (i == 0) no = 4;
				else if (i == totalWidth-1) no = 6;
				else if (j == 0) no = 2;
				else if (j == totalHeight-1) no = 8;

				var tile = this.generateSingleTile("tile_" + no);
				tile.setPosition(firstPos.x + (i * 100*scale), firstPos.y + (j * 100*scale));
				tile.setScale(scale);
				this.addChild(tile);
			}
		}
	}

	patronus.editor_scene = createjs.promote(editor_scene, "GameScene");
}());
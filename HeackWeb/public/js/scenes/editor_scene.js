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
		this.generateTiles();
	}
	var p = createjs.extend(editor_scene, patronus.GameScene);

	// FUNCTION & PROCEDURES
	p.generateSingleTile = function(tile_name) {
		var tile = new patronus.Bitmap('img/tiles/' + tile_name + '.jpg');
		this.addChild(tile);

		return tile;
	}

	p.generateTiles = function() {
		var firstPos = {"x": 880, "y": 40}

		for (var i = 0; i < 10; i++) {
			for (var j = 0; j < 10; j++) {
				var no = 5;
				if (i == 0 && j == 0) no = 1;
				else if (i == 9 && j == 0) no = 3;
				else if (i == 0 && j == 9) no = 7;
				else if (i == 9 && j == 9) no = 9;
				else if (i == 0) no = 4;
				else if (i == 9) no = 6;
				else if (j == 0) no = 2;
				else if (j == 9) no = 8;

				var tile = this.generateSingleTile("tile_" + no);
				tile.setPosition(firstPos.x + (i * 100), firstPos.y + (j * 100));
				this.addChild(tile);
			}
		}
	}

	patronus.editor_scene = createjs.promote(editor_scene, "GameScene");
}());
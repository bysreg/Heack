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
		this.totalWidth = 14;
		this.totalHeight = 10;
		this.scale = 0.9;
		this._size = 100;

		// left top position for tiles and character
		this.firstPos = {"x": (canvas.width - (this.totalWidth * this._size * this.scale)) / 2, "y": canvas.height - 40 - (this.totalHeight * this._size * this.scale)};

		this.timer = this.generateTimer();
		this.generateTiles();

		// start timer
		this.startTimer(30000);

		// players
		this.players = this.generatePlayers();
	}
	var p = createjs.extend(editor_scene, patronus.GameScene);

	// FUNCTION & PROCEDURES
    // override function
    p._tick = function(evtObj) {
        this.GameScene__tick(evtObj);

        // update player position
        if (playerPositions != null) {
        	var posBytes = [];
        	posBytes[0] = (playerPositions >> 24) & 0xff;
        	posBytes[1] = (playerPositions >> 16) & 0xff;
        	posBytes[2] = (playerPositions >> 8) & 0xff;
        	posBytes[3] = playerPositions & 0xff;

        	var pos = [];
	        for (var i = 0; i < 4; i++) {
	        	// get normal position from byte
	        	pos[i] = this.convertByteToPos(posBytes[i]);
	        	
	        	// set player position
	        	var realPos = this.convertUnityPosToCanvasPos(pos[i]);
	        	this.players[i].setPosition(realPos.x, realPos.y);
	        }
        }
    }

    p.convertUnityPosToCanvasPos = function(unityPos) {
    	var oneBlockSize = this._size * this.scale;

    	var posX = this.firstPos.x + (((this.totalWidth - 1) - unityPos.x) * oneBlockSize);
    	var posY = this.firstPos.y + (((this.totalHeight - 1) - unityPos.y) * oneBlockSize);

    	return {"x": posX, "y": posY};
    }

    p.convertByteToPos = function(byteVal) {
    	var byteX = (byteVal >> 4) & 0x0f;
    	var byteY = byteVal & 0x0f;

    	return {"x": byteX - 1, "y": byteY - 1};
    }

    p.generatePlayers = function() {
    	var players = [];

    	for (var i = 0; i < 4; i++) {
    		var player = new patronus.Bitmap('img/face_icon/player' + (i + 1) + '.png');
    		this.addChild(player);

    		players.push(player);
    	}

    	return players;
    }

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
		for (var i = 0; i < this.totalWidth; i++) {
			for (var j = 0; j < this.totalHeight; j++) {
				var no = 5;
				if (i == 0 && j == 0) no = 1;
				else if (i == this.totalWidth-1 && j == 0) no = 3;
				else if (i == 0 && j == this.totalHeight-1) no = 7;
				else if (i == this.totalWidth-1 && j == this.totalHeight-1) no = 9;
				else if (i == 0) no = 4;
				else if (i == this.totalWidth-1) no = 6;
				else if (j == 0) no = 2;
				else if (j == this.totalHeight-1) no = 8;

				var tile = this.generateSingleTile("tile_" + no);
				tile.setPosition(this.firstPos.x + (i * this._size * this.scale), this.firstPos.y + (j * this._size * this.scale));
				tile.setScale(this.scale);
				this.addChild(tile);
			}
		}
	}

	patronus.editor_scene = createjs.promote(editor_scene, "GameScene");
}());
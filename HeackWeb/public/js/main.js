var canvas = null;
var stage = null;
var sceneManager = null;

var playerPositions = null;
var editorTotalTime = 30000;
var isGoToEditor = false;
var isSpawnSignal = false;

function init() {
	canvas = document.getElementById('application-canvas');
	stage = new createjs.Stage(canvas);
	sceneManager = new patronus.SceneManager(stage);

	createjs.Ticker.setFPS(60);
	createjs.Ticker.addEventListener('tick', onTick);
	
	// first scene
	sceneManager.push(new patronus.main_scene());

	init_device_orientation();
}

function getSceneManager() {
	return sceneManager;
}

function onTick(event) {
	// update
	stage.update(event);
}
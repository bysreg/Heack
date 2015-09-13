$(document).ready(function () {
	console.log("Document Loaded");

	// INIT..
	conn = new Connection();
	conn.sendMessage({"type": "connect"});

	// Process incoming game messages
	$(document).on("game_message", function (e, message) {
		console.log("Received Message: " + JSON.stringify(message));
		var payload = message.payload;
		switch (payload.type) {
			case "update_position":
				playerPositions = parseInt(payload.pos);
				break;
		}
	});
});


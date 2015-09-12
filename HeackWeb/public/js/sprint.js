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
			case "case_a":
				console.log("case_a");
				break;
			case "case_b":
				console.log("case_b");
				break;
			case "case_c":
				console.log("case_c");
				break;
		}
	});
});


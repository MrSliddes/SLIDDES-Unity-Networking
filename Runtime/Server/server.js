/*
@autor: SLIDDES
I wish you good luck coding in javascript.
*/

//#region Setup

// import express NodeJS framework module
var express = require('express');
// create an object of the express module
var app = express();
// create a http web server using the http library
var http = require('http').Server(app);
// import socketio communication module
var io = require('socket.io')(http);

app.use("/public/TemplateData", express.static(__dirname + "/public/TemplateData"));
app.use("/public/Build", express.static(__dirname + "/public/Build"));
app.use(express.static(__dirname + '/public'));

//#endregion Setup

// Open a connection with the specific client
io.on('connection', function (socket) {
	console.log('[INFO] A client is ready for a connection.');
	
	// Handle network message PING
	socket.on("PING", function (dataJ) {
		var data = { content: "[Server] Pong" };
		socket.emit("HANDLE", JSON.stringify({ type: "PING", data: JSON.stringify(data)}));
		console.log("Send Ping Response");
	});

}); // END io.on 

//////////////////
// Start server //
//////////////////
http.listen(process.env.PORT || 3001, function () {
	console.log('listening on *:3001');
});
console.log('------- NodeJS server is running -------');
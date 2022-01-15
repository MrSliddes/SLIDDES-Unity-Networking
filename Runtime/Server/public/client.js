/* 
Written by SLIDDES
You dont have to change anything here.
*/

var socket = io() || {};
socket.isReady = false;

var objectNameNetworkManager = '[SLIDDES Network WebGL]';

window.addEventListener('load', function() {
	// Handles all server.js network messages for Unity
	socket.on('HANDLE', function(data) {
		sendUnityMsg('Handle', data);
	});
});

// Send a message to a unity script from browser
function sendUnityMsg(methodName, value = '') {
	if(unityInstance != null) {
		unityInstance.SendMessage(objectNameNetworkManager, methodName, value);
	} else {
		console.log("[Client.js] unityInstance is null");
	}
}
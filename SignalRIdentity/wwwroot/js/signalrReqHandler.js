var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });
connection.onclose(function (error) {
    console.error('Connection closed:', error);
});

function sendMessageToHub(message) {
    connection.invoke('sendMessage', message);
}
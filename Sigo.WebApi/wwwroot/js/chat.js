"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (jsonString) {
    var data = JSON.parse(jsonString);
    var encodedMsg = data.user + " says " + data.message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function(){
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var data = {
        'user': user,
        'message': message
    }
    connection.invoke("SendMessage", JSON.stringify(data)).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
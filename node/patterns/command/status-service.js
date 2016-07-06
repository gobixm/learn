var statusService = {
    statusUpdates: {},
    sendUpdate: function(status) {
        console.log('status sent: ' + status);
        var id = Math.floor(Math.random() * 1000000);
        statusService.statusUpdates[id] = status;
        return id;
    },
    destroyUpdate: function(id) {
        console.log('status removed: ' + id);
        delete statusService.statusUpdates[id];
    }
}

function createSendStatusCommand(service, status) {
    var postId = null;
    var command = function() {
        postId = service.sendUpdate(status);
    };

    command.undo = function() {
        if (postId) {
            service.destroyUpdate(postId);
            postId = null;
        }
    };

    command.serialize = function() {
        return { type: 'status', action: 'post', status: status };
    }

    return command;
}

function Invoker() {
    this.history = [];
}

Invoker.prototype.run = function(command) {
    this.history.push(command);
    command();
    console.log('executed', command.serialize());
};

Invoker.prototype.delay = function(command, delay) {
    var self = this;
    setTimeout(function() {
        self.run(command);
    }, delay);
};

Invoker.prototype.undo = function() {
    var command = this.history.pop();
    command.undo();
    console.log('command undone', command.serialize());
};

var invoker = new Invoker();
var command = createSendStatusCommand(statusService, 'HI!LOW');
invoker.run(command);
invoker.undo();
invoker.delay(command, 2000);
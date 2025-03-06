function createEvent(typeOfEvent) {
    const event = document.createEvent('CustomEvent');
    event.initCustomEvent(typeOfEvent, true, true, null);
    event.dataTransfer = {
        data: {},
        setData: function(key, value) {
            this.data[key] = value;
        },
        getData: function(key) {
            return this.data[key];
        }
    };
    return event;
}

function dispatchEvent(element, event, transferData) {
    if (transferData !== undefined) {
        event.dataTransfer = transferData;
    }
    if (element.dispatchEvent) {
        element.dispatchEvent(event);
    } else if (element.fireEvent) {
        element.fireEvent('on' + event.type, event);
    }
}

function simulateHTML5DragAndDrop(element, destination) {
    const dragStartEvent = createEvent('dragstart');
    dispatchEvent(element, dragStartEvent);
    const dropEvent = createEvent('drop');
    dispatchEvent(destination, dropEvent, dragStartEvent.dataTransfer);
    const dragEndEvent = createEvent('dragend');
    dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);
}

simulateHTML5DragAndDrop(arguments[0], arguments[1]);
var FrameModel = function () {
    this.frames = [];
    this.addFrameEvent = new Event(this);
};

FrameModel.prototype = {

    addFrame: function (frame) {
        this.frames.push({
            firstRoll: first,
            secondRoll: second
        });
        this.addFrameEvent.notify();
    },

    getFrames: function () {
        return this.frames;
    }

};
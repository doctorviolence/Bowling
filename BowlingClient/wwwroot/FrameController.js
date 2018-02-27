var FrameController = function (model, view) {
    this.model = model;
    this.view = view;

    this.init();
};

FrameController.prototype = {

    init: function () {
        this.createChildren()
            .setupHandlers()
            .enable();
    },

    createChildren: function () {
        return this;
    },

    setupHandlers: function () {
        this.addFrameHandler = this.addFrame.bind(this);
        return this;
    },

    enable: function () {
        this.view.addFrameEvent.attach(this.addFrameHandler);
        return this;
    },


    addFrame: function (sender, args) {
        this.model.addFrame(args.frame);
    }

};
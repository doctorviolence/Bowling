var FrameView = function (model) {
    this.model = model;
    this.frames = [];
    this.addFrameEvent = new Event(this);

    this.init();
};

FrameView.prototype = {

    init: function () {
        this.createChildren()
            .setupHandlers()
            .enable();
    },

    createChildren: function () {
        this.$container = $('.js-container');
        this.$addFrameButton = this.$container.find('.js-add-frame-button');

        return this;
    },

    setupHandlers: function () {
        this.addFrameButtonHandler = this.addFrameButton.bind(this);

        return this;
    },

    enable: function () {
        this.$addFrameButton.click(this.addFrameButtonHandler);
        this.model.addFrame.attach(this.addFrameButtonHandler);

        return this;
    },

    addFrameButton: function () {
        this.addFrameEvent.notify({
            task: this.$frameInput.val()
        });
    },

    show: function () {
        this.frameList();
    },

    frameList: function () {
        var frames = this.model.getFrames();
        var html = "";
        var $framesContainer = this.framesContainer;

        $framesContainer.html('');

        var index = 0;
        for (var frame in frames) {
            index++;
        }

    },

    addFrame: function () {
        this.show();
    }

};
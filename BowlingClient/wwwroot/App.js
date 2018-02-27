$(function () {
    var model = new FrameModel(),
        view = new FrameView(model),
        controller = new FrameController(model, view);
});
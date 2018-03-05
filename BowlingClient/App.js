var app = angular.module('bowling-application', []);

app.controller('BowlingController', ['$scope', '$http', '$log', function ($scope, $http) {

    $scope.frames = [];

    $scope.addFrameToArray = function (frame) {
        $scope.frames.push(frame);
        console.log(frames);

        $http({
            url: 'http://localhost:5000/api/bowling/submit',
            dataType: 'json',
            method: 'POST',
            "frames": JSON.stringify(this.frames),
            headers: {
                "Content-Type": "text/plain"
            }
        }).then(function (success) {
            $scope.score = success.data.score;
        }, function (error) {
            $scope.score = "Failed";
            console.log(error);
        });
    }
}]);

angular.module('submitTopic', [])
    .controller('submitTopicCtrl', ['$scope', '$http', '$location', function ($scope, $http, $location) {
        $scope.submit = function () {
            $http.post('/api/WS_Topic/Submit', { title: $scope.title, url: $scope.url })
            .success(function () {
                $location.path('/');
            });
        };
    }]);
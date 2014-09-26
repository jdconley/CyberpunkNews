angular.module('topic', [])
    .controller('rankedTopicCtrl', ['$scope', '$http', function ($scope, $http) {
        $http.get('/api/WS_Topic/GetTopicList')
            .success(function (data, status, header, config) {
                console.log(data);
                $scope.topics = data;
            });
    }]);
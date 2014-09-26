angular.module('topic', [])
    .controller('rankedTopicCtrl', ['$scope', '$http', function ($scope, $http) {

        $http.get('/api/WS_Topic/GetTopicList')
            .success(function (data) {
                console.log(data);
                $scope.topics = data;
                _.forEach($scope.topics, function (topic) {
                    topic.submit_date = new Date(topic.submit_date.substr(0, 19));
                });
            });

        $scope.upvote = function (topic) {
            $http.post('/api/WS_Topic/Upvote/' + topic.id)
            .success(function (data, status) {
                // does return status matter?
                topic.karma += 1;
            });
        };

    }]);
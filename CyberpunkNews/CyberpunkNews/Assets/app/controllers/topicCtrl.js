angular.module('topic', ['ngRoute'])
    .controller('topicCtrl', ['$scope', '$http', '$location',
        function ($scope, $http, $location) {
            
            $http.get('/api/WS_Topic/GetTopicList')
                .success(function (data) {
                    $scope.topics = data;
                    _.forEach($scope.topics, function (topic) {
                        topic.submit_date = new Date(topic.submit_date.substr(0, 19));
                    });

                    var sortType = $location.search().sort;
                    if (sortType === 'date') {
                        $scope.topics = _.sortBy($scope.topics, 'submit_date').reverse();
                    }
                    else {
                        $scope.topics = _.sortBy($scope.topics, 'karma').reverse();
                    }
                });

            $scope.upvote = function (topic) {
                $http.post('/api/WS_Topic/Upvote/' + topic.id)
                .success(function (data, status) {
                    // does return status matter?
                    topic.karma += 1;
                });
            };

        }]);
angular.module('topic', ['ngRoute'])
    .controller('topicCtrl', ['$scope', '$http', '$location',
        function ($scope, $http, $location) {
            
            $http.get('/api/WS_Topic/GetTopicList')
                .success(function (data) {
                    $scope.topics = data;
                    _.forEach($scope.topics, function (topic) {
                        //topic.submitDate = topic.submitDate.substr(0, 19);
                        if (!topic.submitter) topic.submitter = 'anonymous';
                    });

                    var sortType = $location.search().sort;
                    if (sortType === 'date') {
                        $scope.topics = _.sortBy($scope.topics, 'submitDate').reverse();
                    }
                    else {
                        $scope.topics = _.sortBy($scope.topics, 'karma').reverse();
                    }

                    $http.get('/api/WS_Account/GetUserVotes')
                        .success(function (data) {
                            _.forEach($scope.topics, function (topic) {
                                topic.hasVoted = _.some(data, function (vote) {
                                    return vote.topic.id === topic.id;
                                });
                            });

                        });
                });

            $scope.upvote = function (topic) {
                $http.post('/api/WS_Topic/Upvote/' + topic.id)
                .success(function (data, status) {
                    // does return status matter?
                    topic.karma += 1;
                    topic.hasVoted = true;
                });
            };

        }]);
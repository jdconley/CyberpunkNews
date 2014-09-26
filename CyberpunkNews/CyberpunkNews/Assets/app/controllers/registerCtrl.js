angular.module('register', [])
    .controller('registerCtrl', ['$scope', '$http', '$timeout', '$location',
        function ($scope, $http, $timeout, $location) {

        $scope.register = function () {
            var params = {
                Email: $scope.username,
                Password: $scope.password1,
                ConfirmPassword: $scope.password2
            };
            $http.post('/api/Account/Register', params)
                .success(function () {
                    $scope.showSuccess = true;
                    $scope.showAlert = false;
                    $timeout(function () { $location.path('/signin'); }, 2000);
                })
                .error(function (data, status, headers, config) {
                    $scope.message = data.Message;

                    for (var key in data.ModelState) {
                        $scope.message += data.ModelState[key];
                    }

                    $scope.showAlert = true;
                });
        }

        $scope.showAlert = false;
    }]);
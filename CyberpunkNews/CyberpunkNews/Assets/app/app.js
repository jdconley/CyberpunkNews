/* App Module */

var app = angular.module('app', [
    'ngRoute',
    'ngCookies',
    'topic',
    'submitTopic',
    'signIn',
    'register',
    'main'
]);

app.config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {

    //================================================
    // Add an interceptor for AJAX errors
    //================================================
    $httpProvider.responseInterceptors.push(['$q','$location', function ($q, $location) {
        return function (promise) {
            return promise.then(
              // Success: just return the response
              function (response) {
                  return response;
              },
              // Error: check the error status to get only the 401
              function (response) {
                  if (response.status === 401)
                      $location.url('/signin');
                  return $q.reject(response);
              }
            );
        }
    }]);
    
    //================================================
    // Routes
    //================================================
    $routeProvider.when('/', {
        templateUrl: 'Assets/template/topic_list.html',
        controller: 'topicCtrl'
    });
    $routeProvider.when('/submit', {
        templateUrl: 'Assets/template/submit_topic.html',
        controller: 'submitTopicCtrl'
    });
    $routeProvider.when('/register', {
        templateUrl: 'App/Register',
        controller: 'registerCtrl'
    });
    $routeProvider.when('/signin', {
        templateUrl: 'App/SignIn',
        controller: 'signInCtrl'
    });
    
    $routeProvider.otherwise({
        redirectTo: '/'
    });    
}]);

app.run(['$http', '$cookies', '$cookieStore', function ($http, $cookies, $cookieStore) {
    //If a token exists in the cookie, load it after the app is loaded, so that the application can maintain the authenticated state.
    $http.defaults.headers.common.Authorization = 'Bearer ' + $cookieStore.get('_Token');
}]);



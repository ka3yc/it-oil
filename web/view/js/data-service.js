angular.module('appDataModule', ['webapiHelperModule'])
        .config(['$httpProvider', function ($httpProvider) {
            //$httpProvider.defaults.withCredentials = true;
        }])
.factory('appDataService', ['$http', 'webapiHelperService', function ($http, webapiHelp) {
    var self = this;
    
    var apiBaseUrl = "../api/";
    
    return {
        
        getCommonData: function () {
            return webapiHelp.request(function () {
                return $http.get(apiBaseUrl + 'common');
            });
        },

        getMetrics: function () {
            return webapiHelp.request(function () {
                return $http({
                    method: 'GET',
                    url: apiBaseUrl + 'metric'
                });
            });
        },

        getMetricHistory: function (metricId) {
            return webapiHelp.request(function () {
                return $http({
                    method: 'GET',
                    url: apiBaseUrl + 'metric/' + metricId + '/' + 'day'
                });
            });
        },

        getWellData: function () {
            return webapiHelp.request(function () {
                return $http({
                    method: 'GET',
                    url: apiBaseUrl + 'well'
                });
            });
        }
    };

}]);
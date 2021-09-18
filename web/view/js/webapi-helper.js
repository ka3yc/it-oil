/*
 Все методы бекенда возвращают объект BaseResult в котором есть поля IsSuccess, Message, Result
 Чтобы не дублировать логику обработки результата, сделан этот хелпер
 */
angular.module('webapiHelperModule', [])
    .factory('webapiHelperService', ['$q', function ($q) {
        var self = this;
        
        self.processWebapiError = function (response) {
            var text = response.statusText + "; ";

            if (response.data && response.data.Message)
                text += response.data.Message + "; ";
            if (response.data && response.data.ExceptionMessage)
                text += response.data.ExceptionMessage;

            return text;
        };

        return {
            request: function (httpRequest) {
                if (typeof (httpRequest) !== 'function')
                    return;

                var deferred = $q.defer();

                httpRequest().then(function (response) {
                    var data = response.data;
                    //Для обычных Page-методов
                    if (data.d)
                        data = data.d;

                    if (data.IsSuccess)
                        deferred.resolve(data.Result);
                    else
                        deferred.reject(data.Message);
                }, function (response) {
                    deferred.reject(self.processWebapiError(response));
                });

                return deferred.promise;
            }
        };

    }]);
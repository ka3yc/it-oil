var app = angular.module('app', ["appDataModule", "metric-gauge", "metric-license", "metric-poll", "chart-map"])

app.controller('MainController', ['$http', '$interval', '$location', '$timeout', '$scope', 'appDataService', function ($http, $interval, $location, $timeout, $scope, dataSvc) {
    var ctrl = this;
    ctrl.IsLoading = true;
    //Если не задано - показываем общую информацию, если указана скважина - то показываем инфу по ней
    ctrl.ViewWell = null;

    ctrl.Status = {
        Basic: {
            Failed: 0,
            Good: 0
        },
        Wells: {
            Failed: 0,
            Warn: 0,
            Good: 0
        }
    }

    ctrl.Metrics = [];
    ctrl.MetricHistories = [];
    ctrl.Errors = [];

    ctrl.Initialize = function () {

        moment.locale('ru-Ru');

        $scope.$watch('ctrl.ViewWell', function (newVal, oldVal) {
            if (oldVal != newVal && newVal && newVal.Id)
                ctrl.GetWellDetails(newVal.Id)
        });

        var search = $location.search();
        if (search.wellId)
            ctrl.ViewWell = parseInt(search.wellId, 10);

        ctrl.GetCommonData();
        ctrl.RefreshData();

        $interval(function () {
            ctrl.RefreshData();
        }, 5000);

        
    }

    ctrl.RefreshData = function () {
        ctrl.GetMetrics();
        ctrl.GetWellData();
    }

    ctrl.GetCommonData = function () {

        dataSvc.getCommonData()       
            .then(function (result) {
                ctrl.IsLoading = false;
                ctrl.Data = result;
            }, function (error) {
                ctrl.IsLoading = false;
                ctrl.ShowError(error);
            });
    }

    ctrl.GetMetrics = function () {

        dataSvc.getMetrics()
            .then(function (result) {
                ctrl.IsLoading = false;
                
                ctrl.Status.Basic.Failed = 0;
                ctrl.Status.Basic.Good = 0;

                angular.forEach(result, function (metric) {
                    var m = ctrl.Metrics.filter(function (m) { return m.Data.Id == metric.Id })[0];
                    if (m) {
                        m.Data = metric;
                    } else
                        ctrl.Metrics.push({
                            Data: metric
                        });
                    if (!metric.IsOk)
                        ctrl.Status.Basic.Failed++;
                    else
                        ctrl.Status.Basic.Good++;

                    if (metric.ValueDate) {
                        metric.LastPoll = moment(metric.ValueDate).fromNow();
                    }
                });

                //Перерисовать графики
                $timeout(function () {
                    angular.forEach(ctrl.Metrics, function (metric) {
                        if (metric.Chart) {
                            metric.Chart.updateValue(
                                parseInt(metric.Data.Min, 10),
                                parseInt(metric.Data.Max, 10),
                                parseInt(metric.Data.Value, 10));
                        }
                    });
                }, 10);

            }, function (error) {
                ctrl.IsLoading = false;
                ctrl.ShowError(error);
            });
    }

    ctrl.GetWellData = function () {
        dataSvc.getWellData()
            .then(function (result) {
                var wells = getWells(result);

                //Для отладки
                //if(!ctrl.ViewWell)
                //    ctrl.ViewWell = wells[0];

                if (!ctrl.WellData || !ctrl.WellData.length)
                    ctrl.WellData = result;
                else {
                    ctrl.PopulateWellData(wells);
                    ctrl.ResetWellTreeFaults(ctrl.WellData);
                }

                ctrl.UpdateWellStatus(wells);

                if (ctrl.ChartMap)
                    ctrl.ChartMap.updateData(
                        prepareDataForMap(wells));
            }, function (error) {
                ctrl.ShowError(error);
            });
    }

    ctrl.UpdateWellStatus = function (wells) {
        var result = wells.reduce(
            (res, well) => {
                if (!well.IsInternetOk)
                    res.Failed++;
                else if (well.SignalLevel <= 2)
                    res.Warn++;
                else
                    res.Good++;
                return res;
            }, {
            Failed: 0,
            Warn: 0,
            Good: 0
        });

        ctrl.Status.Wells = result;
    }

    //Чтобы всё дерево не перерисовывалось, приходится обновлять значения по каждой скважине
    ctrl.PopulateWellData = function (wells) {
        
        var currentWells = getWells(ctrl.WellData);

        angular.forEach(currentWells, function (curWell) {
            var well = wells.find(function (w) {
                return w.Id == curWell.Id;
            });
            
            if (well) {
                curWell.IsInternetOk = well.IsInternetOk;
                curWell.SignalLevel = well.SignalLevel;
                curWell.LastStatusUpdate = well.LastStatusUpdate;
            }
        });
    }        

    ctrl.ResetWellTreeFaults = function (treeNodes) {

        angular.forEach(treeNodes, function (item) {
            if (item.Children && item.Children.length) {
                ctrl.ResetWellTreeFaults(item.Children);

                item.HasFails = item.Children.some(c => c.HasFails);
                item.HasWarnings = item.Children.some(c => c.HasWarnings);
            }
            else {
                item.HasFails = item.Wells.some(w => !w.IsInternetOk);
                item.HasWarnings = item.Wells.some(w => w.SignalLevel <= 2);
            }
        });

    }

    ctrl.GetWellDetails = function (wellId) {
        //Тут должен быть запрос к серверу
        $timeout(function () {
            ctrl.ViewWell.Details = {
                Wells: [{ Name: "101" }, { Name: "102" }, { Name: "103" }, { Name: "104" }, { Name: "105" }, { Name: "106" }, { Name: "107" }]
            };
        }, 10);
    }

    ctrl.GetMetricHistory = function (metric) {
        dataSvc.getMetricHistory(metric.Data.Id)
            .then(function (response) {
                //не доделал
                //ctrl.MetricHistories.push(response);
            }, function (error) {
                ctrl.ShowError(error);
            })
    }

    ctrl.RemoveHistory = function (hist) {
        let index = ctrl.MetricHistories.indexOf(hist);
        ctrl.MetricHistories.splice(index, 1);
    }

    ctrl.getMetricBoxCss = function (metric) {
        var css = "box ";
        var type = metric.Data.Type;

        if (type == 'gauge')
            css += 'box-gauge ';
        else if (type == 'license')
            css += 'box-license';
        else if (type == 'text')
            css += 'box-text';
        return css;
    }

    ctrl.GetMetricCssClass = function (metric) {
        if (metric.Status == "ok")
            return "fas fa-thumbs-up metric-status-ok";
        if (metric.Status == "warn")
            return "fas fa-exclamation-triangle metric-status-warn";
        if (metric.Status == "fail")
            return "fas fa-times metric-status-fail";
    }

    ctrl.GetWellSignalCss = function (well) {
        if (well.SignalLevel < 3)
            return 'well-badsignal';
        else
            return 'well-goodsignal';
    }

    ctrl.ShowError = function (error) {
        if (ctrl.Errors.indexOf(error) < 0)
            ctrl.Errors.push(error);
    }
    
    ctrl.RemoveError = function (error) {
        var idx = ctrl.Errors.indexOf(error);
        if (idx >= 0)
            ctrl.Errors.splice(idx, 1);
    }

    ctrl.Initialize();

    function getWells(data) {
        var wells = [];

        function findChildrenWells(treeNodes) {
            angular.forEach(treeNodes, function (item) {
                if (item.Wells)
                    wells = wells.concat(item.Wells);
                if (item.Children)
                    findChildrenWells(item.Children);
            });
        }

        findChildrenWells(data);
        
        return wells;
    }

    function prepareDataForMap(wells) {
       
        var mapData = wells.map(function (well) {
            var r = {
                Name: well.FullName,
                Latitude: well.Latitude,
                Longitude: well.Longitude
            }

            if (!well.IsInternetOk)
                r.color = '#f44336';
            else if (well.SignalLevel <= 2)
                r.color = '#ff9800';
            else
                r.color = '#4caf50';

            return r;
        });

        return mapData;
    }
   
}]);



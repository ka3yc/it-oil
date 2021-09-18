/*
 * Компонент для отрисовки метрики вида "опрос". Используется библиотека amcharts
 * */
angular.module("metric-poll", [])
  .directive("metricPoll", function () {
    return {
      restrict: "E",
      scope: {		  
		  lastPoll: '=',
		  period: '=',
		  isOk: '='
	  },
      template: `<div>
					<div><b>Состояние: </b> 
						<i class=\'fas fa-check-circle\' ng-if=\'isOk\'></i>
						<i class=\'fas fa-exclamation-circle\' ng-if=\'!isOk\'></i></div>
					<div><b>Последний опрос:</b> {{lastPoll}}</div>
					<div><b>Периодичность опроса:</b> {{period}}</div></div>`,
      replace: true,
      link: function ($scope) {
		//$scope.getStatus = function(){
  //    		if (parseInt($scope.lastPoll, 10) > parseInt($scope.period, 10))
		//		return 'fail';
		//	else
		//		return 'success';
  //    	}
		
      }
    };
  });
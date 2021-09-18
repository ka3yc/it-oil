angular.module("metric-gauge", ["ui/chartTemplate.html"])
  .directive("metricGauge", function () {
    return {
      restrict: "E",
      scope: {		  
		  number: '=',
		  chart: '='
	  },
      templateUrl: function (element, attrs) {
            return 'ui/chartTemplate.html';
        },
      replace: true,
		controller: function(){
			console.log('contr');
		},
      link: function ($scope) {
		console.log('link');
		 setTimeout(function(){
		 
			var chart = am4core.create("chart" + $scope.number, am4charts.GaugeChart);
			chart.innerRadius = -15;

			var axis = chart.xAxes.push(new am4charts.ValueAxis());
			axis.min = 0;
			axis.max = 100;
			axis.strictMinMax = true;

			var colorSet = new am4core.ColorSet();

			var gradient = new am4core.LinearGradient();
			gradient.stops.push({ color: am4core.color("red") })
			gradient.stops.push({ color: am4core.color("yellow") })
			gradient.stops.push({ color: am4core.color("green") })

			axis.renderer.line.stroke = gradient;
			axis.renderer.line.strokeWidth = 15;
			axis.renderer.line.strokeOpacity = 1;

			axis.renderer.grid.template.disabled = true;

			var hand = chart.hands.push(new am4charts.ClockHand());
			hand.radius = am4core.percent(95);
			
			chart.updateValue = function (min, max, value) {
				axis.min = parseInt(min, 10);
				axis.max = parseInt(max, 10);
				setTimeout(function () {
					hand.showValue(parseInt(value, 10), 500, am4core.ease.cubicOut);	
				}, 10);	
			}
			
			$scope.$on("$destroy", function () {
			  chart.dispose();
			});
			
			$scope.chart = chart;
			
		 }, 10);
        
      }
    };
  });
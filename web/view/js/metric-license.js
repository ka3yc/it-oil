angular.module("metric-license", ["ui/chartTemplate.html"])
  .directive("metricLicense", function () {
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
      link: function ($scope) {
		 setTimeout(function(){
		 			
			var circleSize = 0.8;

			var component = am4core.create("chart" + $scope.number, am4core.Container)
			component.width = am4core.percent(100);
			component.height = am4core.percent(90);

			var chartContainer = component.createChild(am4core.Container);
			chartContainer.x = am4core.percent(50)
			chartContainer.y = am4core.percent(50)

			var circle = chartContainer.createChild(am4core.Circle);
			circle.fill = am4core.color("#dadada");

			var circleMask = chartContainer.createChild(am4core.Circle);

			var waves = chartContainer.createChild(am4core.WavedRectangle);
			waves.fill = am4core.color("#34a4eb");
			waves.mask = circleMask;
			waves.horizontalCenter = "middle";
			waves.waveHeight = 50;
			waves.waveLength = 10;
			waves.y = 200;
			circleMask.y = -200;

			component.events.on("maxsizechanged", function () {
				var smallerSize = Math.min(component.pixelWidth, component.pixelHeight);
				var radius = smallerSize * circleSize / 2;

				circle.radius = radius;
				circleMask.radius = radius;
				waves.height = smallerSize;
				waves.width = Math.max(component.pixelWidth, component.pixelHeight);

				capacityLabel.y = radius;

				var labelRadius = radius + 20

				capacityLabel.path = am4core.path.moveTo({ x: -labelRadius, y: 0 }) + am4core.path.arcToPoint({ x: labelRadius, y: 0 }, labelRadius, labelRadius);
				capacityLabel.locationOnPath = 0.5;
			})
			
			var label = chartContainer.createChild(am4core.Label)
			label.fill = am4core.color("#fff");
			label.fontSize = 20;
			label.horizontalCenter = "middle";

			var capacityLabel = chartContainer.createChild(am4core.Label)		
			capacityLabel.fill = am4core.color("#34a4eb");
			capacityLabel.fontSize = 10;
			capacityLabel.textAlign = "middle";
			capacityLabel.padding(0, 0, 0, 0);
						
			component.updateValue = function (min, max, value) {
				var y = -circle.radius - waves.waveHeight + (1 - value / max) * circle.pixelRadius * 2;
				waves.animate([{ property: "y", to: y }, { property: "waveHeight", to: 1, from: 3 }, { property: "x", from: -20, to: 0 }], 2000, am4core.ease.elasticOut);
				circleMask.animate([{ property: "y", to: -y }, { property: "x", from: 20, to: 0 }], 2000, am4core.ease.elasticOut);
				
				var formattedValue = component.numberFormatter.format(value, "#.#a");
				formattedValue = formattedValue.toUpperCase();
				label.text = formattedValue + " дней";
				
				var formattedCapacity = component.numberFormatter.format(max, "#.#a").toUpperCase();;
				capacityLabel.text = "Всего " + formattedCapacity + " дней";
			}
			
			$scope.chart = component;
			
		 }, 10);
        
      }
    };
  });
angular.module("ui/chartTemplate.html", []).run(["$templateCache", function ($templateCache) {
	$templateCache.put("ui/chartTemplate.html",     
		"<div ng-attr-id='chart{{number}}'>{{number}}</div>");
}]);
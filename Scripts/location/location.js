var app = angular.module('map', ['uiGmapgoogle-maps']);

app.controller('mapController', ['$scope', 'uiGmapGoogleMapApi', '$http', '$q', '$timeout', function ($scope, uiGmapGoogleMapApi, $http, $q, $timeout) {
              

    $scope.trip = [];
    $scope.walk = [];
    $scope.user = [];
    $scope.activity = "?";
    $scope.closest = "";

    var pathPoints = [];
    var markerPoints = [];

    var lat1, lat2, lng1, lng2, t1, t2;

    t1 = new Date().getTime();

    t2 = new Date().getTime();

    var params = getQueryParams(location.href);
    var watchPos = true;
    // onSuccess Callback
    // This method accepts a Position object, which contains the
    // current GPS coordinates
    //

    if (localStorage.getItem("wc" + params["tripId"] + params["Date"])) {
        $scope.walkCoordinates = JSON.parse(localStorage.getItem("wc" + params["tripId"] + params["Date"]));
    }
    else {
        $scope.walkCoordinates = [];
    }

    console.log($scope.walkCoordinates);
    var onSuccess = function (position) {

        uiGmapGoogleMapApi.then(function (mapsApi) {
            google.maps = mapsApi;
            $scope.map = {
                center: { latitude: position.coords.latitude, longitude: position.coords.longitude },
                zoom: 12
            };

            $scope.path = [{
                "latitude": 55.391682,
                "longitude": 10.382157
            }];

            $scope.markers = [{
                id: 1,
                latitude: 55.398584,
                longitude: 10.390258,
                map: $scope.map,
            }];

            lat2 = lat1 = position.coords.latitude;
            lng2 = lng1 = position.coords.longitude;

        getStepsAsPoints(params).then(
            function (data) {
                for (var i = 0; i < data["pts"].length; i++) {

                    pathPoints.push({
                        latitude : data["pts"][i][1],
                        longitude : data["pts"][i][2]
                    });
                    
                    markerPoints.push({
                        id : i,
                        latitude : data["pts"][i][1],
                        longitude: data["pts"][i][2],
                        map: $scope.map,
                        title: data["tts"][i]
                    });
                }
                $scope.markers = markerPoints;


                $scope.trip = [
                        {
                            "id": 1,
                            "path": pathPoints,
                            "stroke": {
                                "color": "#6060FB",
                                "weight": 3
                            },
                            "geodesic": true,
                            "visible": true
                        }];

                $scope.walk = [
                {
                    "id": 1,
                    "path": $scope.walkCoordinates,
                    "stroke": {
                        "color": "#60FBFB",
                        "weight": 3
                    },
                    "geodesic": true,
                    "visible": true
                }];


                navigator.geolocation.watchPosition(onSuccessWatch, onError, { timeout: 30000 });

            }
            );
        });
    };

    var onSuccessWatch = function (position) {

            t1 = t2;

            t2 = new Date();
            t2 = t2.getTime();

            lat1 = lat2;
            lng1 = lng2;

            lat2 = position.coords.latitude;
            lng2 = position.coords.longitude;
            var deltat = (t2 - t1) / 1000;

            var speed = (getDistance(lat1, lat2, lng1, lng2) / 1000) / (deltat / 3600);
            console.log(speed);
            
            if (speed < 1) { $scope.activity = "still"; }
            if (speed < 8 && speed >= 1) { $scope.activity = "walk"; }
            if (speed >= 8) { $scope.activity = "ride/run"; }

            var stepDistance = getDistance(markerPoints[0]["latitude"], position.coords.latitude, markerPoints[0]["latitude"], position.coords.longitude);
            $scope.closest = markerPoints[0]["title"];

            for(var i=0; i< markerPoints.length; i++){
                if (getDistance(markerPoints[i]["latitude"], position.coords.latitude, markerPoints[i]["latitude"], position.coords.longitude) < stepDistance) {
                    $scope.closest = markerPoints[i]["title"];
                }
            }


        $scope.user = [
            {
                id: 0,
                latitude: position.coords.latitude,
                longitude: position.coords.longitude,
                map: $scope.map,
                title: "you"
            }];


        $scope.walkCoordinates.push({ "latitude": position.coords.latitude, "longitude": position.coords.longitude });

    };
    
    $scope.saveWalkPath = function () {
        localStorage.setItem("wc" + params["tripId"] + params["Date"], JSON.stringify($scope.walkCoordinates));
        console.log(localStorage.getItem("wc" + params["tripId"] + params["Date"]));
        console.log(JSON.parse(localStorage.getItem("wc" + params["tripId"] + params["Date"])));
    console.log("path saved");
    }

    function onError(error) {
        alert('code: ' + error.code + '\n' +
              'message: ' + error.message + '\n');
    }


    function getStepsAsPoints(params) {
        var deferred = $q.defer();
        $http({
            url: '/Trip/Points',
            method: 'GET',
            params: {
                Date: params["Date"],
                tripId: params["tripId"]
            }
        }).success(
        function (result) {

            deferred.resolve(JSON.parse(result));
        }).error(
        deferred.reject
        );
        return deferred.promise;
    }


    
    navigator.geolocation.getCurrentPosition(onSuccess, onError);
}]);

function getQueryParams(qs) {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars[hash[0]] = decodeURIComponent(hash[1]);
    }
    return vars;
}

function getDistance(lat1, lat2, lng1, lng2) {

    var R = 6371000; // metres
    var φ1 = lat1.toRadians();
    var φ2 = lat2.toRadians();
    var Δφ = (lat2 - lat1).toRadians();
    var Δλ = (lng2 - lng1).toRadians();

    var a = Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
            Math.cos(φ1) * Math.cos(φ2) *
            Math.sin(Δλ / 2) * Math.sin(Δλ / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    var d = R * c;
    return d;
}

Number.prototype.toRadians = function () {
        return this * Math.PI / 180;
    }


var app = angular.module('map', ['uiGmapgoogle-maps']);

app.controller('mapController', ['$scope', 'uiGmapGoogleMapApi', function ($scope, uiGmapGoogleMapApi) {

    // onSuccess Callback
    // This method accepts a Position object, which contains the
    // current GPS coordinates
    //

    var walkCoordinates = [];
    var onSuccess = function (position) {

        uiGmapGoogleMapApi.then(function (mapsApi) {
            google.maps = mapsApi;
        });

        alert('Latitude: ' + position.coords.latitude + '\n' +
              'Longitude: ' + position.coords.longitude + '\n' +
              'Altitude: ' + position.coords.altitude + '\n' +
              'Accuracy: ' + position.coords.accuracy + '\n' +
              'Altitude Accuracy: ' + position.coords.altitudeAccuracy + '\n' +
              'Heading: ' + position.coords.heading + '\n' +
              'Speed: ' + position.coords.speed + '\n' +
              'Timestamp: ' + position.timestamp + '\n');

        $scope.map = {
            center: { latitude: position.coords.latitude, longitude: position.coords.longitude },
            zoom: 12,
            trip: [
                {
                  "id": 1,
                  "path": [
                    {
                        "latitude": 55.398584,
                        "longitude": 10.390258
                    },
                    {
                        "latitude": 55.391682,
                        "longitude": 10.382157
                    }
                  ],
                  "stroke": {
                      "color": "#6060FB",
                      "weight": 3
                  },
                  "geodesic": true,
                  "visible": true
              }]
        };

        $scope.markers = [
            { 
                id: 1,
                latitude: 55.398584,
                longitude: 10.390258,
                map: $scope.map,
                title: "HCA huset" 
            },
            {
                id: 2,
                latitude: 55.391682,
                longitude: 10.382157,
                map: $scope.map,
                title: "Munke Mose"
            }
        ];
    };

    var onSuccessWatch = function (position) {
        
        $scope.position = position.coords.latitude + " " + position.coords.longitude;
        console.log($scope.position);

        $scope.user = [
            {
                id: 0,
                latitude: position.coords.latitude,
                longitude: position.coords.longitude,
                map: $scope.map,
                title: "you"
            }];


        walkCoordinates.push({ "latitude": position.coords.latitude, "longitude": position.coords.longitude });

        $scope.map.walk = [
                {
                    "id": 1,
                    "path": walkCoordinates,
                    "stroke": {
                        "color": "#60FBFB",
                        "weight": 3
                    },
                    "geodesic": true,
                    "visible": true
                }]
    };

    // onError Callback receives a PositionError object
    //
    function onError(error) {
        alert('code: ' + error.code + '\n' +
              'message: ' + error.message + '\n');
    }


    navigator.geolocation.getCurrentPosition(onSuccess, onError);
    navigator.geolocation.watchPosition(onSuccessWatch, onError, { timeout: 30000 });

}]);




//app.directive('mapd', ['uiGmapGoogleMapApi', '$interval', function (uiGoogleMapsApi, $interval) {

//    var google = { maps: null };
//    var map = null;

//    return {
//        restrict: 'E',
//        scope: {
//            control: '=',
//            center: '='
//        },
//       link: link
//    };



//    function link(scope, $element, attrs) {
//        // Wait for Google Maps script to load
//        uiGoogleMapsApi.then(function (mapsApi) {
//            // Restore google namespace for code compatibility
//            google.maps = mapsApi;
//            scope.center = {
//                lat: 55.371667,
//                lng: 10.420035
//            }
//            if (!scope.center || !scope.center.lat || !scope.center.lng) {
//                return console.error('position failure');
//            }

//            map = createMap($element[0], scope.center);

//            drawUser(scope.center);


//            var controlApi = scope.control || {};
//            controlApi.updateUserPosition = updateUserPosition;


//            if (controlApi.initCallback) {
//                controlApi.initCallback();
//            }

//        });
//    }



//    function createMap(domElement, centerCoords) {
//        var position = new google.maps.LatLng(centerCoords.lat, centerCoords.lng);

//        var mapOptions = {
//            zoom: 14,
//            center: position,
//            mapTypeControl: false,
//            streetViewControl: false,
//            rotateControl: false,
//            //zoomControl: false,
//            noClear: true
//            // noClear: boolean	- If true, do not clear the contents of the Map div.
//        };

//        return new google.maps.Map(domElement, mapOptions);
//    }



//    function drawUser(centerCoords) {
//        var position = new google.maps.LatLng(centerCoords.lat, centerCoords.lng);

//        var markerOptions = {
//            position: position,
//            map: map
//        };

//        var marker = new google.maps.Marker();

//        userMarker = {
//            marker: marker
//        };
//    }



//    function updateUserPosition(coords) {
//        console.log('new position', coords);
//        var position = new google.maps.LatLng(coords.lat, coords.lng);

//        userMrk.marker.setPosition(position);
//        if (followUser) {
//            map.panTo(position);
//        }
//    }


//            return options;
//        }
    
//}]);
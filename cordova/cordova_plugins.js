cordova.define('cordova/plugin_list', function (require, exports, module) {
    module.exports = [
        {
            "file": "plugins/cordova-plugin-geolocation/Position.js",
            "id": "cordova-plugin-geolocation.Position",
            "pluginId": "cordova-plugin-geolocation.Position",
            "runs": true
        },
            {
                "file": "plugins/cordova-plugin-geolocation/Coordinates.js",
                "id": "cordova-plugin-geolocation.Coordinates",
                "pluginId": "cordova-plugin-geolocation.Coordinates",
                "runs": true
            },
        {
            "file": "plugins/cordova-plugin-geolocation/geolocation.js",
            "id": "cordova-plugin-geolocation.geolocation",
            "pluginId": "cordova-plugin-geolocation",
            "runs": true
        },
        {
            "file": "plugins/facebookPluginConnect/facebookPluginConnect.js",
            "id": "com.phonegap.plugins.facebookconnect.FacebookConnectPlugin",
            "pluginId": "com.phonegap.plugins.facebookconnect.FacebookConnectPlugin",
            "runs": true
        },
        {
            "file": "plugins/cordova-plugin-battery-status/battery.js",
            "id": "cordova-plugin-battery-status.battery",
            "pluginId": "cordova-plugin-battery-status.battery",
            "runs": true
        }
    ];
    module.exports.metadata =
    // TOP OF METADATA
    {
        "cordova-plugin-whitelist": "1.2.0",
        "cordova-plugin-geolocation": "1.0.1"
    }
    // BOTTOM OF METADATA
});
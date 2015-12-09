cordova.define('cordova/plugin_list', function(require, exports, module) {
module.exports = [
    {
        "file": "plugins/cordova-plugin-whitelist/whitelist.js",
        "id": "cordova-plugin-whitelist.whitelist",
        "pluginId": "cordova-plugin-whitelist",
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
        "id": "facebookPluginConnect.facebookPluginConnect",
        "pluginId": "facebookPluginConnect",
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
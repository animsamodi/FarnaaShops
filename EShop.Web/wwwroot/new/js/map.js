 //----map
var Lat = $("#Lat").val()
var Lng = $("#Lng").val()

 var mapOptions = {
     center: [Lat, Lng],
     zoom: 15
 }

 var map = new L.map('map', mapOptions);
 var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');

map.addLayer(layer);
L.marker([Lat, Lng]).addTo(map)
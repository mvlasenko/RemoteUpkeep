var maps = [];
var markers = [];

function initMap() {

    $('.geography').each(function (i) {

        var geoVal = $(this).val().replace("(", "").replace(")", "");
        var geoLat = geoVal.split(",")[0].trim();
        var geoLng = geoVal.split(",")[1].trim();
        var geoId = $(this).attr('id');

        var pos = new google.maps.LatLng(geoLat, geoLng);

        maps[i] = new google.maps.Map(document.getElementById('map_' + geoId), {
            center: pos,
            zoom: 16
        });

        markers[i] = new google.maps.Marker({
            position: pos,
            map: maps[i],
            draggable: true,
            animation: google.maps.Animation.DROP
        });

        markers[i].setIcon('https://maps.google.com/mapfiles/ms/icons/green-dot.png')

        google.maps.event.addListener(markers[i], 'dragend', function () {
            geocodePosition(markers[i].getPosition(), geoId);
        });

        google.maps.event.addListener(maps[i], "click", function (event) {
            markers[i].setPosition(event.latLng);
            geocodePosition(event.latLng, geoId);
        });

    });
}

function geocodePosition(pos, id) {
    geocoder = new google.maps.Geocoder();
    geocoder.geocode
        ({
            latLng: pos
        },
        function (results, status) {
            $('#' + id).val(pos);
        });
}

function moveMarker(map, marker, lat, lng, id) {
    if (map && lat && lng) {
        var pos = new google.maps.LatLng(lat, lng);
        map.panTo(pos);
        marker.setPosition(pos);
        geocodePosition(pos, id);
    }
}

function InitRegionChange() {
    $('.region').change(function () {
        var selected = $(this).find('option:selected');
        var lat = selected.data('geo-lat');
        var lng = selected.data('geo-lng');
        moveMarker(maps[maps.length - 1], markers[markers.length - 1], lat, lng, 'Geography');
    });
}
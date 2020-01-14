(function (cibertec){
    window.cibertec.getLocation = function () {
        console.log("Verificando Geolocation!!");
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        }
        else {
            console.log("No se admite Geolocation!!");
        }
    }

    function showPosition(position) {
        var location = {
            lat: -12.1222595,  //position.coords.latitude,
            lng: -77.0304797 //position.coords.longitude
        };
        console.log(location.lat);
        console.log(location.lng);

        var map = new google.maps.Map(document.getElementById('googleMap'), {
            zoom: 15,
            center: location
        });
        var marker = new google.maps.Marker({
            position: location,
            map: map,
            title: 'Cibertec!'
        });

    }
})(window.cibertec = window.cibertec || {});
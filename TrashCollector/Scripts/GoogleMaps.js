async function initMap() {
    let addressCols = document.getElementsByClassName("addressCol");
    let addresses = [];
    for (i = 0; i < addressCols.length; i++)
        addresses.push(addressCols[i].innerHTML);
    let zipcode = document.getElementsByClassName("zipCol")[0].innerHTML;
    let lat = 43;
    let long = -88.25;
    let zoom = 9;
    let geocoder = new google.maps.Geocoder();
    await geocoder.geocode({ 'address': zipcode }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            lat = results[0].geometry.location.lat();
            long = results[0].geometry.location.lng();
            zoom = 12;
        }
    });
    let options = {
        zoom: zoom,
        center: {
            lat: lat, lng: long
        }
    };
    let map = new google.maps.Map(document.getElementById("map"), options);


}
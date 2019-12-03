async function initMap() {

    let addresses = getAddresses();
    let names = getNames();
    let zipcode = document.getElementsByClassName("zipCol")[0].innerHTML;

    let positions = [];
    positions = await getPositions(positions, addresses, zipcode);

    let options = await setOptions(zipcode);

    var map = new google.maps.Map(document.getElementById("map"), options);
    placeMarkers(positions, names, map);
}

function getAddresses() {
    let addressCols = document.getElementsByClassName("addressCol");
    let addresses = [];
    for (i = 0; i < addressCols.length; i++)
        addresses.push(addressCols[i].innerHTML);
    return addresses;
}

function getNames() {
    let nameCols = document.getElementsByClassName("nameCol");
    let names = [];
    for (let i = 0; i < nameCols.length; i++)
        names.push(nameCols[i].innerHTML.split('>')[1].split('<')[0]);
    return names;
}

async function setOptions(zipcode) {
    //Default options for when the geocoder cannot access the api.
    let lat = 43;
    let long = -88.25;
    let zoom = 9;
    //
    let geocoder = new google.maps.Geocoder();
    await geocoder.geocode({ 'address': zipcode }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            lat = results[0].geometry.location.lat();
            long = results[0].geometry.location.lng();
            zoom = 12;
        }
    });
    return {
        zoom: zoom,
        center: { lat: lat, lng: long }
    };
}

function placeMarkers(positions, names, map) {
    let markers = []
    for (let i = 0; i < positions.length; i++) {
        markers.push(new google.maps.Marker({
            position: positions[i],
            map: map,
            title: names[i]
        }));
    }
}

async function getPositions(positions, addresses, zip) {
    //Default Coords for when the geocoder cannot access the api.
    let lat = 43;
    let long = -88;
    //
    let geocoder = new google.maps.Geocoder();
    for (let i = 0; i < addresses.length; i++) {
            await geocoder.geocode({ 'address': addresses[i] + " " + zip }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    lat = results[0].geometry.location.lat();
                    long = results[0].geometry.location.lng();
                }
            });
        positions[i] = { lat: lat, lng: long };
    }
    return positions;
}
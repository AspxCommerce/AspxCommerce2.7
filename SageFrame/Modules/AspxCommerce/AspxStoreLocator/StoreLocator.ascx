<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreLocator.ascx.cs" Inherits="Modules_AspxCommerce_AspxStoreLocator_slnew" %>

<%--<script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC8wFdXlPvTf7Te2bwBrtMYPTmF6qCmYw4">
    </script>--%>
<script type="text/javascript">
    //<![CDATA[
        var aspxCommonObj = {};
        var latitude = 0;
        var longitude = 0;
        var directionsDisplay;
        var directionsService = new google.maps.DirectionsService();
        var mymap;
        var markers = new Array();
        var geocoder;
        var from;
        var to;        

        function GetAllStores() {
            $("#ddlDistance").val(-1);
            $("#txtSearchAddress").val('');
            var mapOptions = {
                center: new google.maps.LatLng(-34.397, 150.644),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                }
            };
            var mymap = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

            $("#divMap").show();
            $("#divEnterAddress").show();
            $("#divDirection").hide();

            var shadow = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/shadow50.png',
                                        new google.maps.Size(37, 32),
                new google.maps.Point(0, 0),
                new google.maps.Point(0, 32));

            var bounds = new google.maps.LatLngBounds();
            var swBound = bounds.getSouthWest();
            var neBound = bounds.getNorthEast();
            var lngSpan = neBound.lng() - swBound.lng();
            var latSpan = neBound.lat() - swBound.lat();


            var sidebarstore = document.getElementById('sidebarstore');
            sidebarstore.innerHTML = '';

            var param = { aspxCommonObj: aspxCommonObj };
            $.ajax({
                type: "POST",
                async: false,
                url: aspxservicePath + "AspxCoreHandler.ashx/GetAllStoresLocation",
                data: JSON2.stringify(param),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var branchImage = '';
                    var length = msg.d.length;
                    if (length > 0 && msg.d != null) {
                        var item;
                        for (var index = 0; index < length; index++) {
                            item = msg.d[index];
                            var point = new google.maps.LatLng(item.Latitude, item.Longitude);
                            var image = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/marker' + (index + 1) + '.png',
                            // This marker is 20 pixels wide by 32 pixels tall.
                                new google.maps.Size(20, 32),
                            // The origin for this image is 0,0.or(9,34)
                                new google.maps.Point(0, 0),
                            // The anchor for this image is the base of the flagpole at 0,32.
                                new google.maps.Point(0, 32));
                            var infoHTML = item.StoreName + '<br>' + item.StoreDescription + '<br>' + item.StreetName + ' ,' + item.LocalityName + ' ,' + item.City + ' ,' + item.State + ', ' + item.Country + ', ' + item.ZIP;
                            branchImage = '<div class="cssClassImgwrapper"><img src="' + aspxRootPath + 'Modules/AspxCommerce/AspxStoreBranchesManagement/uploads/' + item.BranchImage + '" class="uploadImage" height="144px" width="256px"/></div>' + '<div class="cssClassStoreinfo">' + infoHTML + '</div>';
                            markers[index] = new google.maps.Marker({
                                position: point,
                                map: mymap,
                                shadow: shadow,
                                icon: image,
                                //                            shape: shape,
                                title: item.StoreName
                            });

                            markers[index].infowindow = new google.maps.InfoWindow({
                                content: branchImage
                            });

                            google.maps.event.addListener(markers[index], 'click', function () {
                                markers[index].infowindow.open(mymap, markers[index]);
                            });

                            markers[index].setMap(mymap);

                            var sidebarEntry = createSidebarEntry(markers[index], item.StoreName, item.StreetName, item.LocalityName, item.City, item.State, item.Country, item.ZIP, item.Distance, index);
                            sidebarstore.appendChild(sidebarEntry);
                            bounds.extend(point);

                        };
                        
                        console.log(bounds.getCenter());
                        var length = msg.d.length;
                        if (length > 1) {
                            $("#lblTotalResultCount").html('<b>' + length + ' ' + getLocale(AspxStoreLocator, 'Stores Found') + '</b><br/>');
                            mymap.setCenter(bounds.getCenter());
                            mymap.fitBounds(bounds);
                        } else if (length == 1) {
                            $("#lblTotalResultCount").html('<b>' + length + getLocale(AspxStoreLocator, 'Store Found') + '</b><br/>');
                            mymap.setCenter(bounds.getCenter());
                            mymap.setZoom(15);
                        }
                    } else {
                        $("#lblTotalResultCount").html("");
                        sidebarstore.innerHTML = getLocale(AspxStoreLocator, 'No results found.');

                        if (navigator.geolocation) {
                            navigator.geolocation.getCurrentPosition(function (position) {
                                initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                                mymap.setCenter(initialLocation);
                                mymap.setZoom(15);
                            }, function () {
                                // set center to New York,USA
                                map.setCenter(new google.maps.LatLng(40.69847032728747, -73.9514422416687));
                                map.setZoom(14);
                            });
                            // Browser doesn't support Geolocation
                        } else {
                            mymap.setCenter(new google.maps.LatLng(40.69847032728747, -73.9514422416687));
                            mymap.setZoom(14);
                        }
                        return;
                    }
                }
            });
        }

        function createSidebarEntry(marker, name, street, locality, City, State, Country, ZIP, distance, index) {
            var div = document.createElement('div');
            var html = '<b>' + (index + 1) + ') ' + name + '</b> (' + distance.toFixed(2) + ')<br/>' + street + ' ,' + locality + ' ,' + City + ' <br/>' + State + ' ,' + Country + ' ,' + ZIP;
            div.innerHTML = html;
            div.className = 'sidebar';
            google.maps.event.addDomListener(div, 'click', function () {
                google.maps.event.trigger(markers[index], 'click');
            });
         
            return div;
        }

        function GoogleMapSearchLocation() {
            var myOptions = {
                center: new google.maps.LatLng(-34.397, 150.644),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                }
            };

            mymap = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
            $("#divDirection").hide();

            directionsDisplay = new google.maps.DirectionsRenderer();
            geocoder = new google.maps.Geocoder();
            if (geocoder) {
                geocoder.geocode({ 'address': $("#txtSearchAddress").val() }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var shadow = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/shadow50.png',
                            new google.maps.Size(37, 32),
                            new google.maps.Point(0, 0),
                            new google.maps.Point(0, 32));

                        var bounds = new google.maps.LatLngBounds();
                        var swBound = bounds.getSouthWest();
                        var neBound = bounds.getNorthEast();
                        var lngSpan = neBound.lng() - swBound.lng();
                        var latSpan = neBound.lat() - swBound.lat();


                 
                        var searchPlaceMarker = new google.maps.Marker({
                            map: mymap,
                            title: $("#txtSearchAddress").val(),
                            position: results[0].geometry.location
                        });

                        var Searchinfowindow = new google.maps.InfoWindow({
                            content: getLocale(AspxStoreLocator, 'Your search location - ') + $("#txtSearchAddress").val()
                        });

                        google.maps.event.addListener(searchPlaceMarker, 'click', function () {
                            Searchinfowindow.open(mymap, searchPlaceMarker);
                        });
                        searchPlaceMarker.setMap(mymap);

                        bounds.extend(results[0].geometry.location);

                        latitude = results[0].geometry.location.lat();
                        longitude = results[0].geometry.location.lng();

                        var param = { latitude: latitude, longitude: longitude, searchDistance: $("#ddlDistance").val(), aspxCommonObj: aspxCommonObj };
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: aspxservicePath + "AspxCoreHandler.ashx/GetLocationsNearBy",
                            data: JSON2.stringify(param),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var length = msg.d.length;
                                if (length > 0) {

                                    var sidebarstore = document.getElementById('sidebarstore');
                                    sidebarstore.innerHTML = '';
                                    var item;
                                    for (var index = 0; index < length; index++) {
                                        item = msg.d[index];
                                        var point = new google.maps.LatLng(item.Latitude, item.Longitude);
                                        var image = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/marker' + (index + 1) + '.png',
                                            new google.maps.Size(20, 32),
                                            new google.maps.Point(0, 0),
                                            new google.maps.Point(0, 32));

                                        var infoHTML = item.StoreName + "<br>" + item.StoreDescription + "<br>" + item.StreetName + " ," + item.LocalityName + " ," + item.City + " ," + item.State + " ,"
                                            + item.Country + " ," + item.ZIP + ", "
                                                + "<li id=\"getDirection\"  onclick=\"calcRoute('" + $("#txtSearchAddress").val() + "','" + point + "');\"><div class=\"cssClassDirection\">" + getLocale(AspxStoreLocator, 'Get Direction') + "</div></li>"


                                        markers[index] = new google.maps.Marker({
                                            position: point,
                                            map: mymap,
                                            shadow: shadow,
                                            icon: image,
                                            title: item.StoreName
                                        });

                                        markers[index].infowindow = new google.maps.InfoWindow({
                                            content: infoHTML
                                        });

                                        google.maps.event.addListener(markers[index], 'click', function () {
                                            markers[index].infowindow.open(mymap, markers[index]);
                                        });

                                        markers[index].setMap(mymap);

                                        var sidebarEntry = createSidebarEntry(markers[index], item.StoreName, item.StreetName, item.LocalityName, item.City, item.State, item.Country, item.ZIP, item.Distance, index);
                                        sidebarstore.appendChild(sidebarEntry);
                                        bounds.extend(point);
                                        directionsDisplay.setMap(mymap);
                                        directionsDisplay.setPanel(document.getElementById("directionsPanel"));
                                    };
                                    $("#lblTotalResultCount").html('<b>' + length + getLocale(AspxStoreLocator, 'Store Found') + '</b><br/>');
                                    if (length > 0) {
                                        mymap.setCenter(bounds.getCenter());
                                        mymap.fitBounds(bounds);
                                    }
                                } else {
                                    $("#lblTotalResultCount").html("");
                                    $("#sidebarstore").html(getLocale(AspxStoreLocator, 'No results found.'));
                                    mymap.setCenter(results[0].geometry.location);
                                    mymap.setZoom(12);
                                    return;
                                }
                            },
                            error: function () {
                                alert(getLocale(AspxStoreLocator, "error"));
                            }
                        });
                        markers.push(searchPlaceMarker);
                    } else {
                        alert($("#txtSearchAddress").val() + getLocale(AspxStoreLocator, " is not found "));
                        $("#lblTotalResultCount").html("");
                        $("#sidebarstore").html(getLocale(AspxStoreLocator, 'No results found.'));
                        return;
                    }
                });
            }
        }

        function calcRoute(start, end) {
            $("#divDirection").show();
            var selectedMode = $("#mode").val();
            from = start;
            to = end;
            var request = {
                origin: from,
                destination: to,
                travelMode: google.maps.TravelMode[selectedMode]
            };
            directionsService.route(request, function (result, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(result);
                    var route = result.routes[0];
                    var summaryPanel = document.getElementById("directionsPanel");
                    summaryPanel.innerHTML = "";
                    // For each route, display summary information.
                    for (var i = 0; i < route.legs.length; i++) {
                        var routeSegment = i + 1;
                        summaryPanel.innerHTML += "<b>" + getLocale(AspxStoreLocator, "Route Segment: ") + routeSegment + "</b><br />";
                        summaryPanel.innerHTML += route.legs[i].start_address + " to ";
                        summaryPanel.innerHTML += route.legs[i].end_address + "<br />";
                        summaryPanel.innerHTML += route.legs[i].distance.text + "<br /><br />";
                    }
                }
            });

        }       
		$(document).ready(function () {
            aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            $(".sfLocale").localize({
                moduleKey: AspxStoreLocator
            });
            GetAllStores();

            $("#btnSubmitSearchAddress").click(function () {
                if ($("#txtSearchAddress").val() == null || $("#txtSearchAddress").val() == '') {
                    alert(getLocale(AspxStoreLocator, "Please enter the search address!"));
                    return;
                }

                if ($("#ddlDistance").val() != -1) {
                    GoogleMapSearchLocation();
                } else {
                    alert(getLocale(AspxStoreLocator, "Please select the distance!"));
                }
            });

            $("#btnShowAllStore").click(function () {
                GetAllStores();
            });
            $(".sidebar").on('click', function () {
                $(".sidebar").removeClass("selected");
                $(this).addClass("selected");
            });

            $("#mode").change(function () {
                calcRoute(from, to);
            });

            $("#txtSearchAddress").keyup(function (event) {
                if (event.keyCode == 13) {
                    $("#btnSubmitSearchAddress").click();
                }
            });
            $(".cssClassStore").click(function () {
                $(".cssClassStore").removeClass('selected');
                $(this).addClass("selected");
            });
        });	 
   
    //]]>   
</script>

<div id="divEnterAddress" visible="false">
    <div class="cssClassStoreSearch clearfix">
        <label class="cssClassLabel sfLocale">
            Enter full address :</label>
        <div class="cssStoreinput"><input type="text" class="sfInputbox" name="txtSearchAddress" id="txtSearchAddress" /></div>
        <label class="cssClassLabel sfLocale">
            Distance:</label><label class="cssClassLabel sfLocale">miles.</label>
        <select name="ddlDistance" class="sfListmenu" id="ddlDistance">
            <option value="-1" selected class="sfLocale">Choose One</option>
            <option value="5" class="sfLocale">5</option>
            <option value="10" class="sfLocale">10</option>
            <option value="20" class="sfLocale">20</option>
            <option value="30" class="sfLocale">30</option>
            <option value="50" class="sfLocale">50</option>
            <option value="100" class="sfLocale">100</option>
        </select>
        
        <div id="divFrontStoreButton" class="sfButtonwrapper cssClassFrontStoreButton">
            <label class="i-search cssClassCartLabel cssClassGreenBtn"><button type="button" id="btnSubmitSearchAddress" class="sfBtn">
                <span>Search</span></button></label>
            <label class="cssClassCartLabel cssClassDarkBtn i-arrow-right"><button type="button" id="btnShowAllStore" class="sfBtn">
                <span class="sfLocale">Show all Stores</span></button></label>
          
        </div>
        <asp:Label ID="lblError" runat="server" EnableViewState="False" ForeColor="Red"
            meta:resourcekey="lblErrorResource1"></asp:Label>
    </div>
    <div id="divMap" class="gbox clearfix">
       <div class="navwrap"> <div class="navBar">
                    <label class="cssClassLabel" id="lblTotalResultCount">
                    </label>
                    <div id="sidebarstore">
                    </div>
                </div>
                
     <div class="navRoute">
                    <div id="divDirection" visible="false">
                        <b class="sfLocale">Mode of Travel: </b>
                        <select id="mode" class="sfListmenu" name="mode">
                            <option value="DRIVING" selected="selected" class="sfLocale">Driving</option>
                            <option value="WALKING" class="sfLocale">Walking</option>
                            <option value="BICYCLING" class="sfLocale">Bicycling</option>
                        </select>
                        <br />
                        <div id="directionsPanel" style="float: right;height:100%">
                        </div>
                    </div>
                </div></div>
                <div class="navMap">
                    <div class="cssClassMapTxt"><label class="cssClassLabel">
                        Click on icon for more information.</label></div>
                    <div id="map_canvas">
                    </div>
                <div></div>
                
    </div>
</div>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreLocatorEdit.ascx.cs" Inherits="Modules_AspxCommerce_AspxStoreLocator_slEdit" %>

<script type="text/javascript">
    //<![CDATA[
    var aspxCommonObj = {};
    var EditStoreID;
    var markers = [];
    var geocoder;
    var map;
    var LocationID;
    var umi = '<%=UserModuleID%>';
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
        SelectFirstTab();

        $("#btnUpdate").click(function () {
            UpdateStoreLocation();
        });
        $(".cssClassStore").on('click', function () {
            $(".cssClassStore").removeClass("selected");
            $(this).addClass("selected");
        });

        $("#btnCancel").click(function () {
            GetAllStores();
        });
    });

    function SelectFirstTab() {
        var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
        $tabs.tabs('option', 'active', 0);
    }

    function GetAllStores() {
        $("#lblHelp").html("<b>" + getLocale(AspxStoreLocator, "Click on icon for more information.") + "</b>");
        $("#divMap").show();
        $("#divStoreEdit").hide();
        var myOptions = {
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL
            }
        };

        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        $("#divMap").show();
        $("#divEnterAddress").show();

        var shadow = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/shadow50.png',
        // The shadow image is larger in the horizontal dimension
        // while the position and offset are the same as for the main image.
                  new google.maps.Size(37, 32),
                  new google.maps.Point(0, 0),
                  new google.maps.Point(0, 32));

        var bounds = new google.maps.LatLngBounds();
        var swBound = bounds.getSouthWest();
        var neBound = bounds.getNorthEast();
        var lngSpan = neBound.lng() - swBound.lng();
        var latSpan = neBound.lat() - swBound.lat();

        //map.clearOverlays();
        if (markers) {
            for (i in markers) {
                markers[i].setMap(null);
            }
            markers.length = 0;
        }

        var sidebarstore = document.getElementById('sidebarstore');
        sidebarstore.innerHTML = '';

        var param = { aspxCommonObj: aspxCommonObj };
        $.ajax({
            type: "POST", beforeSend: function (request) {
                request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                request.setRequestHeader("UMID", umi);
                request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                request.setRequestHeader("PType", "v");
                request.setRequestHeader('Escape', '0');
            },
            url: aspxservicePath + "AspxCoreHandler.ashx/GetAllStoresLocation",
            data: JSON2.stringify(param),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var length = msg.d.length;
                if (length > 0 && msg.d != null) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        var imagePath = aspxRootPath + "Modules/AspxCommerce/AspxStoreBranchesManagement/uploads/" + item.BranchImage;
                        var point = new google.maps.LatLng(item.Latitude, item.Longitude);
                        var image = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/marker' + (index + 1) + '.png',
                                      new google.maps.Size(20, 32), new google.maps.Point(0, 0), new google.maps.Point(0, 32));
                        var infoHTML = '<div class="cssClassImgwrapper"><img class="cssBranchImage" src=' + imagePath + '  alt="' + item.StoreName + '" title="' + item.StoreName + '"/></div><div class="cssClassStoreinfo">' + item.StoreName + '<br>' + item.StoreDescription + '<br>' + item.StreetName + ' ,' + item.LocalityName + ' ,' + item.City + ' ,'
                                        + item.State + ', ' + item.Country + ', ' + item.ZIP + '</div>';

                        LocationID = item.LocationID;
                        markers[index] = new google.maps.Marker({
                            position: point,
                            map: map,
                            shadow: shadow,
                            icon: image,
                            //                            shape: shape,
                            title: item.StoreName
                        });

                        markers[index].infowindow = new google.maps.InfoWindow({
                            content: infoHTML
                        });

                        google.maps.event.addListener(markers[index], 'click', function () {
                            markers[index].infowindow.open(map, markers[index]);
                        });

                        markers[index].setMap(map);

                        var storeName = '';
                        var storeDescription = '';
                        var streetName = '';
                        var localityName = '';
                        var city = '';
                        var state = '';
                        var country = '';
                        var zip = '';
                        if (item.StoreName != '') {
                            storeName = item.StoreName;
                        } else { storeName = null; }
                        if (item.StoreDescription != '') {
                            storeDescription = item.StoreDescription;
                        } else { storeDescription = null; }
                        if (item.StreetName != '') {
                            streetName = item.StreetName;
                        } else { streetName = null; }
                        if (item.LocalityName != '') {
                            localityName = item.LocalityName;
                        } else { localityName = ''; }
                        if (item.City != '') {
                            city = item.City;
                        } else { city = null; }
                        if (item.State != '') {
                            state = item.State;
                        } else { state = null; }
                        if (item.Country != '') {
                            country = item.Country;
                        } else { country = null; }
                        if (item.ZIP != '') {
                            zip = item.ZIP;
                        } else { zip = null; }
                        //var sidebarEntry = createSidebarEntry(markers[index], item.StoreName, item.StoreDescription, item.StreetName, item.LocalityName, item.City, item.State, item.Country, item.ZIP, item.StoreID, item.PortalID, index);
                        var sidebarEntry = createSidebarEntry(markers[index], storeName, storeDescription, streetName, localityName, city, state, country, zip, aspxCommonObj.StoreID, aspxCommonObj.PortalID, index);

                        sidebarstore.appendChild(sidebarEntry);
                        bounds.extend(point);
                    };
                    $("#lblTotalResultCount").html('<h4>' + length + '&nbsp' + getLocale(AspxStoreLocator, 'Stores Found') + '</h4><br/>');
                    if (length > 1) {
                        map.setCenter(bounds.getCenter());
                        map.fitBounds(bounds);
                    }
                    else if (length == 1) {
                        map.setCenter(bounds.getCenter());
                        map.setZoom(15);
                    }
                }
                else {
                    $("#lblTotalResultCount").html("");
                    sidebarstore.innerHTML = getLocale(AspxStoreLocator, 'No results found.');
                    if (navigator.geolocation) {
                        navigator.geolocation.getCurrentPosition(function (position) {
                            initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                            map.setCenter(initialLocation);
                            map.setZoom(15);
                        }, function () {
                            // set center to New York,USA
                            map.setCenter(new google.maps.LatLng(40.69847032728747, -73.9514422416687));
                            map.setZoom(14);
                        });
                        // Browser doesn't support Geolocation
                    } else {
                        map.setCenter(new google.maps.LatLng(40.69847032728747, -73.9514422416687));
                        map.setZoom(14);
                    }
                    return;
                }
            },
            error: function () {
                //alert("error");
                csscody.error("<h2>" + getLocale(AspxStoreLocator, "Error Message") + "</h2><p>" + getLocale(AspxStoreLocator, "Error occured!!") + "</p>");
            }
        });
    }

    function createSidebarEntry(marker, name, description, street, Locality, City, State, Country, ZIP, StoreID, PortalID, index) {
        var STREET = '';
        var LOCALITY = '';
        var CITY = '';
        var STATE = '';
        var COUNTRY = '';
        var ZIP2 = '';
        var Desc = '';
        if (Locality == null || Locality == 'null')
        { LOCALITY = ''; }
        else { LOCALITY = Locality; }
        if (street == null || street == 'null' || LocationID == 1)
        { STREET = ''; }
        else { STREET = street; }
        if (City == null || City == 'null')
        { CITY = ''; }
        else { CITY = City; }
        if (State == null || State == 'null')
        { STATE = ''; }
        else { STATE = State; }
        if (Country == null || Country == 'null')
        { COUNTRY = ''; }
        else { COUNTRY = Country; }
        if (description == null || description == 'null') {
            Desc = '';
        }
        else {
            Desc = description;
        }
        if (ZIP == null || ZIP == 'null')
        { ZIP2 = ''; }
        else { ZIP2 = ZIP; }
        var div = document.createElement('div');
        div.className = "cssClassStore";
        var html = '<b>' + (index + 1) + ') ' + name + '</b> <br/>' + description + ',' + STREET + ' ,' + LOCALITY + ' ,' + CITY + ' <br/> ' + STATE + ' ,' + COUNTRY + ' ,' + ZIP2;
        html += "<br/>";

        html += " <div class=\"cssClassEditStore sfButtonwrapper\"><button class='sfBtn' type=\"button\" onclick=\"UpdateLocation('" + name + "','" + Desc + " ','" + street + " ','" + LOCALITY + " ','"
            + CITY + "','" + STATE + "','" + COUNTRY + "', " + ZIP + "," + StoreID + "," + index + ");\" ><span  class='icon-edit'>" + getLocale(AspxStoreLocator, "Edit Store") + "</span></button></div>";


        div.innerHTML = html;
        google.maps.event.addDomListener(div, 'click', function () {
            google.maps.event.trigger(markers[index], 'click');
        });

        return div;
    }

    function UpdateLocation(name, description, street, LocalityName, City, State, Country, ZIP, StoreID, index) {
        EditStoreID = StoreID;
        $("#lblTotalResultCount").html("");
        $("#lblHelp").html("<b>" + getLocale(AspxStoreLocator, "Drag the marker to desired location to set the location there") + "</b>");
        $("#divStoreEdit").show();

        $("#txtLatitude").val(markers[index].getPosition().lat());
        $("#txtLongitude").val(markers[index].getPosition().lng());


        if (City != 'null') {
            $.trim($("#txtCity").val(City));
        }
        else { $("#txtCity").val(''); }
        if (State != 'null') {
            $.trim($("#txtState").val(State));
        }
        else { $("#txtState").val(''); }
        if (Country != 'null') {
            $.trim($("#txtCountry").val(Country));
        }
        else { $("#txtCountry").val(''); }
        if (ZIP != '0') {
            $.trim($("#txtZIP").val(ZIP));
        }
        else { $("#txtZIP").val(''); }
        if (name != 'null') {
            $.trim($("#txtStoreName").val(name));
        }
        else { $("#txtStoreName").val(''); }
        if (description != 'null') {
            $.trim($("#txtStoreDescription").val(description));
        }
        else { $("#txtStoreDescription").val(''); }

        $("#txtLocalityName").val(LocalityName);
        $("#txtStreet").val(street);
        $("#txtCity").val(City);
        $("#txtState").val(State);
        $("#txtCountry").val(Country);
        $("#txtZIP").val(ZIP);

        var point = markers[index].getPosition();
        map.setCenter(point);
        map.setZoom(15);
        geocoder = new google.maps.Geocoder();

        var shadow = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/shadow50.png', new google.maps.Size(37, 32), new google.maps.Point(0, 0), new google.maps.Point(0, 32));

        if (markers) {
            for (i in markers) {
                if (markers[i] != markers[index]) {
                    markers[i].setMap(null);
                    markers[i].infowindow.close();
                }
            }
        }
        markers[index].infowindow.open(map, markers[index]);
        markers[index].setDraggable(true);

        var sidebarstore = document.getElementById('sidebarstore');
        sidebarstore.innerHTML = '';

        var sidebarEntry = createSidebarEntry(markers[index], name, description, street, LocalityName, City, State, Country, ZIP, aspxCommonObj.StoreID, aspxCommonObj.PortalID, index);
        sidebarstore.appendChild(sidebarEntry);
        $(".cssClassEditStore").hide();

        var image = new google.maps.MarkerImage(aspxRootPath + 'images/Markers/marker.png',
                                         new google.maps.Size(20, 32),
                                         new google.maps.Point(0, 0),
                                         new google.maps.Point(0, 32));

        google.maps.event.addListener(markers[index], "dragstart", function () {
            markers[index].infowindow.close();
        });

        google.maps.event.addListener(markers[index], "dragend", function () {
            geocoder.geocode({ 'latLng': markers[index].getPosition() }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        var j;
                        street = '';
                        LocalityName = '';
                        ZIP = '';
                        City = '';
                        State = '';

                        for (j = 0; j <= 7; j++) {
                            if (results[0].address_components[j] != undefined) {

                                if (results[0].address_components[j].types[0] == 'street_number') {
                                    street = results[0].address_components[j].long_name;
                                    if (results[0].address_components[j + 1].types[0] == 'route') {
                                        street = street + ' ' + results[0].address_components[j + 1].long_name;
                                    }
                                }
                                else if (results[0].address_components[j].types[0] == 'route') {
                                    if (street == '') {
                                        street = results[0].address_components[j].long_name;
                                    }
                                }
                                else if (results[0].address_components[j].types[0] == 'sublocality') {
                                    LocalityName = results[0].address_components[j].long_name;
                                }
                                else if (results[0].address_components[j].types[0] == 'locality') {
                                    if (LocalityName == '') {
                                        LocalityName = results[0].address_components[j].long_name;
                                    }
                                    else {
                                        City = results[0].address_components[j].long_name;
                                    }
                                }
                                else if (results[0].address_components[j].types[0] == 'administrative_area_level_2') {
                                    if (City == '') {
                                        City = results[0].address_components[j].long_name;
                                    }
                                }
                                else if (results[0].address_components[j].types[0] == 'administrative_area_level_1') {
                                    State = results[0].address_components[j].long_name;
                                }
                                else if (results[0].address_components[j].types[0] == 'country') {
                                    Country = results[0].address_components[j].long_name;
                                }
                                else if (results[0].address_components[j].types[0] == 'postal_code') {
                                    ZIP = results[0].address_components[j].long_name;
                                }
                            }
                        }
                        markers[index].infowindow.setContent(results[0].formatted_address);
                        markers[index].infowindow.open(map, markers[index]);

                        $("#txtLatitude").val(markers[index].getPosition().lat());
                        $("#txtLongitude").val(markers[index].getPosition().lng());

                    }
                } else {
                    alert(getLocale(AspxStoreLocator, "Geocoding failed due to:") + status);
                }
            });
        });

        google.maps.event.addListener(markers[index], "click", function () {
            geocoder.geocode({ 'latLng': markers[index].getPosition() }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        markers[index].infowindow.open(map, markers[index]);
                    }
                } else {
                    alert(getLocale(AspxStoreLocator, "Geocoding failed due to:") + status);
                }
            });
        });
    }
    function UpdateStoreLocation() {
        var storeName = $.trim($("#txtStoreName").val());
        if (storeName != '') {
            aspxCommonObj.StoreID = EditStoreID;
            var param = { aspxCommonObj: aspxCommonObj, storeName: storeName, storeDescription: $.trim($("#txtStoreDescription").val()), streetName: $.trim($("#txtStreet").val()), localityName: $.trim($("#txtLocalityName").val()), city: $.trim($("#txtCity").val()), state: $.trim($("#txtState").val()), country: $.trim($("#txtCountry").val()), zip: $.trim($("#txtZIP").val()), latitude: $.trim($("#txtLatitude").val()), longitude: $.trim($("#txtLongitude").val()) };
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/UpdateStoreLocation",
                data: JSON2.stringify(param),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {
                        csscody.info("<h2>" + getLocale(AspxStoreLocator, "Successful Message") + "</h2><p>" + getLocale(AspxStoreLocator, "Location has been updated successfully.") + "</p>");
                        GetAllStores();
                    }
                },
                error: function () {
                    csscody.error("<h2>" + getLocale(AspxStoreLocator, "Error Message") + "</h2><p>" + getLocale(AspxStoreLocator, "Sorry !! Failed to update location.") + "</p>");
                }
            });
        }
        else {
            $('#nameErrorLabel').html(getLocale(AspxStoreLocator, 'Please mention store name!')).css("color", "red");
            SelectFirstTab();
            return false;
        }
    }

    //]]>
</script>

<h1 class="sfLocale">Store Locator Management</h1>

<div id="divMap" class="sfFormwrapper">
    <table cellpadding="4" cellspacing="8" width="100%">
        <tr>
            <td style="vertical-align: top;" width="28%">
                <label class="cssClassLabel" id="lblTotalResultCount"></label>
                <div id="sidebarstore" class="cssClassStoreSideBar"></div>
            </td>
            <td valign="top" width="70%">
                <div class="cssClassCommonBox Curve" id="divStoreEdit" visible="false">
                    <%--<div class="cssClassHeader">
        <h2>
            <label id="lblAttrFormHeading" class="cssClassLabel">
                Store Information</label>
        </h2>
    </div>--%>
                    <div class="cssClassTabPanelTable">
                        <div id="container-7">
                            <ul>
                                <li><a href="#fragment-1">
                                    <asp:Label ID="lblTabTitle1" runat="server" Text="General Information"
                                        meta:resourcekey="lblTabTitle1Resource1"></asp:Label>
                                </a></li>
                                <li><a href="#fragment-2">
                                    <asp:Label ID="lblTabTitle2" runat="server" Text="Location Information"
                                        meta:resourcekey="lblTabTitle2Resource1"></asp:Label>
                                </a></li>
                            </ul>
                            <div id="fragment-1">
                                <div class="sfFormwrapper">
                                    <div id="divStoreGeneralInfo">
                                        <div class="cssClassCommonBox Curve">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">Store Name:</label>
                                                        <span class="cssClassRequired">*</span></td>
                                                    <td>
                                                        <input type="text" id="txtStoreName" class="sfInputbox" disabled="disabled" />
                                                        <span id="nameErrorLabel"></span></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">Store Description:</label></td>
                                                    <td>
                                                        <input type="text" id="txtStoreDescription" class="sfInputbox" /></td>
                                                </tr>
                                            </table>
                                            <div class="log"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="fragment-2">
                                <div class="sfFormwrapper">
                                    <div id="divAddressDetails">
                                        <div class="cssClassCommonBox Curve">
                                            <table width="100%" cellpadding="4" cellspacing="8">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblLatitude" runat="server" Text="Latitude:"
                                                            CssClass="cssClassLabel" meta:resourcekey="lblLatitudeResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtLatitude" type="text" name="latitude" class="sfInputbox " /></td>
                                                    <td>
                                                        <asp:Label ID="lblLongitude" runat="server" Text="Longitude:"
                                                            CssClass="cssClassLabel" meta:resourcekey="lblLongitudeResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtLongitude" type="text" name="longitude" class="sfInputbox " /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblStreet" runat="server" Text="Street:"
                                                            CssClass="cssClassLabel" meta:resourcekey="lblStreetResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtStreet" type="text" name="street" class="sfInputbox " /></td>
                                                    <td>
                                                        <asp:Label ID="lblLocalityName" runat="server" Text="Locality Name:"
                                                            CssClass="cssClassLabel" meta:resourcekey="lblLocalityNameResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtLocalityName" type="text" name="localityName" class="sfInputbox " /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="cssClassLabel"
                                                            meta:resourcekey="lblCityResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtCity" type="text" name="city" class="sfInputbox " /></td>
                                                    <td>
                                                        <asp:Label ID="lblState" runat="server" Text="State:" CssClass="cssClassLabel"
                                                            meta:resourcekey="lblStateResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtState" type="text" name="state" class="sfInputbox " /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCountry" runat="server" Text="Country:"
                                                            CssClass="cssClassLabel" meta:resourcekey="lblCountryResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtCountry" type="text" name="country" class="sfInputbox " /></td>
                                                    <td>
                                                        <asp:Label ID="lblZip" runat="server" Text="ZIP:" CssClass="cssClassLabel"
                                                            meta:resourcekey="lblZipResource1"></asp:Label></td>
                                                    <td>
                                                        <input id="txtZIP" type="text" name="zip" class="sfInputbox " /></td>
                                                </tr>
                                            </table>
                                            <%--<div class="loading">
                                        <img src="<%=ResolveUrl("~/")%>Templates/AspxCommerce/images/ajax-loader.gif" />
                                    </div>--%>
                                            <div class="log"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="sfButtonwrapper">
                                <p>
                                    <button type="button" id="btnUpdate" class="sfBtn"><span class="sfLocale icon-save">Save</span></button>
                                    <button type="button" id="btnCancel" class="sfBtn"><span class="sfLocale icon-close">Cancel</span></button>
                                </p>
                                <div class="cssClassClear"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />


                <label class="cssClassLabel sfLocale" id="lblHelp"><b>Click on icon for more information.</b></label>
                <div id="map_canvas" style="width: 100%; height: 600px"></div>
            </td>
        </tr>
    </table>
</div>


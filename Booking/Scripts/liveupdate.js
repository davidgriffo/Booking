﻿(function() {

    var url = "http://roomsrest.azurewebsites.net/api/Room/Search";
    var element = $("#test1");


    var token = $("#tokenDiv").data('value');
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    var ajax = null;

    $(".searchTarget .datepicker, .textinput")
        .change(function() {
            if (ajax != null) {
                ajax.abort();
            };
            var data = {
                startDate: $("#startDate").val(),
                endDate: $("#endDate").val(),
                department: $("#department :selected").val(),
                capacity: $("#capacity").val(),
                equipment: $("#equipment :selected")
            }

            element.empty();
            setRow(data);
        });


    function setRow(searchData) {

        var selectedItemString = "";
        $.each(searchData.equipment,
            function(i, v) {
                selectedItemString += '&selectedEquipment[' + i + ']=' + $(this).val();
            });

        var finishedUrl = url +
            '?startDate=' +
            searchData.startDate +
            '&endDate=' +
            searchData.endDate +
            '&capacity=' +
            searchData.capacity +
            '&departmentId=' +
            searchData.department +
            selectedItemString;
            

        $.ajax({
                type: 'GET',
                url: finishedUrl,
                headers: headers
            })
            .done(function(json) {

                console.log(finishedUrl);

                //Convert the data to an json object.
                var data = json;

                $.each(data,
                    function(index, value) {
                        var item = "";
                        $.each(value.Equipment,
                            function(i, v) {
                                item += '<p>' + v.Name + '<p>';
                            });

                        element.append(
                            '<div class="row" id="roomRow"><div class="col-md-2"><p><b>Lokale:</b>' +
                            value.Name +
                            '</p><p><b>Afdeling:</b> ' +
                            value.Department.Name +
                            '</p></div><div class="col-md-4"><p><b>Antal personer:</b>' +
                            value.Capacity +
                            '</p><p><b>Beskrivelse:</b>' +
                            value.Description +
                            '</p></div><div class="col-md-4"><p><b>Udstyr:</b>' +
                            item +
                            '</p></div><div class="col-md-2">  <a class="glyphicon glyphicon-calendar" href="/SelectedBooking/Index/' +
                            value.Id +
                            '" id="btnBook"> </a>    </div>'
                        );
                    });

            })
            .fail(function(jqxhr, textStatus, error) {
                alert(error);
            });
    }
})();
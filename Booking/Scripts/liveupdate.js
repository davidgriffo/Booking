(function () {

    var url = "http://roomsrest.azurewebsites.net/api/Room";
    var element = $("#test1");
    var ajax = null;

    $(".searchTarget").change(function () {
        if (ajax != null) {
            ajax.abort();
        };
        var data = {
            startDate: $("#startDate").value,
            endDate: $("#endDate").value,
            department: $("#department").value,
            capacity: $("#capacity").value,
            equipment: $("#equipment").value
            
        }
        element.empty();
        setRow(data);
    });

    function setRow(searchData) {
        ajax = $.getJSON(url, {}).done(function (json) {

            //Convert the data to an json object.
            var data = json;

            $.each(data, function (index, value) {

                var item;

                $.each(value.Equipment,
                    function (i, v) {
                        item += '<p>' + v.Name + '<p>';
                    });

                element.append(
                    '<div class="row" id="roomRow"><div class="col-md-2"><p><b>Lokale:</b>' + value.Name + '</p><p><b>Afdeling:</b> ' + value.Department + '</p></div><div class="col-md-4"><p><b>Antal personer:</b>' + value.Capacity + '</p><p><b>Beskrivelse:</b>' + value.Description + '</p></div><div class="col-md-4"><p><b>Udstyr:</b>' + item + '</p><div class="col-md-2"></div></div>'
            );
            });

        }).fail(function (jqxhr, textStatus, error) {
            alert(error);
        });
    }
})();

$(document).ready(function () {

    function searchRepo() {
        const searchValue = $("#search").val();

        if (searchValue === "") {
            return;
        }

        $(".table").hide();
        $("#loader").show();
        $("#no-results").hide();

        $.get(`https://api.github.com/search/repositories?q=${searchValue}`,
            function (data) {
                const res = data.items;
                var ids = [];
                var htmlUrls = [];

                $("#loader").hide();
                $("#repositories").html("");

                if (res.length == 0) {
                    $("#no-results").show();
                    return;
                }

                $(".table").show();

                $.each(res,
                    function (index, value) {
                        $("#repositories").append("<tr><td scope='row'>" +
                            index +
                            "</td>" +
                            "<td>" +
                            "<a href ='" + value.html_url + "' target='_blank'>" + value.name + "</a>" +
                            "</td>" +
                            "<td> <img src='" +
                            value.owner.avatar_url +
                            "' width='32' height='32' /></td >" +
                            "<td>" +
                            value.owner.login +
                            "</td>" +
                            "</tr>");

                        ids.push(value.id);
                        htmlUrls.push(value.html_url);
                    });

                $.post("/Home/SaveRepos",
                    { ids: ids, htmlUrls: htmlUrls },
                    function (data) {
                        if (data === false)
                            alert("An error occurred!");
                    });

            });
    }

    $("#search").keydown(function(e) {
        if (e.which === 13)
            searchRepo();
        if ($("#search").val().length === 0)
            $("#SearchButton").prop("disabled", true);
        else 
            $("#SearchButton").prop("disabled", false);
    });

    $("#SearchButton").click(function () {
        searchRepo();
    });
});
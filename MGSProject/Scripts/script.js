$(document).ready(function() {

    $("#search").keydown(function(e) {
        if (e.which === 13)
            goToController();
        if ($("#search").val().length === 0)
            $("#SearchButton").prop("disabled", true);
        else
            $("#SearchButton").prop("disabled", false);
    });

    $("#SearchButton").click(function() {
        goToController();
    });


    function goToController() {
        const searchValue = $("#search").val();

        if (searchValue === "") {
            return;
        }

        $("#loader").show();
        $(".table").hide();

        $.post("/Home/SendJsonToClient",
            { repoName: searchValue },
            function(jsonData) {
                if (jsonData == null) {
                    alert("Error!");
                } else {
                    $(".table").show();
                    $("#no-results").hide();
                    $("#loader").hide();
                    $("#repositories").html("");
                    const res = JSON.parse(jsonData).items;

                    if (res.length == 0) {
                        $("#no-results").show();
                        return;
                    }

                    $.each(res,
                        function(index, value) {
                            $("#repositories").append(
                                `<tr><td scope='row'>${index}</td><td><a href ='${value.html_url}' target='_blank'>${
                                value.name}</a></td><td> <img src='${value.owner.avatar_url
                                }' width='32' height='32' /></td ><td>${value.owner.login}</td></tr>`);
                        });
                }
            });
        $.get("/Home/SaveToDatabase2");
    }
});
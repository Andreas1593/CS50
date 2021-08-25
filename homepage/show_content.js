// Run script once DOM is loaded //
document.addEventListener("DOMContentLoaded", function() {

    // When button is clicked, show content
    let button = document.querySelector("#button");
    button.addEventListener("click", function() {

        /* Picture and text on the right */
        var x = document.getElementsByClassName("emmy2");
        if (x[0].style.display === "none") {
            x[0].style.display = "block";
            x[1].style.display = "block";
        } else {
            x[0].style.display = "none";
            x[1].style.display = "none";
        }

        /* Picture and text on the right */
        var y = document.getElementsByClassName("emmy3");
        if (y[0].style.display === "none") {
            y[0].style.display = "block";
            y[1].style.display = "block";
        } else {
            y[0].style.display = "none";
            y[1].style.display = "none";
      }
    });
});
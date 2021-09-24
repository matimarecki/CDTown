var modal = document.getElementById("myModal");
var btn = document.getElementById("modalBtn");
var span = document.getElementsByClassName("close")[0];
btn.onclick = function () {
    modal.style.display = "block";
}
span.onclick = function (event) {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target === modal){
        modal.style.display = "none";
    }
}
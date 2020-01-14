(function (cibertec) {
    cibertec.Index = {
        currentYear: function () {
            var today = new Date();
            return today.getFullYear();
        }
    };
    document.getElementById("date").innerHTML = cibertec.Index.currentYear();
})(window.cibertec = window.cibertec || {});

/*Forma 2:*/
/*
window.onload = function (cibertec) {
    cibertec.Index = {
        currentYear: function () {
            var today = new Date();
            return today.getFullYear();
        }
    };
    document.getElementById("date").innerHTML = cibertec.Index.currentYear();
};
*/

/*Forma3:*/
/*
window.load = MiFuncion;
function MiFuncion(cibertec) {
    cibertec.Index = {
        currentYear: function () {
            var today = new Date();
            return today.getFullYear();
        }
    };
    document.getElementById("date").innerHTML = cibertec.Index.currentYear();
};
*/

/*Con JQuery - Forma 1*/
/*$(document).ready(function (cibertec) {
cibertec.Index = {
    currentYear: function () {
        var today = new Date();
        return today.getFullYear();
    }
};
document.getElementById("date").innerHTML = cibertec.Index.currentYear();
});
*/


/*Con JQuery - Forma 2*/
/*$(document).ready(MiFuncion);
  
function Mifuncion (cibertec) {
    cibertec.Index = {
        currentYear: function () {
            var today = new Date();
            return today.getFullYear();
        }
    };
    document.getElementById("date").innerHTML = cibertec.Index.currentYear();
};
*/

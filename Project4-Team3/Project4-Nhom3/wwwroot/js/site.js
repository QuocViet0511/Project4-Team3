// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    var list = document.getElementsByClassName("date-format");
    
    for (let i = 0; i < list.length; i++) {
        var item = list[i].innerHTML;
        list[i].innerHTML = item.substring(0, item.lastIndexOf("/") + 5);
    }

    var list2 = document.getElementsByClassName("date-format2");

    for (let i = 0; i < list2.length; i++) {
        var item = list[i].innerHTML;
        list[i].innerHTML = item.substring(0, item.lastIndexOf("/") + 8);
    }
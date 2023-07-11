"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/cartHub").build();
//Disable send button until connection is established
//document.getElementById("sendButton").disabled = false;
connection.on("ShowQuantity", function (list) {
    for (var i in list) {
        var stock = $(`input[value=${parseInt(list[i].id)}]+ .stock`)
        stock.text("In stock: " + (parseInt(stock.text().slice(10)) - parseInt(list[i].number)))
    }
});
connection.start();
$("#checkout").click((e) => {
    e.preventDefault();
    let check = true;
    var cookieList = [];
    var getCookie = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (getCookie) {
        cookieList = JSON.parse(decodeURIComponent(getCookie));
    }
    $.get("/Detail", {
        handler: "Quantity"
    }, function (data) {
        var list = data.items
        for (var i in list) {
            if (list[i].number - cookieList[i].number < 0) {
                check = false;
                ShowAlert("yellow", "Warning", "Quantity of " + list[i].name + " is more than unit in stock")
            }
        }
        if (check) {
            $("#checkoutForm").submit()
        }
        connection.invoke("ChangeQuantity", cookieList).catch(function (err) {
            return console.error(err.toString());
        });
    });
})

function ShowAlert(attr, title, mess) {
    var alertList = $(".alert-list");
    var el = document.createElement("div");
    el.classList.add("flex")
    el.classList.add("justify-end")
    el.innerHTML = (`<div class="alert flex p-3 w-fit mb-2 text-sm text-white border border-${attr}-300 rounded-lg bg-${attr}-50 dark:bg-${attr}-700 dark:text-green-400 dark:border-green-800" role="alert">
  <svg aria-hidden="true" class="flex-shrink-0 inline w-5 h-5 mr-3" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clip-rule="evenodd"></path></svg>
  <span class="sr-only">Info</span>
  <div>
    <span class="font-medium">${title} alert!</span> ${mess}
  </div>
</div>
</div>`);
    alertList.append(el);
    setTimeout(() => {
        $(el).remove();
    }, 3500);
}

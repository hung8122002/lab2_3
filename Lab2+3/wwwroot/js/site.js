// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
var qualityInput = $("#quantity");
var increase = $("#increase");
var decrease = $("#decrease");
var addToCart = $("#addToCart");
var cookieList = [];
var getCookie = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
if (getCookie) {
    cookieList = JSON.parse(decodeURIComponent(getCookie));
    for (var i = 0; i < cookieList.length; i++) {
        addItem(cookieList[i].imageLink, cookieList[i].name, cookieList[i].number, cookieList[i].id, cookieList[i].price)
    }
}

getTotalPrice()

increase.click(() => {
    if (parseInt(qualityInput.val()) < 99) {
        qualityInput.val(parseInt(qualityInput.val()) + 1);
    }
})

decrease.click(() => {
    if (parseInt(qualityInput.val()) > 1) {
        qualityInput.val(parseInt(qualityInput.val()) - 1);
    }
})

qualityInput.on("input", () => {
    if (qualityInput.val() > 99) {
        qualityInput.val(99)
    }
    if (qualityInput.val() < 1) {
        qualityInput.val(1)
    }
})

$(".checkout").click((e) => {
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
        if (check && cookieList.length) {
            window.location.href = "/checkout";
        }
    });
})

addToCart.click(() => {
    var check = checkItem($(".productId").val())

    if (check) {
        let quantity = parseInt($("#quantity").val()) + parseInt(check.number)
        if (quantity > 99) {
            quantity = 99
        }
        if (quantity > $(".stock").text().slice(10)) {
            ShowAlert("yellow", "Warning", "Quantity is more than unit in stock")
            return
        }
        $(`.cart-list .Total${check.id}`).text("$" + quantity * parseFloat($(`.cart-list .Price${check.id}`).text().slice(1)))
        updateCookie($(".productId").val(), $(".productName").text(), $(".imageLink").attr("src"), $(".productPrice").text(), $("#quantity").val(), false)
        $(`.cart-list :input[value='${check.id}'].itemId + .quantityCart`).val(quantity)
        getTotalPrice()
        ShowAlert("green", "Success", "Add to cart successfully")
        return
    }

    addItem($(".imageLink").attr("src"), $(".productName").text(), $("#quantity").val(), $(".productId").val(), $(".productPrice").text())
    updateCookie($(".productId").val(), $(".productName").text(), $(".imageLink").attr("src"), $(".productPrice").text(), $("#quantity").val(), false)
    getTotalPrice()
    ShowAlert("green", "Success", "Add to cart successfully")
})

function updateCookie(id, name, imageLink, price, number, fromCart) {
    var cartList = [];
    var matches = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (matches) {
        cartList = JSON.parse(decodeURIComponent(matches));

        var find = cartList.find(x => x.id == id)
        if (find) {
            find.number = parseInt(find.number) + parseInt(number);
            if (parseInt(find.number) > 99) {
                find.number = 99;
            }
            if (fromCart == true) {
                find.number = number;
            }
            find.number = find.number.toString()
            document.cookie = "cart=" + encodeURIComponent(JSON.stringify(cartList)) + ";path=/"
            return
        }
    }
    var item = {
        id,
        name,
        imageLink,
        price,
        number
    }
    cartList.push(item)
    document.cookie = "cart=" + encodeURIComponent(JSON.stringify(cartList)) + "; path=/;";
}

function removeCookie(id) {
    var cartList = [];
    var matches = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (matches) {
        cartList = JSON.parse(decodeURIComponent(matches));
        var find = cartList.filter(x => x.id != id)
        if (find.length) {
            document.cookie = "cart=" + JSON.stringify(find) + ";path=/"
        } else {
            document.cookie = "cart=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        }
    }
}

function checkItem(id) {
    var matches = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (matches) {
        var cartList = JSON.parse(decodeURIComponent(matches));
        var find = cartList.find(x => x.id == id)
        return find
    }
    return false
}

function addItem(imageLink, name, quantity, id, price) {
    var cartList = document.createElement("li");
    $(cartList).addClass("flex items-center gap-4");
    cartList.innerHTML = (`<img
              src="${imageLink}"
              alt=""
              class="h-16 w-16 rounded object-cover"
            />

            <div>
              <a href="/Detail/${id}" class="text-sm font-bold text-gray-900">${name}</a>

              <dl class="mt-0.5 space-y-px text-[10px] text-gray-600">
                <div>
                  <dt class="inline">Price:</dt>
                  <dd class="Price${id} inline">${price}</dd>
                </div>

                <div>
                  <dt class="inline">Total:</dt>
                  <dd class="Total${id} inline">$${parseFloat(price.slice(1)) * parseInt(quantity)}</dd >
                </div>
              </dl>
            </div>

            <div class="flex flex-1 items-center justify-end gap-2">
              <div>
                <label for="Quantity" class="sr-only"> Quantity </label>

                <div class="flex items-center gap-1">
                  <button
                    class="decrease"
                    type="button"
                    class="w-7 h-10 leading-10 text-gray-600 transition hover:opacity-75"
                  >
                    &minus;
                  </button>

                  <input hidden class="itemId"
                    value="${id}"/>

                    <input 
                    type="number"
                    value="${quantity}"
                    class="quantityCart h-10 w-7 rounded border-gray-200 text-center [-moz-appearance:_textfield] sm:text-sm [&::-webkit-outer-spin-button]:m-0 [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:m-0 [&::-webkit-inner-spin-button]:appearance-none"
                    />

                  <button
                    type="button"
                    class="increase"
                    class="w-7 h-10 leading-10 text-gray-600 transition hover:opacity-75"
                  >
                    &plus;
                  </button>
                </div>
              </div>

              <button class="remove text-gray-600 transition hover:text-red-600">
                <span class="sr-only">Remove item</span>

                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke-width="1.5"
                  stroke="currentColor"
                  class="h-4 w-4"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
                  />
                </svg>
              </button>
            </div>`);
    $(".cart-list").append(cartList);
    var quantityCart = cartList.querySelector(".quantityCart")
    quantityCart.addEventListener("input", () => {
        if (quantityCart.value < 1) {
            quantityCart.value = 1
        }
        if (quantityCart.value > 99) {
            quantityCart.value = 99
        }
        $(`.cart-list .Total${itemId}`).text("$" + quantityCart.value * parseFloat($(`.cart-list .Price${itemId}`).text().slice(1)))
        updateCookie(itemId, name, imageLink, price, quantityCart.value, true)
        getTotalPrice()
    })
    var itemId = cartList.querySelector(".itemId").value
    cartList.onclick = ((e) => {
        if (e.target.closest(".decrease")) {
            if (quantityCart.value > 1) {
                quantityCart.value = parseInt(quantityCart.value) - 1;
                updateCookie(itemId, name, imageLink, price, -1, false)
                $(`.cart-list .Total${itemId}`).text("$" + quantityCart.value * parseFloat($(`.cart-list .Price${itemId}`).text().slice(1)))
                getTotalPrice()
            }
        }
        if (e.target.closest(".increase")) {
            if (quantityCart.value < 99) {
                quantityCart.value = parseInt(quantityCart.value) + 1;
                updateCookie(itemId, name, imageLink, price, 1, false)
                $(`.cart-list .Total${itemId}`).text("$" + quantityCart.value * parseFloat($(`.cart-list .Price${itemId}`).text().slice(1)))
                getTotalPrice()
            }
        }
        if (e.target.closest(".remove")) {
            $(cartList).remove();
            removeCookie(itemId)
            getTotalPrice()
        }
    })
}

function getTotalPrice() {
    let totalPrice = 0;
    let cookieList = [];
    var cartNumber = $(".cart-number")
    cartNumber.show();
    const getCookie = document.cookie.replace(/(?:(?:^|.*;\s*)cart\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    if (getCookie) {
        cookieList = JSON.parse(decodeURIComponent(getCookie));
        for (var i = 0; i < cookieList.length; i++) {
            totalPrice += parseInt(cookieList[i].number) * parseFloat(cookieList[i].price.slice(1))
        }
    }
    if (!cookieList.length) {
        $(".cart-title").css("display", "block");
        cartNumber.hide()
    } else {
        $(".cart-title").css("display", "none");
        cartNumber.text(cookieList.length)
    }
    $(".cartTotal").text(`Total(${cookieList.length}): $${totalPrice}`)
}

function ShowAlert(attr, title, mess) {
    var alertList = $(".alert-list");
    var el = document.createElement("div");
    el.classList.add("flex")
    el.classList.add("justify-end")
    el.innerHTML = (`<div class="alert z-10 flex p-3 w-fit mb-2 text-sm text-white border border-${attr}-300 rounded-lg bg-${attr}-50 dark:bg-${attr}-700 dark:text-green-400 dark:border-green-800" role="alert">
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
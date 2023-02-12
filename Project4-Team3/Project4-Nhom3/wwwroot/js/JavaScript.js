function cleanCart() {
    console.log("da xoa cart");
    //setCookie("cart", "", 0);
    DeleteCookie("cart");
}

function AddCart(id) {
    var cart = {};
    var cartJson = getCookie("cart");
    if (cartJson == "") {
        cart[id] = 1;
    }
    else {
        cart = JSON.parse(cartJson);
        if (cart[id] == null) {
            cart[id] = 1;
        } else {
            cart[id]++;
        }
        
    }
    setCookie("cart", JSON.stringify(cart), 7);
    console.log(cart);
}

function UpdateCart(id, SoLuong) {
    var cartjson = getCookie("cart");
    var cart = JSON.parse(cartjson);
    cart[id] = SoLuong;
    setCookie("cart", JSON.stringify(cart), 7);
    console.log(cart);
}

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function DeleteCookie(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}
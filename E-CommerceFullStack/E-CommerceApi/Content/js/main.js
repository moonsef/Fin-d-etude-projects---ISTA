
const produit_container = document.querySelector(".latest-products > div > div");
const nav = document.querySelector(".navbar-nav");

async function getProduits() {
    try {
        var produits = (await axios.get("/api/produits/latest")).data;


        for (let produit of produits) {

            produit_container.innerHTML += `
            <div class="col-md-4">
                <div class="product-item">
                    <a href="/produits/produit?nom=${new URLSearchParams(produit.Nom).toString()}"><img src="data:image/jpg;base64,${produit.Photos[0].Photo}" alt=""></a>
                    <div class="down-content">
                        <a href="/produits/produit?nom=${new URLSearchParams(produit.Nom).toString()}">
                            <h4>${produit.Nom.length > 29 ? produit.Nom.substring(0, 29) + "..." : produit.Nom}</h4>
                        </a>
                        <h6 style="${produit.Promo != 0 ? 'font-size: 14px; text-decoration: line-through;' : ''}">${produit.Prix} MAD</h6>
                        <h6>${produit.Promo != 0 ? parseInt(produit.Prix) - (parseInt(parseInt(produit.Prix) * produit.Promo / 100)) + ',00 MAD' : ''}</h6>
                        <h6 style="color: ${produit.Statut == 'en stock' ? '#559f45' : '#d9534f'}; font-size: 14px; padding-top: 8px;"> Produit ${produit.Statut} ${produit.Quantity != 0 ? '('+ produit.Quantity +')' : ''}</h6>
                        <p>${produit.Description.substring(0, 70) + "..."}</p>
                        <div class="stars-${produit.ID}">
                           
                        </div>
                        <span>Avis (${produit.TotalRating})</span>
                    </div>
                </div>
            </div>
        `;
            var stars = document.querySelector(`.stars-${produit.ID}`);

            for (var j = 0; j < produit.Rating; j++) {
                console.log("first for loop", j);
                stars.innerHTML += `<i class="fa fa-star checked"></i>`;
            }

            for (var j = produit.Rating; j < 5; j++) {
                console.log("second for loop", j);
                stars.innerHTML += `<i class="fa fa-star"></i>`;

            }
        }
    } catch (e) {
        console.log(e);
        if (e.response) {
            alert(e.response.data.Message ?? "Error occured")
        }
    } finally {
        $("#preloader").animate({
            'opacity': '0'
        }, 600, function () {
            setTimeout(function () {
                $("#preloader").css("visibility", "hidden").fadeOut();
            }, 300);
        });
    }
}


async function logout() {
    try {
        await axios.post("/api/auth/logout");
        window.location.reload();
    } catch (e) {
        console.log(e);
    }
}


getProduits();

(async () => {
    try {

      
        await axios.post("api/auth/me");

        nav.innerHTML += `
                <li class="nav-item ">
                    <a class="nav-link" href="/profile">Profile</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" onclick="logout();" style="cursor: pointer;">
                                Logout
                            </a>
                </li>`;

    } catch (e) {
        if (e.response) {
            if (e.response.status === 400) {

                console.log("heheheh")
                nav.innerHTML += `<li class="nav-item">
                    <a class="nav-link" href="/login">Se connecter / S'inscrire</a>
                </li>`;
            }
        }
        console.log(e);
    }
})();

   


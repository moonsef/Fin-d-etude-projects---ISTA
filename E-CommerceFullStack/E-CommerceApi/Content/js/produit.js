
            
const nav = document.querySelector(".navbar-nav");
const product_name = document.querySelector(".details h3");
const product_desc = document.querySelector(".product-description");
const product_prix = document.querySelector(".price span");
const product_prix_old = document.querySelector(".price-old");
const preview_pic = document.querySelector(".preview-pic");
const preview_thumb = document.querySelector(".preview-thumbnail");
const review_tab = document.getElementById("reviews");
const review_count = document.getElementById("reviews-tab");
const review_no = document.querySelector(".review-no");
const product_stars = document.querySelector(".stars");
const product_statut = document.querySelector(".produit-statut");
const product_etat = document.querySelector(".produit-etat");



(async () => {
    try {
        await axios.post("/api/auth/me");

        nav.innerHTML += `
                <li class="nav-item">
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

                nav.innerHTML += `<li class="nav-item">
                    <a class="nav-link" href="/login">Se connecter / S'inscrire</a>
                </li>`;
            }
        }
        console.log(e);
    }
})();


async function logout() {
    try {
        await axios.post("/api/auth/logout");
        window.location.reload();
    } catch (e) {
        console.log(e);
    }
}
document.addEventListener("DOMContentLoaded", async function () {

    try {
        const params = new URLSearchParams(window.location.search)
        if (params.has("nom")) {
            var nom = params.get("nom")
            var data = (await axios.post("/api/produits/produit", { "nom": nom.includes("=") ? nom.slice(0, -1) : nom })).data;

            product_name.textContent = data.Nom;
            product_desc.textContent = data.Description;
            review_no.textContent = data.TotalRating + " avis";
            product_statut.innerHTML = `<h6 style=" color: ${data.Statut == 'en stock' ? '#559f45' : '#d9534f'}; font-size: 14px; padding-top: 8px;"> ${data.Statut.toUpperCase()} ${data.Quantity != 0 ? '(' + data.Quantity + ')' : ''}</h6>`;
            product_etat.innerHTML = `<h6 style="margin-bottom:5px; color: ${data.Etat == 'neuf' ? '#559f45' : '#d9534f'}; font-size: 16px; padding-top: 8px;"> ${data.Etat.toUpperCase()} </h6>`;

            for (var i = 0; i < data.Rating; i++)
                product_stars.innerHTML += `<span class="fa fa-star checked"></span>`;

            for (var i = data.Rating; i < 5; i++)
                product_stars.innerHTML += `<span class="fa fa-star"></span>`;


            if (data.Promo != 0) {
                var prix = parseInt(data.Prix);
                product_prix.textContent = parseInt((prix - (prix * data.Promo / 100))).toString() + ",00 MAD";
                product_prix_old.textContent = data.Prix + " MAD";
            } else {
                product_prix.textContent = data.Prix + " MAD";
                product_prix_old.style.display = "none";
            }



            var i = 0;
            for (var photo of data.Photos) {
                preview_pic.innerHTML += `
                    <div class="tab-pane ${i === 0 ? 'active' : ''}"  id="pic-${photo.ID}"><img src="data:image/jpg;base64,${photo.Photo}"/></div>
                `;

                preview_thumb.innerHTML += `
                    <li class="${i === 0 ? 'active' : ''}"><a data-target="#pic-${photo.ID}" style="cursor: pointer;" data-toggle="tab"><img src="data:image/jpg;base64,${photo.Photo}" /></a></li>
                `;
                i++;
            }
            var j = 0;
            review_count.textContent = `Commentaires(${data.Avis.length})`;
            for (var avis of data.Avis) {
                review_tab.innerHTML += `
                <div class="media-body">   
                    <div class="d-sm-flex justify-content-between">
                        <p class="mt-1 mb-2">
                            <strong>${avis.Client.Nom + ' ' + avis.Client.Prenom}</strong>
                            <span>– </span><span>${avis.Date}</span>
                        </p>
                        <div class="rating">
                            <div class="stars-review">
                            </div>
                        </div>
                    </div>
                    <p class="mb-0">${avis.Text}</p>
                </div>  
                <hr/>
                `;
                if (j == 0) {
                    var stars = document.querySelector(".stars-review");
                    for (var i = 0; i < avis.AvisEtoile; i++)
                        stars.innerHTML += `<svg class="svg-inline--fa fa-star fa-w-18 checked" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="star" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg=""><path fill="currentColor" d="M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z"></path></svg>`;

                    for (var i = avis.AvisEtoile; i < 5; i++)
                        stars.innerHTML += `<svg class="svg-inline--fa fa-star fa-w-18" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="star" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg=""><path fill="currentColor" d="M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z"></path></svg>`;

                }
                else {
                    var stars = document.querySelectorAll(".stars-review");
                    for (var i = 0; i < avis.AvisEtoile; i++)
                        stars[j].innerHTML += `<svg class="svg-inline--fa fa-star fa-w-18 checked" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="star" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg=""><path fill="currentColor" d="M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z"></path></svg>`;

                    for (var i = avis.AvisEtoile; i < 5; i++)
                        stars[j].innerHTML += `<svg class="svg-inline--fa fa-star fa-w-18" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="star" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg=""><path fill="currentColor" d="M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z"></path></svg>`;
                }

                j++;
            }

            review_tab.innerHTML += `
                    <h5 class="mt-4">Ajouter un commentaire</h5>
                        <div class="my-3">
                            <div class="rating">
                                <div class="stars" style="cursor: pointer;">
                                    <i class="fa fa-star star-review" onclick="setStar1()"></i>
                                    <i class="fa fa-star star-review" onclick="setStar2()"></i>
                                    <i class="fa fa-star star-review" onclick="setStar3()"></i>
                                    <i class="fa fa-star star-review" onclick="setStar4()"></i>
                                    <i class="fa fa-star star-review" onclick="setStar5()"></i>
                                </div>
                            </div>
                        </div>
                        <div>
                        <form id="review-form"> 
                            <div class="md-form md-outline">
                                <label for="form76">Votre avis:</label>
                                <textarea id="form76" name="review" class="md-textarea form-control pr-6" rows="4"></textarea>
                            </div>
                            <div class="invalid-feedback mt-2" style="display: block; font-size: 14px; margin-bottom: 6px;"></div>

                            <div class="text-right pb-2">
                                <button id="post-avis" type="button" onclick="postAvis()" class="btn btn-danger mt-3 mb-3">Ajouter un commentaire</button>
                            </div>

                        </form>
                           
                    </div>
            `;



        }
    } catch (e) {
        console.log(e);
    } finally {

        $("#preloader").animate({
            'opacity': '0'
        }, 600, function () {
            setTimeout(function () {
                $("#preloader").css("visibility", "hidden").fadeOut();
            }, 300);
        });


    }
});

async function postAvis() {
    const form = document.getElementById("review-form");
    const button = document.getElementById("post-avis");
    const error_text = document.querySelector(".invalid-feedback");
    const stars = document.querySelectorAll(".star-review");
    const params = new URLSearchParams(window.location.search)

    var stars_count = 0;

    stars.forEach(e =>  {
        if (e.classList.contains("checked")) {
            stars_count++;
        }
    });


    var data = {
        "produitnom": params.get("nom").includes("=") ? params.get("nom").slice(0,-1) : params.get("nom"),
        "avisetoile": stars_count,
        "text": form.review.value
    };

   
    try {
        button.textContent = "Chargement...";
        error_text.textContent = "";

        await axios.post("/api/avis/create", data);

        window.location.reload();

    } catch (e) {
        if (e.response) {
            if (!e.response.data.ModelState) {
                error_text.textContent = e.response.data.Message;
            } else {
                for (const key in e.response.data.ModelState) {
                    error_text.textContent = e.response.data.ModelState[key][0]
                }
            }
        }

    } finally {
        button.textContent = "Ajouter un commentaire";

    }
}


function setStar1() {
    var star = document.querySelectorAll(".star-review")[0];
    star.classList.remove("checked");
    star.classList.add("checked");
}

function setStar2() {
    
    for (var i = 0; i < 2; i++) {
        var star = document.querySelectorAll(".star-review")[i];
        star.classList.remove("checked");
        star.classList.add("checked");
    }
   
}
function setStar3() {

    for (var i = 0; i < 3; i++) {
        var star = document.querySelectorAll(".star-review")[i];
        star.classList.remove("checked");
        star.classList.add("checked");
    }

}
function setStar4() {

    for (var i = 0; i < 4; i++) {
        var star = document.querySelectorAll(".star-review")[i];
        star.classList.remove("checked");
        star.classList.add("checked");
    }

}
function setStar5() {

    for (var i = 0; i < 5; i++) {
        var star = document.querySelectorAll(".star-review")[i];
        star.classList.remove("checked");
        star.classList.add("checked");
    }

}





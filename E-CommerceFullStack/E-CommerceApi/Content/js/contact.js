const nav = document.querySelector(".navbar-nav");

async function logout() {
    try {
        await axios.post("/api/auth/logout");
        window.location.reload();
    } catch (e) {
        console.log(e);
    }
}


(async () => {
    try {
        await axios.post("api/auth/me");

        nav.innerHTML += `
                        <li class="nav-item ">
                            <a class="nav-link" href="/profile">Profile</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" style="cursor:pointer;" onclick="logout();">
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
    }
})();

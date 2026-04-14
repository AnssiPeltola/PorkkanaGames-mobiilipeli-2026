document.addEventListener("DOMContentLoaded", () => {
  // Get the language toggle button and all elements that have translatable keys into variables
  const languageButton = document.getElementById("language-toggle");
  const translatableElements = document.querySelectorAll("[data-i18n]");

  const translations = {
    en: {
      "nav.home": "Home",
      "nav.trailer": "Trailer",
      "nav.about": "About",
      "nav.developers": "Developers",
      "nav.partners": "Partners",
      "nav.portal": "Portal",
      "about.title": "About",
      "about.text":
        "Our project is a two-player cooperative cooking game for children. In the game, two players work together through different levels around the different stages of food preparation. The goal of the two players is to get the selected recipe ready as a dish through cooperation using different game mechanics. The game is implemented at the request of the client Welltech lab for their YETI tablet. The purpose of the game is to teach players a general understanding of the different stages of cooking and what cooking involves.",
      "roles.developer": "Developer",
      "roles.artist": "Artist",
    },
    fi: {
      "nav.home": "Koti",
      "nav.trailer": "Traileri",
      "nav.about": "Tietoa",
      "nav.developers": "Kehittäjät",
      "nav.partners": "Yhteistyökumppanit",
      "nav.portal": "Portaali",
      "about.title": "Tietoa",
      "about.text":
        "Projektimme on lapsille suunnattu kahdelle pelaajalle tarkoitettu yhteistyöpeli kokkaamisesta. Pelissä kaksi pelaajaa työskentelee yhdessä eri tasojen läpi ruoan eri valmistusvaiheiden ympärillä. Tavoitteena kahdella pelaajalla on saada valittu resepti valmiiksi ruoaksi yhteistyön avulla eri pelimekaniikkoja käyttäen. Peli toteutetaan toimeksiantajan Welltech labin pyynnöstä heidän YETI-tabletille. Pelin tarkoitus on opettaa pelaajille ruoanlaiton eri vaiheiden yleistä ymmärtämistä ja mitä ruoanlaittoon kuuluu.",
      "roles.developer": "Koodari",
      "roles.artist": "Artisti",
    },
  };

  // Set default lanuage as english
  let currentLanguage = "en";

  // Apply current language to all marked elements and update toggle button label
  const applyLanguage = () => {
    translatableElements.forEach((element) => {
      element.textContent = translations[currentLanguage][element.dataset.i18n];
    });

    languageButton.textContent = currentLanguage === "en" ? "FI" : "EN";
    document.documentElement.lang = currentLanguage;
  };

  // Set initial language text when page is ready
  applyLanguage();

  // Switch between English and Finnish on button click
  languageButton.addEventListener("click", () => {
    currentLanguage = currentLanguage === "en" ? "fi" : "en";
    applyLanguage();
  });
});

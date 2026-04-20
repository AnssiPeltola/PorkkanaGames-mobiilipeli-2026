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
      "download.button": "Download",
      "about.title": "About",
      "about.text":
        '"Kokataan yhdessä" is a co-operative multiplayer cooking game, where you can learn the basics of cooking and communication in a stress-free environment. Choose the recipe, work together and enjoy the game at your own pace. There are no penalties or time limits. The goal is simple: learn and have fun. This is a cooking game for everyone!',
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
      "download.button": "Lataa",
      "about.title": "Tietoa",
      "about.text":
        '"Kokataan yhdessä" on yhteistyöhön kannustava kahden pelaajan kokkauspeli, jossa voit oppia kokkauksen ja kommunikaation perusteita rauhallisessa ympäristössä. Valitkaa resepti, työskennelkää yhdessä ja nauttikaa pelistä omassa tahdissanne. Ei rangaistuksia tai aikarajoitteita. Pelin tavoite on yksinkertainen: opi ja pidä hauskaa. Tämä on kokkauspeli kaikille!',
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

    languageButton.textContent = currentLanguage === "en" ? "EN" : "FI";
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

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
      "about.text": "This is the about text.",
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
      "about.text": "Tämä on tietoa tekesti",
      "roles.developer": "Koodari",
      "roles.artist": "Graafikko",
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

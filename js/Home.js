const openLoginBtn = document.getElementById("openLogin");
const openSignupBtn = document.getElementById("openSignup");
const loginForm = document.getElementById("login");
const signupForm = document.getElementById("signup");
const homeImage = document.getElementById("homeImage");

let openLogin = false;
let openSignup = false;

function toggleForms(matchValue, otherValue, mainForm, otherForm) {
  if (openLogin || openSignup) {
    homeImage.style.display = "none";
  }
  if (otherValue) {
    otherForm.style.display = "none";
  }
  mainForm.style.display = matchValue ? "flex" : "none";
  if (!openLogin && !openSignup) {
    console.log("hii");
    homeImage.style.display = "block";
  }
}

openSignupBtn.onclick = () => {
  openSignup = !openSignup;
  toggleForms(openSignup, openLogin, signupForm, loginForm);
};

openLoginBtn.onclick = () => {
  openLogin = !openLogin;
  toggleForms(openLogin, openSignup, loginForm, signupForm);
};

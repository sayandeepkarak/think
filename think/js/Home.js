const openLoginBtn = document.getElementById("openLogin");
const openSignupBtn = document.getElementById("openSignup");
const loginForm = document.getElementById("login");
const signupForm = document.getElementById("signup");
const homeImage = document.getElementById("homeImage");

let openLogin = false;
let openSignup = false;

function toggleForm(mainValue, mainForm, otherForm) {
  homeImage.style.display = mainValue ? "none" : "block";
  if (mainValue) {
    otherForm.style.display = "none";
  }
  mainForm.style.display = mainValue ? "flex" : "none";
}

function validateFields(condition, el) {
  el.style.border = `1px solid var(${
    condition ? "--border" : "--error-border"
  })`;
  return condition;
}

openSignupBtn.onclick = () => {
  openLogin = false;
  openSignup = !openSignup;
  toggleForm(openSignup, signupForm, loginForm);
};

openLoginBtn.onclick = () => {
  openSignup = false;
  openLogin = !openLogin;
  toggleForm(openLogin, loginForm, signupForm);
};

loginForm.addEventListener("submit", (e) => {
  e.preventDefault();
  const formData = new FormData(loginForm);
  const formDataObj = Object.fromEntries(formData);
  const { loginEmail, loginPassword } = { ...formDataObj };
  let isValid = true;
  isValid &= validateFields(
    loginEmail !== "",
    document.getElementById("loginEmail")
  );
  isValid &= validateFields(
    loginPassword !== "",
    document.getElementById("loginPassword")
  );
  if (isValid) {
    console.log(formDataObj);
    loginForm.reset();
  }
});

signupForm.addEventListener("submit", (e) => {
  e.preventDefault();
  const formData = new FormData(signupForm);
  const formDataObj = Object.fromEntries(formData);
  const { fullname, email, mobile, password, confirmPass } = { ...formDataObj };
  let isValid = true;
  isValid &= validateFields(
    fullname !== "",
    document.getElementById("fullname")
  );
  isValid &= validateFields(email !== "", document.getElementById("email"));
  isValid &= validateFields(
    /^[6-9]([0-9]){9}$/.test(mobile),
    document.getElementById("mobile")
  );
  isValid &= validateFields(
    password.length > 8,
    document.getElementById("password")
  );
  isValid &= validateFields(
    confirmPass === password,
    document.getElementById("confirmPass")
  );
  if (isValid) {
    console.log(formDataObj);
    signupForm.reset();
  }
});

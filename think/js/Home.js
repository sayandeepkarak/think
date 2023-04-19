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

loginForm.addEventListener("submit", async (e) => {
  e.preventDefault();
  const formData = new FormData(loginForm);
  const { loginEmail, loginPassword } = Object.fromEntries(formData);
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
    try {
      const res = await fetch("/api/login.ashx", {
        method: "POST",
        headers: {
          ContentType: "application/json",
        },
        body: JSON.stringify({ email: loginEmail, password: loginPassword }),
      });
      if (res.ok) {
        const data = await res.json();
        window.location.href =
          data.userType == "admin" ? "/AdminPanel" : "/UserPanel";
        loginForm.reset();
      } else if (res.status == 404) {
        alert("Invalid credential");
      }
    } catch (error) {
      console.log(error);
    }
  }
});

signupForm.addEventListener("submit", async (e) => {
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
    try {
      const res = await fetch("/api/userRegister.ashx", {
        method: "POST",
        headers: {
          ContentType: "application/json",
        },
        body: JSON.stringify({ fullname, email, mobile, password }),
      });
      if (res.ok) {
        const { status, message } = await res.json();
        window.location.href = "/UserPanel";
      } else if (res.status == 409) {
        alert(res.statusText);
      }
      signupForm.reset();
    } catch (error) {
      console.log(error);
    }
  }
});

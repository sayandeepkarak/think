const openLoginBtn = document.getElementById("openLogin");
const openSignupBtn = document.getElementById("openSignup");
const loginForm = document.getElementById("login");
const signupForm = document.getElementById("signup");
const recoverForm = document.getElementById("recoverPasswordForm");
const homeImage = document.getElementById("homeImage");
const openRegLink = document.getElementById("openRegLink");
const openLogLink = document.getElementById("openLogLink");
const opeForgotLink = document.getElementById("forgotLink");

let openLogin = false;
let openSignup = false;
let openRecover = false;

function toggleForm(mainValue, mainForm, ...otherForm) {
  homeImage.style.display = mainValue ? "none" : "block";
  if (mainValue) {
    otherForm.forEach((e) => {
      e.style.display = "none";
    });
  }
  mainForm.style.display = mainValue ? "flex" : "none";
}

function validateFields(condition, el) {
  el.style.border = `1px solid var(${
    condition ? "--border" : "--error-border"
  })`;
  return condition;
}

const openSignUpForm = () => {
  openLogin = false;
  openRecover = false;
  openSignup = !openSignup;
  toggleForm(openSignup, signupForm, loginForm, recoverForm);
};

const openLoginForm = () => {
  openSignup = false;
  openRecover = false;
  openLogin = !openLogin;
  toggleForm(openLogin, loginForm, signupForm, recoverForm);
};
opeForgotLink.onclick = () => {
  openSignup = false;
  openLogin = false;
  openRecover = !openRecover;
  const errorText = document.getElementById("recoverError");
  recoverForm.reset();
  errorText.style.display = "none";
  errorText.innerText = "";
  toggleForm(openRecover, recoverForm, signupForm, loginForm);
};

openRegLink.onclick = () => openSignUpForm();
openLogLink.onclick = () => openLoginForm();
openSignupBtn.onclick = () => openSignUpForm();
openLoginBtn.onclick = () => openLoginForm();

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
    const loginBtn = document.getElementById("loginBtn");
    loginBtn.disabled = true;
    loginBtn.innerText = "Login...";

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
        const logErr = document.getElementById("loginError");
        logErr.innerText = "Invalid credentials";
        logErr.style.display = "block";
      }

      loginBtn.disabled = false;
      loginBtn.innerText = "Login";
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
    const registerBtn = document.getElementById("registerBtn");
    registerBtn.disabled = true;
    registerBtn.innerText = "Register...";
    try {
      const res = await fetch("/api/userRegister.ashx", {
        method: "POST",
        headers: {
          ContentType: "application/json",
        },
        body: JSON.stringify({ fullname, email, mobile, password }),
      });
      if (res.ok) {
        await res.json();
        signupForm.reset();
        window.location.href = "/UserPanel";
      } else if (res.status == 409) {
        alert(res.statusText);
      }
      registerBtn.disabled = true;
      registerBtn.innerText = "Register...";
    } catch (error) {
      console.log(error);
    }
  }
});

recoverForm.addEventListener("submit", async (e) => {
  e.preventDefault();
  const errorText = document.getElementById("recoverError");
  errorText.style.display = "none";
  errorText.innerText = "";

  const formData = new FormData(recoverForm);
  const { recoverEmail } = Object.fromEntries(formData);

  let isValid = true;
  isValid &= validateFields(
    recoverEmail !== "",
    document.getElementById("recoverEmail")
  );

  if (isValid) {
    const recoverButton = document.getElementById("recoverBtn");
    recoverButton.disabled = true;
    recoverButton.innerText = "Finding...";
    try {
      const res = await fetch("/api/recoverPassword.ashx", {
        method: "POST",
        headers: {
          ContentType: "application/json",
        },
        body: JSON.stringify({ email: recoverEmail }),
      });

      if (res.ok) {
        const data = await res.json();
        showAlert(
          true,
          `It's great to tell you dear ${data?.message}, your password is sent to your mail address`
        );
        recoverForm.reset();
        openRecover = !openRecover;
        toggleForm(openRecover, recoverForm, signupForm, loginForm);
      } else if ((res.status = 404)) {
        errorText.innerText = "Email id doesn't exist";
        errorText.style.display = "block";
      } else if ((res.status = 500)) {
        showAlert(false, "Please try again after sometimes");
      }

      recoverButton.disabled = true;
      recoverButton.innerText = "Get Password";
    } catch (error) {
      console.log(error);
    }
  }
});

const cancleBtn = document.getElementById("cancleBtn");
const openBtn = document.getElementById("openUpdateForm");
const panelImage = document.getElementById("userPanelImage");
const userInfoForm = document.getElementById("userInfoform");

const resetAllInput = (...inputs) => {
  Array.from(inputs).forEach((e) => {
    document.getElementById(e).value = "";
    document.getElementById(e).style.border = "1px solid var(--border)";
  });
};

const toggleForm = (isOpen) => {
  !isOpen && resetAllInput("fullname", "email", "mobile", "password");
  panelImage.style.display = isOpen ? "none" : "block";
  userInfoForm.style.display = isOpen ? "flex" : "none";
};

cancleBtn.onclick = () => {
  toggleForm(false);
};

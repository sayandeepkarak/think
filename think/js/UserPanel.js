const cancleBtn = document.getElementById("cancleBtn");
const openBtn = document.getElementById("openUpdateForm");
const panelImage = document.getElementById("userPanelImage");
const userInfoForm = document.getElementById("userInfoform");

const toggleForm = (isOpen) => {
  if (!isOpen) {
    document.getElementById("fullname").value = "";
    document.getElementById("email").value = "";
    document.getElementById("mobile").value = "";
    document.getElementById("password").value = "";
  }
  panelImage.style.display = isOpen ? "none" : "block";
  userInfoForm.style.display = isOpen ? "flex" : "none";
};

cancleBtn.onclick = () => {
  toggleForm(false);
};

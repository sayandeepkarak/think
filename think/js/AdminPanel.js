function showAlert(isOk, text) {
  const alertArea = document.getElementById("alertArea");
  alertArea.innerHTML = `
        <div class="alert alert-dismissible fade show customAlert" role="alert">
            <div class="alert-indicator ${isOk ? "ok" : "err"}"></div>
            <strong>${isOk ? "Success" : "Failed"}!</strong> ${text}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
}

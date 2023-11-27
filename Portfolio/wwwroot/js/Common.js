let timerInterval;
window.ShowPPTDialog = () => {
    Swal.fire({
        title: "TicTacToe AI Game in Blazor - Rikam Palkar",
        html: "<p> Presented at Sharda University, Delhi, India</p><p>Event organized by C#Corner</p> </br> Navigate through the presentation using the arrow keys. ( ↑  →  ↓  ← )",
        timer: 3000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading();
            const timer = Swal.getPopup().querySelector("b");
            timerInterval = setInterval(() => {
                timer.textContent = `${Swal.getTimerLeft()}`;
            }, 100);
        },
        willClose: () => {
            clearInterval(timerInterval);
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            console.log("Closed by the timer");
        }
    });
}

window.getWindowWidth = function () {
    return window.innerWidth;
};

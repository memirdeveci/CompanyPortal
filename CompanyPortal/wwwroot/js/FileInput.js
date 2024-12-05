const cloudArrow = document.querySelector(".formDiv");
const fileInput = document.querySelector(".file-input");
const completed = document.querySelector(".CompletedText");

cloudArrow.addEventListener("click", () => {
    fileInput.click();
});

fileInput.onchange = ({ target }) => {
    let file = target.files[0];
    if (file) {
        let fileName = file.name;
        if (fileName.length >= 12) {
            let splitName = fileName.split('.');
            fileName = splitName[0].substring(0, 13) + "... ." + splitName[1];
        }
        completed.innerHTML = fileName + " adlı dosya yüklendi";
    }
}
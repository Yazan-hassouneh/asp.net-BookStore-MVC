let ImageInput = document.getElementById("Image")
let ImagePreview = document.getElementById("ImagePreview")

ImageInput.onchange = () => {
    ImagePreview.classList.remove("d-none")
    ImagePreview.src = window.URL.createObjectURL(ImageInput.files[0])
}
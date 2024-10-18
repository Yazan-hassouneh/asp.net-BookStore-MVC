const NoCategoryCloseBtn = document.querySelectorAll("[data-bs-dismiss='alert']")

NoCategoryCloseBtn.forEach((btn) => {
    btn.addEventListener('click', () => {
        btn.parentElement.classList.add("d-none")
    })
})

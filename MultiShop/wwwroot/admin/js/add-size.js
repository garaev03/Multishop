let AddSizeBtn = document.querySelector(".AddSize")
let sizeColorMain = document.querySelector(".size-color-main")
let sizeColor = document.querySelector(".size-color")

AddSizeBtn.addEventListener("click", () => {
    let newSizeColor = document.createElement("div")
    let deleteBtn = document.createElement("a")
    deleteBtn.setAttribute("class","deleteBtn btn btn-outline-danger text-center mt-1 p-0 ps-2 pe-2")
    deleteBtn.innerHTML+='<i class="fas fa-minus-circle fa-xs"></i>'
   
    newSizeColor.setAttribute("class", "size-color")
    newSizeColor.innerHTML = sizeColor.outerHTML
    newSizeColor.children[0].children[0].children[0].appendChild(deleteBtn)
    sizeColorMain.appendChild(newSizeColor)
   
    deleteBtn.addEventListener("click",()=>{
        deleteBtn.parentElement.parentElement.parentElement.remove();
    })
})  

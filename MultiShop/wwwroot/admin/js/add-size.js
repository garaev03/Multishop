let AddSizeBtn = document.querySelector(".AddSize")
let sizeColor = document.querySelector(".size-color")
let sizeColorMain = document.querySelector(".size-color-main")

AddSizeBtn.addEventListener("click", () => {
    sizeColorMain.innerHTML += sizeColor.outerHTML
    let selectSizes=document.querySelectorAll(".select-sizes")
    let last = selectSizes[selectSizes.length- 1];
    last.innerHTML+='<a class="btn btn-outline-danger text-center btn-sm mt-3 pt-2 ms-3" style="height:34px"><i class="fas fa-minus-circle"></i></a>'
    console.log(last);
})  

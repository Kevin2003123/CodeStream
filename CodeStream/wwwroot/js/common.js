function DataTable() {
    $('#adminTable').DataTable();
}

function Load(comp) {
    $(comp).load();
}

 function  AddToCart(id) {

     let items = JSON.parse(localStorage.getItem("cart"));
    

    if (items) {
        
       //let exist =   items.find(x=>x.id == item.id)
       // let isArray =  Array.isArray(items); 
        let exist2 =  items.some((x) => x == id);
        if (!exist2) {
           localStorage.setItem("cart", JSON.stringify([...items, id]))
        }
       
    } else {
        localStorage.setItem("cart", JSON.stringify([id]))
    }
    
}

function RemoveCartItem(id) {
    let items = JSON.parse(localStorage.getItem("cart"));


        let exist = items.some((x) => x == id);
    if (exist) {


       let result =  items.filter(x => x != id)
            localStorage.setItem("cart", JSON.stringify([...result]))
        }

  
}

window.showToastr = (type, message) => {
    if (type === 'success') {
        toastr.success(message, "Operation Correcta", { timeOut: 10000 })
    }

    if (type === 'error') {
        toastr.error(message, "Operation Fallida", { timeOut: 10000 })
    }
}


window.showSwal = (type, message) => {
    if (type === 'success') {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        );
    }

    if (type === 'error') {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}


function ClearHours(weekId) {

    $(`#${weekId}`).val("");

}

function AddHours(weekId, hours) {

    $(`#${weekId}`).val(hours);

}





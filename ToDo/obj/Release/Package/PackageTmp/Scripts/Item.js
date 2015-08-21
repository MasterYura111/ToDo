$(document).ready(function() {
    window.CountAddItem = $('#countItem').val();
   
    
})
function AddItem(a) {
    //var template = $('.NewItemtemplate').html()
    
    var html2Add = "<div class='newItemNotDB'><input class='form-control input-inline' type='text' data-val='true' data-val-length='min lenght 3,max lenght 50 ' data-val-length-max='50' data-val-length-min='3' data-val-required='enter text' name='[" + window.CountAddItem + "].Text' /><span class='deleteItemFromList' onclick='DeleteOneItem(this);'><img src='/Content/images/delete.png' /></span></div>"

    window.CountAddItem++
    

    $('.NewItem').append(html2Add)
    
}
function DeleteOneItem(a) {
    var attrClass=$(a).parent().attr('class')
    if (attrClass != "newItemNotDB") {
        $(a).parent().hide()
        $(a).parent().children('input[type="text"]').val(null)
    } else {
        $(a).parent().remove()

        window.CountAddItem = $('#countItem').val();
        $('.newItemNotDB').each(function() {
            $(this).children('input[type="text"]').attr('name', '[' + window.CountAddItem + '].Text')
            window.CountAddItem++
        })
    }
    
}
$(document).ready(function () {
    $('.ulItems :checkbox').change(function () {
        $('.ItemSaveConfirm').hide()
        var id = $(this).attr('id')
        var value=$(this).prop('checked')
        var data = { Id: id, value:value };
        $.ajax({
            type: "POST",
            url: "/Item/Change",
            data: data,
            datatype: "json",
            success: function (data) {
                //alert(data)
                if(data=="True")
                    $('.ItemSaveConfirm').show()
                else {
                    //alert("Save item error")
                    $('.ItemSaveConfirm').hide()
                }
            }
        });


    });
})
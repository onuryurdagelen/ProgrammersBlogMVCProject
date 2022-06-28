
//Tüm kategorileri getiren fonksiyondur.
function LoadAllCategories() {
    const url = '/Admin/Category/GetAllCategories/'; //Area/Controller/ActionName/
    $.ajax({
        type: 'GET',
        url: url,
        contentType: "application/json",
        //İsteği atmadan önce kullanılır.Spinner kullanılır ve tablo temizlenir.updatedTableRowObject
        beforeSend: function () {
            $("#categoriesTable").hide(); //Yenile butonua bastığımızda tabloyu gizleriz.
               //console.log("BEFORE-SEND")
            $(".spinner-border").show(); //İstek atıldığında spinner görünür hale gelir.
        },
        success: function (data) {
            //console.log("SUCCESS")
            const categoryListDto = jQuery.parseJSON(data);
            //console.log(categoryListDto.Categories.$values);
            var data = categoryListDto.Categories.$values;
            //console.log("JSON DATA" + data);
            if (categoryListDto.ResultStatus === 0) {
                let tableBody = "";
                $.each(data,
                    function (index, category) {
                        tableBody +=
                            `
                                     <tr data-id='${category.Id}'>
                                                     <td>${category.Id}</td>
                                                     <td>${category.Name}</td>
                                                     <td>${category.Description}</td>
                                                     <td>${convertFirstLetterToUpperCase(category.IsActive.toString())}</td>
                                                     <td>${convertFirstLetterToUpperCase(category.IsDeleted.toString())}</td>
                                                     <td>${category.Note}</td>
                                                     <td>${converToShortDate(category.CreatedDate)}</td>
                                                     <td>${category.CreateByName}</td>
                                                     <td>${converToShortDate(category.ModifiedDate)}</td>
                                                     <td>${category.ModifiedByName}</td>
                                                     <td>
                                                         <button class="btn btn-warning btn-sm btn-edit" data-id="${category.Id}"><span class="fas fa-edit"></span</button>
                                                         <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span></button>
                                                     </td>
                                                 </tr>
                                    `;
                    });
                $("#categoriesTable > tbody").replaceWith(tableBody);
                $(".spinner-border").hide(); //İstek atıldığında spinner görünmez hale gelir.
                $("#categoriesTable").fadeIn(1400); //Verileri getirme başarılı oldugunda tabloyu gösteririz.
                //console.log($("#categoriesTable tbody")[0])

            }
            else {
                console.log(categoryListDto);
                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!')

            }

            //console.log("DATA: "+data);
        },
        error: function (error) {
            console.log(error);
            //console.log(categoryListDto);
            console.log("HATA OLUŞTU!");
            $(".spinner-border").hide(); //İstek atıldığında spinner görünmez hale gelir.
            $("#categoriesTable").fadeIn(1000); //Verileri getirme başarılı oldugunda tabloyu gösteririz.
            toastr.error(`${error.responseText}`, 'Hata!')
        }
    })
}


$(document).ready(function () {


    $('#categoriesTable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                className: 'btn btn-success',
                attr: {
                    id: "btnAdd"
                },
                text: 'Ekle',
                action: function (e, dt, node, config) {

                }
            },
            {
                className: 'btn btn-warning',
                text: 'Yenile',
                attr: {
                    id: "btnRefresh"
                },
                action: function (e, dt, node, config) {
                    LoadAllCategories();
                }
            }


        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi"
                },
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın.",
                "createState": "Şuanki Görünümü Kaydet",
                "removeAllStates": "Tüm Görünümleri Sil",
                "removeState": "Aktif Görünümü Sil",
                "renameState": "Aktif Görünümün Adını Değiştir",
                "savedStates": "Kaydedilmiş Görünümler",
                "stateRestore": "Görünüm -&gt; %d",
                "updateState": "Aktif Görünümün Güncelle"
            },
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar",
                        "notContains": "İçermeyen",
                        "notStarts": "Başlamayan",
                        "notEnds": "Bitmeyen"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d",
                "showMessage": "Tümünü Göster",
                "collapseMessage": "Tümünü Gizle"
            },
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen",
                "weekdays": {
                    "6": "Paz",
                    "5": "Cmt",
                    "4": "Cum",
                    "3": "Per",
                    "2": "Çar",
                    "1": "Sal",
                    "0": "Pzt"
                },
                "months": {
                    "9": "Ekim",
                    "8": "Eylül",
                    "7": "Ağustos",
                    "6": "Temmuz",
                    "5": "Haziran",
                    "4": "Mayıs",
                    "3": "Nisan",
                    "2": "Mart",
                    "11": "Aralık",
                    "10": "Kasım",
                    "1": "Şubat",
                    "0": "Ocak"
                }
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            },
            "stateRestore": {
                "creationModal": {
                    "button": "Kaydet",
                    "columns": {
                        "search": "Kolon Araması",
                        "visible": "Kolon Görünümü"
                    },
                    "name": "Görünüm İsmi",
                    "order": "Sıralama",
                    "paging": "Sayfalama",
                    "scroller": "Kaydırma (Scrool)",
                    "search": "Arama",
                    "searchBuilder": "Arama Oluşturucu",
                    "select": "Seçimler",
                    "title": "Yeni Görünüm Oluştur",
                    "toggleLabel": "Kaydedilecek Olanlar"
                },
                "duplicateError": "Bu Görünüm Daha Önce Tanımlanmış",
                "emptyError": "Görünüm Boş Olamaz",
                "emptyStates": "Herhangi Bir Görünüm Yok",
                "removeConfirm": "Görünümü Silmek İstediğinize Eminminisiniz?",
                "removeError": "Görünüm Silinemedi",
                "removeJoiner": "ve",
                "removeSubmit": "Sil",
                "removeTitle": "Görünüm Sil",
                "renameButton": "Değiştir",
                "renameLabel": "Görünüme Yeni İsim Ver -&gt; %s:",
                "renameTitle": "Görünüm İsmini Değiştir"
            }
        }
    });


    // Datatable ends here
    //Ajax GET / Getting the _CategoryAddPartial as Modal FormData startsWith from here .
    $(function () {
        //const url = '@Url.Action("Add","Category")';
        const url = '/Admin/Category/Add/';
        const placeHolderDiv = $("#modalPlaceHolder");


        $("#btnAdd").click(function () {
            //console.log("GIRDI")
            $.get(url).done(function (data) { // data == _CategoryAddPartial
                console.log(data);
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show'); //placeholderDiv içerisindeki .modal classına ait divi bul demektir ve göster demektir.
                //placeHolderDiv.find(".modal").attr('style','opacity: 1;display:block;'); //placeholderDiv içerisindeki .modal classına ait divi bul demektir ve göster demektir.

            });
        })
        //Ajax GET / Getting the _CategoryAddPartial as Modal FormData ends here.
        //Ajax POST / Posting the FormData as CategoryAddDto starts from here.

        //placeholderDiv isimli div'in icindeki #btnSave id'sine sahip butona basildigi anda gerceklesecek islemler asagidadir.
        placeHolderDiv.on("click",
            "#btnSave",
            function (event) {
                event.preventDefault();
                //console.log("GIRDI 2");
                const form = $("#form-category-add"); //Formu sectik
                //const actionUrl = form.attr("action").toString();
                //const url = '@Url.Action("Add","Category")';
                const url = '/Admin/Category/Add';
                //action attribute'u aslinda bir Url uretir.
                //console.log(actionUrl);
                const dataToSend = form.serialize(); //form'u serialize yani donusturmus olduk.


                $.post(url, dataToSend).done(function (data) {
                    console.log(data);
                    const categoryAddAjaxModel = jQuery.parseJSON(data); //data objesını JSON'a parse ederiz.
                    //console.log(categoryAddAjaxModel);
                    const newFormBody = $(".modal-body", categoryAddAjaxModel.CategoryAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val();
                    //console.log(isValid);
                    if (isValid == 'True') {
                        console.log("VALİD!!!")

                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                            <tr name="row-${categoryAddAjaxModel.CategoryDto.Category.Id}">
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.Id}</td>
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td>
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                                                <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.CreateByName}</td>
                                                <td>${converToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                                <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                                <td>
                                                         <button class="btn btn-warning btn-sm btn-edit" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span> Düzenle</button>
                                                         <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                                     </td>
                             </tr>`;
                        const newTableRowObject = $(newTableRow); //Javascript Objesi haline gelir.
                        newTableRowObject.hide();
                        $("#categoriesTable").append(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!')

                    }
                    else {
                        let summaryText = "";
                        console.log("VALİD DEĞİL!!!")
                        const errors = $("#validation-summary > ul > li");
                        $.each(errors, function () {
                            let text = $(this).text();
                            summaryText += `*${text}\n`;
                        })
                        toastr.warning(summaryText);
                    }
                })
                    .fail(function () {
                        alert("error");
                    });
            })
    })

    $(document).on(
        "click",
        ".btn-delete",
        function (event) {
            event.preventDefault();
            var dataId = $(this).attr('data-id');
            var currentRow = $(`[name="row-${dataId}"]`);
            const tableRow = $(currentRow).find('td:eq(1)').text();
            //console.log(typeof dataId);

            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${tableRow} adlı kategori silinecektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet,silmek istiyorum.',
                cancelButtonText: 'Hayır,silmek istemiyorum.'
            }).
                then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            type: 'POST',
                            url: '/Admin/Category/Delete/',
                            dataType: "json", //Json verisi göndereceğimi söyledik.
                            data: {
                                categoryId: parseInt(dataId)
                            },
                            success: function (data) {
                                console.log("BAŞARILI!");
                                const result = jQuery.parseJSON(data);
                                console.log(result);

                                if (result.ResultStatus === 0) {
                                    Swal.fire(
                                        'Silindi!',
                                        `${result.Category.Name} adlı kategori başarıyla silinmiştir.`,
                                        'success'
                                    );
                                    $(currentRow).fadeOut(3500);

                                    //toastr.success(`${ajaxData.Message}`,'Başarılı İşlem')
                                }
                                else {
                                    console.log('Result Status: ' + result.ResultStatus);
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Bir hata oluştu...',
                                        text: `${result.Category.Message}`
                                    });
                                }
                            },
                            error: function (error) {
                                console.log(error);
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Bir hata oluştu...',
                                    text: `${error.statusText}`
                                });
                            }
                        });
                    }
                });


        });

    $(function () {
        //_CategoryUpdatePartial view'i gelir.
        $(document).on('click', '.btn-edit', function (event) {
            var dataId = $(this).attr('data-id');
            var url = '/Admin/Category/Update/';
            //console.log(dataId);
            event.preventDefault();
            //console.log('EDIT GİRDİ!!!')
            const placeHolderDiv = $("#modalPlaceHolder");

            $.ajax({
                url: url,
                type: 'GET',
                data: {
                    categoryId: parseInt(dataId)
                },
                success: function (data) {
                    console.log("BAŞARILI!!!")
                    //console.log(data);
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');

                },
                error: function (error) {
                    console.log()
                    console.log("HATA!!!")
                    toastr.error("Bir Hata Oluştu!!");
                }
            });
        });
    });

    //Ajax POST / Updating a Category starts from here.s
    $(document).on('click',
        '#btnUpdate',
        function (event) {
            //console.log("UPDATE BUTONU");
            event.preventDefault();
           
            const form = $("#form-category-update");
            const actionUrl = '/Admin/Category/Update';
            const dataToSend = form.serialize(); //formdaki datayı serialize ederek tekrardan dataToSend değişkenine atadık.
            const placeHolderDiv = $("#modalPlaceHolder");
           
            //const isValid = newFormBody.find('[name="IsValid"]').val();

            $.ajax({
                type: 'POST',
                url: actionUrl,
                data: dataToSend,
                success: function (data) {
                    const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(categoryUpdateAjaxModel);
                    const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial); //CategoryUpdatePartial .modal-body'nin içine yazıyoruz.
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val();

                    if (isValid == 'True') {
                        console.log('VALID!')
                        const updatedCategory = categoryUpdateAjaxModel.CategoryDto.Category;
                        const updatedMessage = categoryUpdateAjaxModel.CategoryDto.Message;
                        
                        //console.log(currentRow);
                        console.log(updatedCategory);

                        const updatedTableRow = `
                            <tr name="row-${updatedCategory.Id}">
                                                <td>${updatedCategory.Id}</td>
                                                <td>${updatedCategory.Name}</td>
                                                <td>${updatedCategory.Description}</td>
                                                <td>${convertFirstLetterToUpperCase(updatedCategory.IsActive.toString())}</td>
                                                <td>${convertFirstLetterToUpperCase(updatedCategory.IsDeleted.toString())}</td>
                                                <td>${updatedCategory.Note}</td>
                                                <td>${converToShortDate(updatedCategory.CreatedDate)}</td>
                                                <td>${updatedCategory.CreateByName}</td>
                                                <td>${converToShortDate(updatedCategory.ModifiedDate)}</td>
                                                <td>${updatedCategory.ModifiedByName}</td>
                                                <td>
                                                     <button class="btn btn-warning btn-sm btn-edit" data-id="${updatedCategory.Id}"><span class="fas fa-edit"></span></button>
                                                     <button class="btn btn-danger btn-sm btn-delete" data-id="${updatedCategory.Id}"><span class="fas fa-minus-circle"></span></button>
                                                     </td>
                             </tr>`;
                        const updatedTableRowObject = $(updatedTableRow);
                        const currentRow = $(`[name="row-${updatedCategory.Id}"]`);
                        console.log(currentRow);
                        placeHolderDiv.find('.modal').modal('hide');
                        updatedTableRowObject.hide();
                        currentRow.replaceWith(updatedTableRowObject);
                        updatedTableRowObject.fadeIn(3500);
                        toastr.success(`${updatedMessage}`, 'Başarılı İşlemi')
                    }
                    else {
                        let summaryText = "";
                        console.log("VALİD DEĞİL!!!")
                        const errors = $("#validation-summary > ul > li");
                        $.each(errors, function () {
                            let text = $(this).text();
                            summaryText += `* ${text}\n`;
                        })
                        toastr.warning(summaryText);

                    }
                },
                error: function (response) {
                    console.log(response);
                }
            })
            
        }
    )
});
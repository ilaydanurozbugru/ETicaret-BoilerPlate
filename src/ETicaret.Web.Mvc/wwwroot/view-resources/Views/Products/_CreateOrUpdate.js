(function () {
    app.modals.CreateOrEditProdcutModal = function () {
        var l = abp.localization.getSource('ETicaret');
        var _modalManager;
        var _service = abp.services.app.product;
        var _$informationForm = null;
        var uploadedFileToken = null;
        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$informationForm = _modalManager.getModal().find('form[name=MainForm]');
            _$informationForm.validate({ ignore: "" }); 
            $('#Image').change(async function (event) {
                const fileInput = event.target;
                const formData = new FormData();
                const file = fileInput.files[0];

                if (!file) {
                    console.error('No file selected.');
                    return;
                }

                // Dosya tipi kontrolü
                const validTypes = ['image/jpg', 'image/jpeg', 'image/png', 'image/gif', 'image/webp'];
                if (!validTypes.includes(file.type)) {
                    alert('Geçersiz dosya türü. Yalnızca jpg, jpeg, png, gif ve webp dosyalarına izin verilir.');
                    return;
                }

                // Dosya boyutu kontrolü (5MB sınırı)
                const maxSizeInBytes = 5242880; // 5MB
                if (file.size > maxSizeInBytes) {
                    alert('Dosya boyutu 5MB\'yi geçemez.');
                    return;
                }

                // FormData'ya dosyayı ve ek verileri ekleyin
                formData.append('Picture', file);
                formData.append('FileType', file.type);
                formData.append('FileName', 'Picture');
                formData.append('FileToken', 'your_generated_token'); // Burada kendi token oluşturma metodunuzu kullanın 
                const token = _$informationForm.find('input[name="__RequestVerificationToken"]').val();
                if (!token) {
                    console.error('Request verification token not found.');
                    return;
                }
                formData.append('__RequestVerificationToken', token);

                try {
                    const response = await fetch("/File/UploadFile", {
                        method: 'POST',
                        body: formData,
                    });

                    const result = await response.json(); 
                    if (response.ok && result.success) {
                         
                        uploadedFileToken = result.result.fileToken; // Eğer bir token dönerse, burada işleyin
                    } else {
                        alert(`Hata: ${result.error ? result.error.message : 'Bilinmeyen hata'}`);
                    }
                } catch (error) {
                    console.error('Gönderim sırasında bir hata oluştu:', error);
                    alert('Dosya yüklenirken bir hata oluştu.');
                }
            });




           
        };

        this.save = function () {
             
            if (!_$informationForm.valid()) {
                return;
            }

            var data = _$informationForm.serializeFormToObject();
            data.ImageToken = uploadedFileToken
            _modalManager.setBusy(true);
            if (data.Id > 0) {
                _service.update(data).done(function () {
                    abp.notify.info(l('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditProdcutModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
                
            } else {
                _service.create(data).done(function () {
                    abp.notify.info(l('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditProdcutModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
            }
           
        };
    };
})();
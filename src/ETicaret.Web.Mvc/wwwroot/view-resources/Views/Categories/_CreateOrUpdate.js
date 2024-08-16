(function () {
    app.modals.CreateOrEditCategoryModal = function () {
        var l = abp.localization.getSource('ETicaret');
        var _modalManager;
        var _service = abp.services.app.category;
        var _$informationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$informationForm = _modalManager.getModal().find('form[name=MainForm]');
            _$informationForm.validate({ ignore: "" }); 
        };

        this.save = function () {
            if (!_$informationForm.valid()) {
                return;
            }

            var data = _$informationForm.serializeFormToObject(); 
            _modalManager.setBusy(true);
            if (data.Id > 0) {
                _service.update(data).done(function () {
                    abp.notify.info(l('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditCategoryModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
                
            } else {
                _service.create(data).done(function () {
                    abp.notify.info(l('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditCategoryModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
            }
           
        };
    };
})();
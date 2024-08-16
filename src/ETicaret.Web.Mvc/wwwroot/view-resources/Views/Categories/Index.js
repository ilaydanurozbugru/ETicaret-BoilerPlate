(function ($) {
    var _service = abp.services.app.category,
        l = abp.localization.getSource('ETicaret');
    var _permissions = {
        create: abp.auth.hasPermission('Pages.Category.Create'),
        edit: abp.auth.hasPermission('Pages.Category.Update'),
        'delete': abp.auth.hasPermission('Pages.Category.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Categories/CreateOrUpdate',
        scriptUrl: abp.appPath + 'view-resources/Views/Categories/_CreateOrUpdate.js',
        modalClass: 'CreateOrEditCategoryModal',
        cssClass: 'scrollable-modal'
    }); 

    var _$table = $('#mainTable').DataTable({
        paging: true,
        serverSide: true,
        processing:true,
        listAction: {
            ajaxFunction: _service.getList,
            inputFilter: function () {
                return $('#searchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$productsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                data: 'name',
                sortable: false,
                width: '30%'
            },
            {
                targets: 1,
                data: "Actions",
                responsivePriority: -1,
                orderable: false,
                autoWidth: false,
                defaultContent: '',
                rowAction: {
                    text: '<i class="fa fa-cog"></i> ' + l('Actions') + ' <span class="caret"></span>',
                    items: [{
                        text: l('Edit'),
                        visible: function () {
                            return _permissions.edit;
                        },
                        action: function (data) {
                            debugger
                            _createOrEditModal.open({ id: data.record.id });
                        }
                    }, {
                        text: l('Delete'),
                        visible: function () {
                            return _permissions.delete;
                        },
                        action: function (data) {
                            deleteData(data.record);
                        }
                    }]
                }
            }
        ]
    });

    abp.event.on('app.createOrEditCategoryModalSaved', (data) => {
        reloadTable();
    });

    function reloadTable() {
        _$table.ajax.reload();
    }

    function deleteData(data) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                data.name),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _service.delete({
                        id: data.id
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        reloadTable();
                    });
                }
            }
        );
    }
    $("#createButton").click(function(){
        _createOrEditModal.open();
    });

    $("#clearButton").click(function () {
        app.resetFilter($('#searchForm')); 
        reloadTable();
    });

    $("#searchButton").click(function () {
        reloadTable();
    });

    
})(jQuery);
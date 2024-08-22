(function ($) {
    var _service = abp.services.app.product,
        l = abp.localization.getSource('ETicaret'),
        _$modal = $('#ProductCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ProductsTable');
    var _permissions = {
        create: abp.auth.hasPermission('Pages.Product.Create'),
        edit: abp.auth.hasPermission('Pages.Product.Update'),
        'delete': abp.auth.hasPermission('Pages.Product.Delete'),
    };
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Products/CreateOrUpdate',
        scriptUrl: abp.appPath + 'view-resources/Views/Products/_CreateOrUpdate.js',
        modalClass: 'CreateOrEditProdcutModal',
        cssClass: 'scrollable-modal'
    }); 
    var _$table = _$table.DataTable({
        paging: true,
        serverSide: true,
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
                data: "FileObjectId",
                render: function (id, ype, row, meta) {
                    
                    
                    if (row.imageId) {
                        var profilePictureUrl = "/File/GetImageById?id=" + row.imageId;

                        return "<img src='" + profilePictureUrl + "' style='max-height:50px' class='img-circle' />";
                    }

                    return "";
                }
            },
            {
                targets: 1,
                data: 'productName',
                sortable: false
            },
            {
                targets: 2,
                data: 'categoryName',
                sortable: false
            },
            {
                targets: 3,
                data: 'description',
                sortable: false
            },
            {
                targets: 4,
                data: 'price',
                sortable: false
            },
            {
                targets: 5,
                data: 'stockQuantity',
                sortable: false
            },
            {
                targets: 6,
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

    abp.event.on('app.createOrEditProdcutModalSaved', (data) => {
        reloadTable();
    });

    function reloadTable() {
        _$table.ajax.reload();
    }

    function deleteData(data) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                data.productName),
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
    $("#createButton").click(function () {
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
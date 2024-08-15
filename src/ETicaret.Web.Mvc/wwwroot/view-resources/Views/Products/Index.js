(function ($) {
    var _productService = abp.services.app.product,
        l = abp.localization.getSource('ETicaret'),
        _$modal = $('#ProductCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ProductsTable');

    var _$productsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _productService.getAll,
            inputFilter: function () {
                return $('#ProductsSearchForm').serializeFormToObject(true);
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
                data: 'productName',
                sortable: false,
                width: '30%'
            },
            {
                targets: 1,
                data: 'description',
                sortable: false,
                width: '20%'
            },
            {
                targets: 2,
                data: 'price',
                sortable: false,
                width: '15%'
            },
            {
                targets: 3,
                data: 'stockQuantity',
                sortable: false,
                width: '15%'
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                width: '20%',
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `<button type="button" class="btn btn-sm bg-secondary edit-product" data-product-id="${row.id}" data-toggle="modal" data-target="#ProductEditModal">`,
                        `<i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '</button>',
                        `<button type="button" class="btn btn-sm bg-danger delete-product" data-product-id="${row.id}" data-product-name="${row.productName}">`,
                        `<i class="fas fa-trash"></i> ${l('Delete')}`,
                        '</button>',
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.delete-product', function () {
        var productId = $(this).attr("data-product-id");
        var productName = $(this).attr('data-product-name');

        deleteProduct(productId, productName);
    });

    $(document).on('click', '.edit-product', function (e) {
        var productId = $(this).attr("data-product-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Products/EditModal?productId=' + productId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProductEditModal div.modal-content').html(content);
            },
            error: function (e) {
                console.error('Error loading edit modal:', e);
            }
        })
    });

    abp.event.on('product.edited', (data) => {
        _$productsTable.ajax.reload();
    });

    function deleteProduct(productId, productName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                productName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _productService.delete({
                        id: productId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$productsTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$productsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$productsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
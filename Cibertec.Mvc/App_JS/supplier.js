(function (supplier) {
    supplier.success = successReload;
    supplier.pages = 1;
    supplier.rowSize = 10;
    /*Atributos para el manejo del Hub*/
    supplier.hub = {};
    supplier.ids = [];
    supplier.recordInUse = false;

    supplier.addSupplier = addSupplierId;
    supplier.removeHubSupplier = removeSupplierId;
    supplier.validate = validate;

    $(function () {
        connectToHub();
        init(1);
    })

    return supplier;

    function successReload(option) {
        cibertec.closeModal(option);
        elements = document.getElementsByClassName('active');
        activePage = elements[1].children;
        console.log(activePage[0].text);

        lstTableRows = $('.table >tbody >tr').length - 1;
        console.log(lstTableRows);

        if (option === "delete" && lstTableRows === 1) {
            cant = activePage[0].text;
            init(cant - 1);
        }
        else 
            init(activePage[0].text); 
    }

    function init(numPage) {
        $.get('/Supplier/Count/' + supplier.rowSize,
            function (data) {
                supplier.pages = data;
                $('.pagination').bootpag({
                    total: supplier.pages,
                    page: numPage,
                    maxVisible: 5,
                    leaps: true,
                    firstLastUse: true,
                    first: '← ',
                    last: '→ ',
                    wrapClass: 'pagination',
                    activeClass: 'active',
                    disabledClass: 'disabled',
                    nextClass: 'next',
                    prevClass: 'prev',
                    lastClass: 'last',
                    firstClass: 'first'
                }).on('page', function (event, num) {
                    getSuppliers(num);
                });
                getSuppliers(numPage);
            });
    }

    function getSuppliers(cantPages) {
        var url = '/Supplier/List/' + cantPages + '/' + supplier.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
            //console.log(data);
        });
    }

    function addSupplierId(id) {
        supplier.hub.server.addSupplierId(id);
    }

    function removeSupplierId(id) {
        supplier.hub.server.removeSupplierId(id);
    }

    function connectToHub() {
        supplier.hub = $.connection.supplierHub;
        supplier.hub.client.supplierStatus = supplierStatus;
    }

    function supplierStatus(supplierIds) {
        console.log(supplierIds);
        supplier.ids = supplierIds;
    }

    function validate(id) {
        supplier.recordInUse = (supplier.ids.indexOf(id) > -1);
        if (supplier.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn-save').html("");
        }
    }
})(window.supplier = window.supplier || {});
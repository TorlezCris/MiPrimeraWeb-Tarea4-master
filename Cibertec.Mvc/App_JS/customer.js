(function (customer) {
    customer.success = successReload;
    customer.pages = 1;
    customer.rowSize = 10;
    /*Atributos para el manejo del Hub*/
    customer.hub = {};
    customer.ids = [];
    customer.recordInUse = false;

    customer.addCustomer = addCustomerId;
    customer.removeHubCustomer = removeCustomerId;
    customer.validate = validate;

    $(function () {
        connectToHub();
        init(1);
    })

    return customer;

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
        $.get('/Customer/Count/' + customer.rowSize,
            function (data) {
                customer.pages = data;
                $('.pagination').bootpag({
                    total: customer.pages,
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
                    getCustomers(num);
                });
                getCustomers(numPage);
            });
    }

    function getCustomers(cantPages) {
        var url = '/Customer/List/' + cantPages + '/' + customer.rowSize;
        $.get(url, function (data) {
            $('.content').html(data);
            //console.log(data);
        });
    }

    function addCustomerId(id) {
        customer.hub.server.addCustomerId(id);
    }

    function removeCustomerId(id) {
        customer.hub.server.removeCustomerId(id);
    }

    function connectToHub() {
        customer.hub = $.connection.customerHub;
        customer.hub.client.customerStatus = customerStatus;
    }

    function customerStatus(customerIds) {
        console.log(customerIds);
        customer.ids = customerIds;
    }

    function validate(id) {
        customer.recordInUse = (customer.ids.indexOf(id) > -1);
        if (customer.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn-save').html("");
        }
    }
})(window.customer = window.customer || {});
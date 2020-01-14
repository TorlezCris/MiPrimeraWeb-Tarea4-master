(function (order) {
	order.success = successReload;
	order.pages = 1;
	order.rowSize = 10;
	/*Atributos para el manejo del Hub*/
	order.hub = {};
	order.ids = [];
	order.recordInUse = false;

	order.addOrder = addOrderId;
	order.removeHubOrder = removeOrderId;
	order.validate = validate;

	$(function () {
		connectToHub();
		init(1);
	})

	return order;

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
		$.get('/Orders/Count/' + order.rowSize,
			function (data) {
				order.pages = data;
				$('.pagination').bootpag({
					total: order.pages,
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
					getOrders(num);
				});
				getOrders(numPage);
			});
	}

	function getOrders(cantPages) {
		var url = '/Orders/List/' + cantPages + '/' + order.rowSize;
		$.get(url, function (data) {
			$('.content').html(data);
			//console.log(data);
		});
	}

	function addOrderId(id) {
		order.hub.server.addOrderId(id);
	}

	function removeOrderId(id) {
		order.hub.server.removeOrderId(id);
	}

	function connectToHub() {
		order.hub = $.connection.orderHub;
		order.hub.client.orderStatus = orderStatus;
	}

	function orderStatus(orderIds) {
		console.log(orderIds);
		order.ids = orderIds;
	}

	function validate(id) {
		order.recordInUse = (order.ids.indexOf(id) > -1);
		if (order.recordInUse) {
			$('#inUse').removeClass('hidden');
			$('#btn-save').html("");
		}
	}
})(window.order = window.order || {});
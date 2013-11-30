function ServerUpdate() { }

ServerUpdate.Init = function () {
	$("#txtBuyDate").datepicker({
		dateFormat: 'yy-mm-dd'
	});


	$("#selIdcCode").change(function () {
		ServerUpdate.GetRackInfo($(this).val());
	});

	$("#btnSave").click(ServerUpdate.Update);
	$("#btnClose").click(ServerUpdate.Close);
}

ServerUpdate.Update = function () {

	if ($("#txtSapNo").val() == "") {
		alert('SAP NO를 입력해주세요.');
		return;
	}

	if ($("#txtServerName").val() == "") {
		alert('ServerName을 입력해주세요.');
		return;
	}

	if ($("#txtSapNo").val() == "") {
		alert('SAP NO를 입력해주세요.');
		return;
	}

	if ($("#txtSerialNo").val() == "") {
		alert('Serial No를 입력해주세요.');
		return;
	}

	if ($("#txtQty").val() == "" || $.isNumeric($("#txtQty").val()) == false) {
		alert('수량을 숫자로 입력해주세요');
		return;
	}

	if ($("#selEquipTypeCode").val() == "") {
		alert('장비구분을 선택해주세요.');
		return;
	}

	if ($("#selSiteCode").val() == "") {
		alert('SITE를 선택해주세요.');
		return;
	}

	if ($("#selTaskCode").val() == "") {
		alert('업무구분을 선택해주세요.');
		return;
	}

	if ($("#selServiceCode").val() == "") {
		alert('서비스를 선택해주세요.');
		return;
	}

	if ($("#selIdcCode").val() == "") {
		alert('IDC를 선택해주세요.');
		return;
	}

    if ($("#selRackCode").val() == "") {
		alert('rack위치를 선택해주세요.');
		return;
	}

	if ($("#selOsCode").val() == "") {
		alert('OS를 선택해주세요.');
		return;
	}

	if ($("#selVenderCode").val() == "") {
		alert('Vender를 선택해주세요.');
		return;
	}

	if ($("#txtContent").val() == "") {
		alert('내용을 입력해주세요.');
		return;
	}

	if ($("#txtBuyDate").val() == "") {
		alert('구매일자를 입력해주세요.');
		return;
	}

	if ($("#txtRackStartLocationValue").val() == "" || $.isNumeric($("#txtRackStartLocationValue").val()) == false) {
		alert('Rack위치값을 숫자로 입력해주세요');
		return;
	}

	var data = {
		"Seq": $("#txtSeq").val(),
		"SapNo": $("#txtSapNo").val(),
		"SerialNo": $("#txtSerialNo").val(),
		"ServerName": $("#txtServerName").val(),
		"Qty": $("#txtQty").val(),
		"EquipTypeCode": $("#selEquipTypeCode").val(),
		"SiteCode": $("#selSiteCode").val(),
		"TaskCode": $("#selTaskCode").val(),
		"ServiceCode": $("#selServiceCode").val(),
		"IdcCode": $("#selIdcCode").val(),
		"RackCode": $("#selRackCode").val(),
		"RackStartLocationValue": $("#txtRackStartLocationValue").val(),
		"BuyDate": $("#txtBuyDate").val(),
		"OsCode": $("#selOsCode").val(),
		"VenderCode": $("#selVenderCode").val(),
		"Content": $("#txtContent").val()
	};

	if (!confirm('저장하시겠습니까?')) return;

	$.ajax({
		type: "POST",
		url: "/Asset/SetServerUpdate/",
		data: data,
		dataType: "JSON",
		success: function (result) {
			if (result != null && result.statusCode == 0) {
				alert('저장되었습니다.');
				opener.ServerList.Search();
				ServerUpdate.Close();
			}
		},
		error: function (xhr, status, error) { }
	});
}

ServerUpdate.Close = function () {
	window.close();
}

//RACK 리스트 조회
ServerUpdate.GetRackInfo = function (IdcCode) {
	
	if (IdcCode != null && IdcCode != "") {

		data = {
			"IdcCode": IdcCode
		};

		$.ajax({
			type: "POST",
			url: "/Code/GetAssetRackListByIdcCode/",
			data: data,
			dataType: "JSON",
			success: function (data) {
				if (data.length > 0) {
					$("#selRackCode").empty();
					$("#selRackCode").append("<option value=''>선택하세요.</option>");
					$.each(data, function (index, entry) {
						$("#selRackCode").append("<option value='" + entry.RackCode + "'>" + entry.RackName + "</option>");
					});
				}
			},
			error: function (xhr, status, error) { }
		});
	}
}
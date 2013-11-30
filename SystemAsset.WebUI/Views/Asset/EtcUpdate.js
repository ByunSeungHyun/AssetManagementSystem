function EtcUpdate() { }

EtcUpdate.Init = function () {
    $("#txtBuyDate").datepicker({
        dateFormat: 'yy-mm-dd'
    });


    $("#selIdcCode").change(function () {
        EtcUpdate.GetRackInfo($(this).val());
    });

    $("#btnSave").click(EtcUpdate.Update);
    $("#btnClose").click(EtcUpdate.Close);
}

EtcUpdate.Update = function () {



    if ($("#txtServerName").val() == "") {
        alert('HostName을 입력해주세요.');
        return;
    }




    if ($("#selEquipTypeCode").val() == "") {
        alert('장비구분을 선택해주세요.');
        return;
    }


    if ($("#selTaskCode").val() == "") {
        alert('업무구분을 선택해주세요.');
        return;
    }


    if ($("#selIdcCode").val() == "") {
        alert('IDC를 선택해주세요.');
        return;
    }

    if ($("#selRackCode").val() == "") {
        alert('RACK위치를 선택해주세요.');
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



    if ($("#txtRackStartLocationValue").val() == "" || $.isNumeric($("#txtRackStartLocationValue").val()) == false) {
        alert('Rack위치값을 숫자로 입력해주세요');
        return;
    }

    var data = {
        "Seq": $("#txtSeq").val(),
        "ServerName": $("#txtServerName").val(),
        "EquipTypeCode": $("#selEquipTypeCode").val(),
        "IdcCode": $("#selIdcCode").val(),
        "RackCode": $("#selRackCode").val(),
        "RackStartLocationValue": $("#txtRackStartLocationValue").val(),
        "VenderCode": $("#selVenderCode").val(),
        "Content": $("#txtContent").val()
    };

    if (!confirm('저장하시겠습니까?')) return;

    $.ajax({
        type: "POST",
        url: "/Asset/SetEtcUpdate/",
        data: data,
        dataType: "JSON",
        success: function (result) {

            if (result != null && result.statusCode == 0) {

                alert('저장되었습니다.');
                opener.EtcList.Search();
                EtcUpdate.Close();
            } else {
                alert("실패하였습니다.")
            }
        },
        error: function (xhr, status, error) { }
    });
}

EtcUpdate.Close = function () {
    window.close();
}

//RACK 리스트 조회
EtcUpdate.GetRackInfo = function (IdcCode) {

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
function NetworkList() { }

NetworkList.Init = function () {

	$("#btnNetworkAdd").click(NetworkList.Insert); 	//추가팝업
	$("#btnNetworkEdit").click(NetworkList.Update); //수정팝업
	$("#btnNetworkDel").click(NetworkList.Delete); 	//삭제
	$("#btnNetworkXlsDownload").click(NetworkList.XlsDownload); //엑셀 다운로드
	$("#btnNetworkXlsUpload").click(NetworkList.XlsUpload); 	//엑셀 업로드

	NetworkList.Search();
}


NetworkList.Search = function () {
    var data = {
        searchType: $("#searchType").val(),
        searchText: $("#searchText").val()
    };

    $("#jqGridList").setGridParam({
        url: '/Asset/GetNetworkList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

NetworkList.Insert = function () {
	var url = "/Asset/NetworkUpdate/";
	window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

NetworkList.Update = function () {
	var NetworkSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

	if (NetworkSeq.length == 0) {
		alert('수정할 정보를 선택해 주세요');
		return;
	}
	else if (NetworkSeq.length > 1) {
		alert('수정은 한 개씩만 가능합니다.');
		return;
	}

	var rowData = $("#jqGridList").jqGrid('getRowData', NetworkSeq);
	var url = "/Asset/NetworkUpdate/?Seq=" + rowData.Seq;
	window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

NetworkList.Delete = function () {
    var NetworkSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (NetworkSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < NetworkSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', NetworkSeq[i]);
        arrSeqList.push(rowData.Seq);
    }

    strSeqLIst = arrSeqList.join(',');
    if (arrSeqList.length > 1) {
        data = {
            SeqList: strSeqLIst
        };

        $.ajax({
            type: "POST",
            url: "/Asset/DelNetworkUpdateList/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    NetworkList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    else {
        data = {
            Seq:NetworkSeq[0]
        };

        $.ajax({
            type: "POST",
            url: "/Asset/DelNetworkUpdate/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    NetworkList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }

}

NetworkList.XlsDownload = function () {

	location.href = "/Asset/GetNetworkExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

NetworkList.XlsUpload = function () {


}

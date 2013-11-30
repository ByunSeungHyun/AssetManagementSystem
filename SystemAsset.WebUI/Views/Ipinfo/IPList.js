function IPList() { }

IPList.Init = function () {

	$("#btnIPAdd").click(IPList.Insert); 	//추가팝업
	$("#btnIPEdit").click(IPList.Update); //수정팝업
	$("#btnIPDel").click(IPList.Delete); 	//삭제
	$("#btnIPXlsDownload").click(IPList.XlsDownload); //엑셀 다운로드
	$("#btnIPXlsUpload").click(IPList.XlsUpload); 	//엑셀 업로드

	IPList.Search();
}


IPList.Search = function () {
    var data = {
        searchType: "0",
        searchText: ""
    };

    $("#jqGridList").setGridParam({
        url: '/Ipinfo/GetIPList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

IPList.Insert = function () {
	var url = "/Ipinfo/IPUpdate/";
	window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

IPList.Update = function () {
	var IPSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

	if (IPSeq.length == 0) {
		alert('수정할 정보를 선택해 주세요');
		return;
	}
	else if (IPSeq.length > 1) {
		alert('수정은 한 개씩만 가능합니다.');
		return;
	}

	var rowData = $("#jqGridList").jqGrid('getRowData', IPSeq);
	var url = "/Ipinfo/IPUpdate/?Seq=" + rowData.Seq;
	window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

IPList.Delete = function () {
    var IPSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (IPSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < IPSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', IPSeq[i]);
        arrSeqList.push(rowData.Seq);
    }

    strSeqLIst = arrSeqList.join(',');
    if (arrSeqList.length > 1) {
        data = {
            SeqList: strSeqLIst
        };

        $.ajax({
            type: "POST",
            url: "/Ipinfo/DelIPUpdateList/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    IPList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    else {
        data = {
            Seq:IPSeq[0]
        };

        $.ajax({
            type: "POST",
            url: "/Ipinfo/DelIPUpdate/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    IPList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }

}

IPList.XlsDownload = function () {

	location.href = "/Ipinfo/GetIPExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

IPList.XlsUpload = function () {


}

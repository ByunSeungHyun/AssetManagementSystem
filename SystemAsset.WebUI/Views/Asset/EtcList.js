function EtcList() { }

EtcList.Init = function () {
    $("#btnEtcAdd").click(EtcList.Insert); 	//추가팝업
    $("#btnEtcEdit").click(EtcList.Update); //수정팝업
    $("#btnEtcDel").click(EtcList.Delete); 	//삭제
    $("#btnEtcXlsDownload").click(EtcList.XlsDownload); //엑셀 다운로드
    $("#btnEtcXlsUpload").click(EtcList.XlsUpload); 	//엑셀 업로드

    EtcList.Search();
}

EtcList.Search = function () {
    var data = {
        searchType: $("#searchType").val(),
        searchText: $("#searchText").val()
    };

    $("#jqGridList").setGridParam({
        url: '/Asset/GetEtcList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

EtcList.Insert = function () {
    var url = "/Asset/EtcUpdate/";
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

EtcList.Update = function () {
    var EtcSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (EtcSeq.length == 0) {
        alert('수정할 정보를 선택해 주세요');
        return;
    }
    else if (EtcSeq.length > 1) {
        alert('수정은 한 개씩만 가능합니다.');
        return;
    }

    var rowData = $("#jqGridList").jqGrid('getRowData', EtcSeq);
    var url = "/Asset/EtcUpdate/?Seq=" + rowData.Seq;
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

EtcList.Delete = function () {

    var EtcSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (EtcSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < EtcSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', EtcSeq[i]);
        arrSeqList.push(rowData.Seq);

    }

    strSeqLIst = arrSeqList.join(',');
    if (arrSeqList.length > 1) {
        data = {
            SeqList: strSeqLIst
        };

        $.ajax({
            type: "POST",
            url: "/Asset/DelEtcUpdateList/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    EtcList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    else {
        data = {
            Seq: EtcSeq[0]
        };

        $.ajax({
            type: "POST",
            url: "/Asset/DelEtcUpdate/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    EtcList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }


}

EtcList.XlsDownload = function () {

    location.href = "/Asset/GetEtcExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

EtcList.XlsUpload = function () {


}

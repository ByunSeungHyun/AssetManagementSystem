function ServerList() { }

ServerList.Init = function () {

    $("#btnServerAdd").click(ServerList.Insert); 	//추가팝업
    $("#btnServerEdit").click(ServerList.Update); //수정팝업
    $("#btnServerDel").click(ServerList.Delete); 	//삭제
    $("#btnServerXlsDownload").click(ServerList.XlsDownload); //엑셀 다운로드
    $("#btnServerXlsUpload").click(ServerList.XlsUpload); 	//엑셀 업로드

    ServerList.Search();
}

ServerList.Search = function () {
    var data = {
        searchType: "0",
        searchText: $("#searchText").val()
    };

    $("#jqGridList").setGridParam({
        url: '/Asset/GetServerList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

ServerList.Insert = function () {
    var url = "/Asset/ServerUpdate/";
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

ServerList.Update = function () {
    var serverSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (serverSeq.length == 0) {
        alert('수정할 정보를 선택해 주세요');
        return;
    }
    else if (serverSeq.length > 1) {
        alert('수정은 한 개씩만 가능합니다.');
        return;
    }

    var rowData = $("#jqGridList").jqGrid('getRowData', serverSeq);
    var url = "/Asset/ServerUpdate/?Seq=" + rowData.Seq;
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

ServerList.Delete = function () {

    var serverSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (serverSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < serverSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', serverSeq[i]);
        arrSeqList.push(rowData.Seq);
    }

    strSeqLIst = arrSeqList.join(',');


    /*
    Modify 2012-11-28
    jungil
    */

    if (arrSeqList.length > 1) {
        data = {
            SeqList: strSeqLIst
        };
        $.ajax({
            type: "POST",
            url: "/Asset/DelServerUpdateList/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    ServerList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    else {

        data = {
            Seq: serverSeq[0]
        };
        $.ajax({
            type: "POST",
            url: "/Asset/DelServerUpdate/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    ServerList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    /*
    Modify End
    */

}

ServerList.XlsDownload = function () {

    location.href = "/Asset/GetServerExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

ServerList.XlsUpload = function () {


}

ServerList.ViewToolTip = function (data) {
    alert(data);
}

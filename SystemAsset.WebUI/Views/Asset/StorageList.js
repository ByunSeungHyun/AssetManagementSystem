function StorageList() { }

StorageList.Init = function () {

    $("#btnStorageAdd").click(StorageList.Insert); 	//추가팝업
    $("#btnStorageEdit").click(StorageList.Update); //수정팝업
    $("#btnStorageDel").click(StorageList.Delete); 	//삭제
    $("#btnStorageXlsDownload").click(StorageList.XlsDownload); //엑셀 다운로드
    $("#btnStorageXlsUpload").click(StorageList.XlsUpload); 	//엑셀 업로드

    StorageList.Search();
}

StorageList.Search = function () {
    var data = {
        searchType: "0",
        searchText: ""
    };

    $("#jqGridList").setGridParam({
        url: '/Asset/GetStorageList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

StorageList.Insert = function () {
    var url = "/Asset/StorageUpdate/";
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

StorageList.Update = function () {
    var storageSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (storageSeq.length == 0) {
        alert('수정할 정보를 선택해 주세요');
        return;
    }
    else if (storageSeq.length > 1) {
        alert('수정은 한 개씩만 가능합니다.');
        return;
    }

    var rowData = $("#jqGridList").jqGrid('getRowData', storageSeq);
    var url = "/Asset/StorageUpdate/?Seq=" + rowData.Seq;
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

StorageList.Delete = function () {

    var StorageSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (StorageSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < StorageSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', StorageSeq[i]);
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
            url: "/Asset/DelStorageUpdateList/",
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
            Seq: StorageSeq[0]
        };
        $.ajax({
            type: "POST",
            url: "/Asset/DelStorageUpdate/",
            data: data,
            dataType: "JSON",
            success: function (result) {
                if (result != null && result.statusCode == 0) {
                    alert('삭제되었습니다.');
                    StorageList.Search();
                }
            },
            error: function (xhr, status, error) { }
        });
    }
    /*
    Modify End
    */
}

StorageList.XlsDownload = function () {

    location.href = "/Asset/GetStorageExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

StorageList.XlsUpload = function () {


}

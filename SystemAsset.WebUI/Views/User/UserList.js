function UserList() { }

UserList.Init = function () {

    $("#btnUserAdd").click(UserList.Insert); 	//추가팝업
    $("#btnUserEdit").click(UserList.Update); //수정팝업
    $("#btnUserDel").click(UserList.Delete); 	//삭제
    $("#btnUserXlsDownload").click(UserList.XlsDownload); //엑셀 다운로드
    $("#btnUserXlsUpload").click(UserList.XlsUpload); 	//엑셀 업로드

    UserList.Search();
}

UserList.Search = function () {
    var data = {
        searchType: "0",
        searchText: ""
    };

    $("#jqGridList").setGridParam({
        url: '/User/GetUserList/',
        mtype: 'POST',
        postData: data
    });
    $("#jqGridList").trigger('reloadGrid');
}

UserList.Insert = function () {
    var url = "/User/UserUpdate/";
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

UserList.Update = function () {
    var UserSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (UserSeq.length == 0) {
        alert('수정할 정보를 선택해 주세요');
        return;
    }
    else if (UserSeq.length > 1) {
        alert('수정은 한 개씩만 가능합니다.');
        return;
    }

    var rowData = $("#jqGridList").jqGrid('getRowData', UserSeq);
    var url = "/User/UserUpdate/?Seq=" + rowData.Seq;
    window.open(url, "", 'width=760,height=388,resizable=no,scrollbars=no,toolbars=no,status=yes,menu=no');
}

UserList.Delete = function () {

    var UserSeq = jQuery("#jqGridList").jqGrid('getGridParam', 'selarrrow');

    if (UserSeq.length == 0) {
        alert('삭제할 정보를 선택해 주세요');
        return;
    }

    if (!confirm('삭제하시겠습니까?')) return;

    var arrSeqList = new Array();
    var strSeqLIst = "";
    for (var i = 0; i < UserSeq.length; i++) {
        var rowData = $("#jqGridList").jqGrid('getRowData', UserSeq[i]);
        arrSeqList.push(rowData.Seq);
    }

    strSeqLIst = arrSeqList.join(',');

    data = {
        SeqList: strSeqLIst
    };

    $.ajax({
        type: "POST",
        url: "/User/DelUserUpdateList/",
        data: data,
        dataType: "JSON",
        success: function (result) {
            if (result != null && result.statusCode == 0) {
                alert('삭제되었습니다.');
                UserList.Search();
            }
        },
        error: function (xhr, status, error) { }
    });

}

UserList.XlsDownload = function () {

    location.href = "/User/GetUserExcelList/?searchType=1&searchText=" + $("#searchText").val() + "&page=1&rows=10000";
}

UserList.XlsUpload = function () {


}

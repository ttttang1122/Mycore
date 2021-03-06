﻿$(function ($) {
    GetLoadNav();
});

function UseMenuTab(_html, n) {
    _html += '<dl class="layui-nav-child">';
    $.each(n, function (i) {
        var subrow = n[i];
        _html += '<dd  data-name="' + subrow.MenuName + '">';

        var childNodes = subrow.ChildNodes;
        if (childNodes.length > 0) {
            _html += '<a href="javascript:;" ><i class="' + subrow.MenuIcon + '"></i>' + subrow.MenuNameCN + '</a>';

            _html = UseMenuTab(_html, childNodes);
        }
        else {
            _html += '<a  data-id="' + subrow.MenuID + '"lay-href="' + subrow.MenuUrl + '" ><i class="' + subrow.MenuIcon + '"></i>' + subrow.MenuNameCN + '</a>';
        }
        _html += '</dd>';
    })
    _html += '</dl>';
    return _html;
};


function GetLoadNav() {
    var MenuData;
    var BtnData
    $.ajax({
        url: "/Main/GetMenuData",
        type: "get",
        dataType: "json",
        async: false,
        success: function (data) {
            MenuData = eval(data.UseMenuDatas);
        }
    });
    var Usedata = eval(MenuData.Result);
    var _html = "";
    $.each(Usedata, function (i) {
        var row = Usedata[i];
        if (row.MenuParentID == "0") {
            _html += '<li data-name="' + row.MenuName + '" class="layui-nav-item">';
            _html += '<a  href="javascript:;" lay-tips="' + row.MenuNameCN + '" lay-direction="2"><i class="' + row.MenuIcon + '"></i><cite>' + row.MenuNameCN + '</cite></a>';
            var childNodes = row.ChildNodes;
            if (childNodes.length > 0) {
                _html = UseMenuTab(_html, childNodes);
            }

            _html += '</li>';
        }
    });
    $("#LAY-system-side-menu").prepend(_html);
};
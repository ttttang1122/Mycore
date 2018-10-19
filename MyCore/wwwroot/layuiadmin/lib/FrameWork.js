$.FromOpen = function (id,title, url, width, height,Zindex) {
    top.layer.open({
        id: id,
        btn: ['确定', '关闭'],
        type: 2,
        title: title,
        area: [width, height],
        fixed: false, //不固定
        content: url,
        zIndex: Zindex,
        yes: function (index, layero) {
            var body = top.layer.getChildFrame('body', index);
            var iframeWin = top.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：
            iframeWin.submitForm();
        },
        btn2: function (index, layero) {
            return true;
        }
    });
}
$.FromOpenNoBtn = function (id, title, url, width, height, Zindex) {
    top.layer.open({
        id: id,
        type: 2,
        title: title,
        area: [width, height],
        fixed: false, //不固定
        content: url,
        zIndex: Zindex,       
    });
}
$.loading = function (bool, text) {
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    };
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}
$.currentWindow = function () {
    var iframeId = top.$(".layui-show").find(".layadmin-iframe").attr("id");
    return top.frames[iframeId];
}
$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
};
$.fn.formGetModel = function () {
    var element = $(this);
    var datas = {};
    var t = element.serializeArray();
    $.each(t, function () {
        datas[this.name] = this.value;
    });    
    return datas;
}

$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "true") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "select":
                    $id.val(value).trigger("change");
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            default:
                var value = $this.val() == "" ? "&nbsp;" : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/&nbsp;/g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};
$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                });
            }
        });
    } else {

    }
};
$.fn.authorizeButton = function () {
    var strurls = top.$(".layui-show").find(".layadmin-iframe").attr("src");
    var dataJson
    $.ajax({
        url: "/Main/GetBtn",
        data: { urls: strurls},
        type: "get",
        dataType: "json",
        async: false,
        success: function (data) {
            dataJson = eval(data.UseBtnData);
        }
    });
    var $element = $(this);
    $element.find('button[authorize=yes]').attr('authorize', 'no');
    $element.find('input[authorize=yes]').attr('authorize', 'no');
    if (dataJson != undefined) {
        $.each(dataJson, function (i) {
            $element.find('button[data-type=' + dataJson[i].MenuName + ']').attr('authorize', 'yes');
            $element.find('input[data-type=' + dataJson[i].MenuName + ']').attr('authorize', 'yes');
        });
    }
    $element.find('[authorize=no]').remove();
}

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
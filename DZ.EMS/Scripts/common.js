
function addDate(a, dadd) {
    //  var a = new Date(dd)
    a = a.valueOf()
    a = a + dadd * 24 * 60 * 60 * 1000
    a = new Date(a)
    return a;
}
function datenowstr() {
    var curr_time = new Date();
    var strDate = curr_time.getFullYear() + "-";
    strDate += curr_time.getMonth() + 1 + "-";
    strDate += curr_time.getDate();
    return strDate;
}
function dateaddstr(addday) {
    var curr_time = new Date();
    curr_time = addDate(curr_time, addday)
    var strDate = curr_time.getFullYear() + "-";
    strDate += curr_time.getMonth() + 1 + "-";
    strDate += curr_time.getDate();
    return strDate;
}
//��չeasyui������֤
$.extend($.fn.validatebox.defaults.rules, {
    //��֤����
    CHS: {
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: 'ֻ�����뺺��'
    },
    //�ƶ��ֻ�������֤
    mobile: {//valueֵΪ�ı����е�ֵ
        validator: function (value) {
            var reg = /^1[3|4|5|8|9]\d{9}$/;
            return reg.test(value);
        },
        message: '�����ֻ������ʽ��׼ȷ.'
    },
    //�����ʱ���֤
    zipcode: {
        validator: function (value) {
            var reg = /^[1-9]\d{5}$/;
            return reg.test(value);
        },
        message: '�ʱ�����Ƿ�0��ʼ��6λ����.'
    },
    //����
    pwd: {
        validator: function (value) {
            var reg = /^[0-9]\d{5}$/;
            return reg.test(value);
        },
        message: '����Ϊ6λ����.'
    },
    idcard: {// ��֤���֤
        validator: function (value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '���֤�����ʽ����ȷ'
    },
    minLength: {
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '���������٣�2�����ַ�.'
    },
    length: {
        validator: function (value, param) {
            var len = $.trim(value).length;
            return len >= param[0] && len <= param[1];
        },
        message: "�������ݳ��ȱ������{0}��{1}֮��."
    },
    //�û��˺���֤(ֻ�ܰ��� _ ���� ��ĸ) 
    account: {//param��ֵΪ[]��ֵ
        validator: function (value, param) {
            if (value.length < param[0] || value.length > param[1]) {
                $.fn.validatebox.defaults.rules.account.message = '�û������ȱ�����' + param[0] + '��' + param[1] + '��Χ';
                return false;
            } else {
                if (!/^[\w]+$/.test(value)) {
                    $.fn.validatebox.defaults.rules.account.message = '�û���ֻ�����֡���ĸ���»������.';
                    return false;
                } else {
                    return true;
                }
            }
        }, message: ''
    }
})
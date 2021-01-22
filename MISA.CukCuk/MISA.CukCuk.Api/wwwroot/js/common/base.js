class BaseJS {
    constructor() {
        this.host = "http://localhost:54485";
        this.apiRouter = null;
        this.setApiRouter();
        this.initEvents();
        this.loadData();
    }

    setApiRouter() {

    }

    /**
     * khởi tạo tất cả sự kiện cho các button 
     * 
     * */
    initEvents() {
        var me = this;
        //sự kiện click khi nhấn thêm mới:
        $('#btnAdd').click(me.btnAddOnClick.bind(me));

        //sự kiện khi ấn close
        $('#btnClose').click(function () {
            $('#dialog').css('display', ' none');
        })

        //sự kiện khi ấn nút Hủy
        $('#btnCancel').click(function () {
            $('#dialog').css('display', ' none');
        })

        //load lại sự kiện khi  nhấn button nạp:
        $('#btnRefresh').click(function () {
            //hiển thị dialog thông tin chi tiết 
            me.loadData();
        })

        //Ẩn form chi tiết khi nhấn  hủy
        $('#btnCancel').click(function () {
            //hiển thị dialog thông tin chi tiết 
            //dialodDetail.dialog('close');
        })

        //thực hiện lưu dữ liệu khi nhấn button [Lưu] trên trang chi tiết
        $('#btnSave').click(me.btnSaveOnClick.bind(me));

        //hiển thị 1 bản ghi chi tiết khi nhấn đúp chuột vào 1 bản ghi trên danh sách dữ liệu
        $('table tbody').on('dblclick', 'tr', function () {
            $(this).addClass('row-selected');
            //load form

            // load dữ liệu cho các combobox:
            me.loadCombobox($('option[fieldName]'))

          me.FormMode = 'Edit';
            //Lấy khóa chính của bản ghi:
            var recordId = $(this).data('recordId');
            me.recordId = recordId;
            //gọi service lấy thông tin chi tiết qua Id:
            $.ajax({
                url: me.host + me.apiRouter + `/${recordId}`,
                method: "GET",
            }).done(function (res) {
                //Bindding dữ liệu lên form chi tiết 

                // lấy tất cả các control nhập liệu
                var inputs = $('input[fieldName], select[fieldName]');
                var entity = {};
                $.each(inputs, function (index, input) {
                    var propertyName = $(this).attr('fieldname');
                    var value = res[propertyName];
                    console.log(value);
                    //lấy giá trị check box
                    if (propertyName == "Gender") {
                        if (res[propertyName] == "1") {
                            $('#rdMale').prop("checked", true);
                            $('#rdFemale').prop("checked", false);
                        }
                        else if (res[propertyName] == "0") {
                            $('#rdMale').prop("checked", false);
                            $('#rdFemale').prop("checked", true);
                        }
                    }
                    //lấy ngày 
                    if (propertyName == "DateOfBirth") {
                        value = formatDate(value);
                        $(this).attr('value', value);
                    }

                    $(this).val(value);
                    //xét lại value checkbox như mặc định
                    $('#rdMale').val('1');
                    $('#rdFemale').val('0');
                })
            }).fail(function (res) {

            })

            $('#dialog').css('display', 'block');
        })


        /**---------------
         * validate bắt buộc nhập
         * Created: HTTRANG(31/12/2020)
         * */


        $('input[required]').blur(function () {
            //kiểm tra dữ liệu đã nhập, nếu bỏ trống thì cảnh báo:
            var value = $(this).val();
            if (!value) {
                // this.classList.add("txtCustomerCode");
                $(this).addClass('border-red');
                $(this).attr('title', 'Trường này không được phép để trống');
                $(this).attr("validate", false);
            }
            else {
                $(this).removeClass('border-red');
                $(this).attr("validate", true);
            }

        })

        /**---------------
        * validate email đúng định dạng
        * Created: HTTRANG(31/12/2020)
        * */
        $('input[type="email"]').blur(function () {
            var value = $(this).val();
            var testEmail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            if (!testEmail.test(value)) {
                $(this).addClass('border-red');
                $(this).attr('title', 'Email không đúng định dạng');
            }
            else {
                $(this).removeClass('border-red');
            }
        })

    }
    /**
     * load dữ liệu
     * createBy: HTTRANG
     * */
    loadData() {
        $('table tbody').empty();
        var me = this;
        try {
            var colums = $('table thead th')
            var getDataUrl = this.getDataUrl;
            $.ajax({
                url: me.host + me.apiRouter,
                method: "GET",
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    var tr = $(`<tr></tr>`);
                    $(tr).data("recordId", obj.EmployeeId);
                    //lấy thông tin dữ liệu sẽ map tương ứng với các cột
                    $.each(colums, function (index, th) {
                        var td = $(`<td></td>`);
                        td.addClass("width");
                        var fieldName = $(th).attr('fieldname');
                        var value = obj[fieldName];
                        //lấy dữ liệu với radio button
                        if (fieldName == "Gender") {
                            if (value == 0)
                                value = "Nam";
                            else if (value == 1)
                                value = "Nữ";
                            else value = "Khác";
                        }
                        
                        //định dạng dữ liệu lấy ra
                        var formatType = $(th).attr('formatType');
                        switch (formatType) {
                            case "ddmmyyyy":
                                td.addClass("text-align-center");
                                value = formatDate(value);
                                break;
                            case "Money":
                                td.addClass("text-align-right");
                                value = formatMoney(value);
                                break;
                            default:
                        }
                        td.append(value);
                        $(tr).append(td);
                    })
                    $('table tbody').append(tr);
                })
            }).fail(function (res) { })
        }
        catch (e) {
            console.log(e);
        }
        //lấy thông tin các cột dữ liệu

    }

    /**
     * Hàm xử lý khi nhấn button thêm mới
     * CreatedBy HTTrang(07/01/2021)
     * */
    btnAddOnClick() {
        try {
            var me = this;
            me.FormMode = 'Add';
            //hiển thị dialog thông tin chi tiết 
            $('#dialog').css('display', ' block');
            $('input:not(input[type="radio"])').val(null);
            //load dữ liệu cho các combobox 
            me.loadCombobox();
            me.loadCombobox($('select#cbxPositionGroup'));
            me.loadCombobox($('select#cbxDepartmentGroup'));
        } catch (e) {
            console.log(e);
        }
    }
    /**
    * Hàm xử lý khi nhấn button Save
    * CreatedBy HTTrang(07/01/2021)
    * */
    btnSaveOnClick() {
        var me = this;
        //validate dữ liệu
        var inputValidates = $('input[required], input[type="email"]');
        $.each(inputValidates, function (index, input) {
            $(input).trigger('blur');
        })
        var inputNotValids = $('input[validate="false"]');
        if (inputNotValids && inputNotValids.length > 0) {
            alert("Dữ liệu không hợp lệ vui lòng kiểm tra lại");
            inputNotValids[0].focus();
            return;
        }
        //thu thập thông tin dữ liệu được nhập--> built thành object
        // lấy tất cả các control nhập liệu
        var inputs = $('input[fieldName], select[fieldName]');
        var entity = {};
        
        $.each(inputs, function (index, input) {
            var propertyName = $(this).attr('fieldName');
            var value = $(this).val();
            //Check với trường hợp input là radio, thì chỉ lấy value của input có attribute checked:
            if ($(this).attr('type') == "radio") {
                if (this.checked) {
                    entity[propertyName] = value;
                }

            } else {
                entity[propertyName] = value;
            }

        })
        
        console.log(entity);
        var method = "POST";
        if (me.FormMode == 'Edit') {
            method = "PUT";
            entity.CustomerId = me.recordId;
        }

        //gọi service tương ứng thực hiện lưu dữ liệu
        $.ajax({
            url: me.host + me.apiRouter,
            method: method,
            data: JSON.stringify(entity),
            contentType: 'application/json'
            //sau khi lưu thành công thì: 
            //+đưa ra thông báo thành công
            //+ẩn form chi tiết
            //+load lại dữ liệu  
        }).done(function (res) {
            
            //đưa ra thông báo cho người dùng
            console.log(entity);
            if (me.FormMode == 'Add') {
                alert('Thêm thành công!');
            } if (me.FormMode == 'Edit') {
                alert('Cập nhật thành công!');
            }
            //$("#content-mess").text('Thêm thành công');
            //$("#messenger").show();
            //$(function () {
            //    function show_popup() {
            //        $("#messenger").hide();
            //    };
            //    window.setTimeout(show_popup, 5000)
            //});
            //ẩn form chi tiết
            $('#dialog').css('display', ' none');
            me.loadData();
        }).fail(function (res) {
            console.log(res);
        })
      
    }

    btnDeleteOnClick() {
        var me = this;
        try {
            var result = confirm("Bạn có chắc chắn muốn xóa?");
            if (result) {
                $.ajax({
                    url: me.host + me.apiRouter + `/${me.recordId}`,
                    method: "DELETE"
                }).done(function (res) {
                    alert('Xóa thành công!');
                    $('#dialog').css('display', ' none');
                    me.loadData();
                }).fail(function (res) {

                })
            }
        } catch (e) {

        }
    }

    loadCombobox(select = $('select#cbxCustomerGroup')) {
        var me = this;
        select.empty();
        $.each(select, function (index, select) {
            // lấy dữ liệu nhóm khách hàng:
            var api = $(select).attr('api');
            var fieldName = $(select).attr('fieldId');
            var fieldValue = $(select).attr('fieldValue');
            $('.loading').show();
            $.ajax({
                url: me.host + api,
                method: "GET",
                async: true
            }).done(function (res) {
                if (res) {
                    $.each(res, function (index, obj) {
                        var option = $(`<option value="${obj[fieldValue]}">${obj[fieldName]}</option>`);
                        $(select).append(option);
                    })
                }
                $('.loading').hide();
            }).fail(function (res) {
                $('.loading').hide();
            })
        })
    }
}
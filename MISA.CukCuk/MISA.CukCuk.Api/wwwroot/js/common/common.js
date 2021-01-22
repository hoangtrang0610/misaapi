/**---------------
 * format dữ liệu ngày tháng sang ngày/tháng/năm
 * @param {any} date tham số kiểu dữ liệu bất kì
 */

function formatDate(date) {
    if (date != null) {
        date = new Date(date);
        if (Number.isNaN(date.getTime())) {
            return "";
        } else {
            var day = date.getDate(),
                month = date.getMonth() + 1,
                year = date.getFullYear();

            day = day < 10 ? '0' + day : day;
            month = day < 10 ? '0' + month : month;

            return month + '/' + day + '/' + year;
        }

    } else {
        return "";
    }
    
}


/**---------------
 * format lương
 * @param {number} money kiểu number
 */

function formatMoney(money) {
    if (money) {
        var num = parseFloat(money).toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
        return num;
    }
    else {
        return "";
    }

}


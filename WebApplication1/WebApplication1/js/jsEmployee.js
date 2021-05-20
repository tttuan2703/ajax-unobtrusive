const { post } = require("jquery");

function updateEmp(idEmp) {
    emp = $("#" + idEmp + ">#id").value;
    var name = document.getElementById("Name").value;
    var gender = document.getElementById("gender").value;
    var city = document.getElementById("city").value;
    var hinhAnh = document.getElementById("hinhAnh").value.split("\\")[document.getElementById("hinhAnh").value.split("\\").length - 1];
    var statusflag = document.getElementById("statusflag").value;
    var deptID = document.getElementById("deptID").value;
    console.log(name);
    console.log(gender);
    console.log(city);
    console.log(hinhAnh);
    console.log(statusflag);
    console.log(deptID);
    $.ajax({
        url: "/Employee/AccessUpdate?id=" + idEmp + "&Name=" + name + "&gender=" + gender + "&city=" + city + "&image=" + hinhAnh + "&flag=" + statusflag + "&deptId=" + deptID,
        success: function (res) {
            alert("Bạn đã chỉnh sửa thành công rồi!");
            window.location.reload();
            window.location.href="/Employee/Index"
        }
    })
}
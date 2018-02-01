/// <reference path="typings/jquery/jquery.d.ts" />
var Facade;
(function (Facade) {
    var GradeReport = (function () {
        function GradeReport() {
        }
        return GradeReport;
    }());
    Facade.GradeReport = GradeReport;
    var ReportController = (function () {
        function ReportController() {
        }
        ReportController.LoadReport = function () {
            var tableContent = $("#table_data_container");
            $.ajax({
                url: 'api/Report',
                type: 'GET',
                beforeSend: function () {
                    tableContent
                        .html('<tr class="odd"><td valign="top" colspan="6" class="dataTables_empty">Loading...</td></tr>');
                },
                success: function (gradeReports) {
                    tableContent.empty();
                    gradeReports.forEach(function (report, index) {
                        var isEven = index % 2 == 0;
                        var color = isEven ? "lightblue" : "white";
                        var row = $('<tr></tr>');
                        row.append("<td class=\"" + color + "\">" + report.Grade + "</td>");
                        row.append("<td class=\"" + color + "\">" + report.BestSubject + "</td>");
                        row.append("<td class=\"" + color + "\">" + report.WorstSubject + "</td>");
                        row.append("<td class=\"" + color + "\">" + report.BestStudent + "</td>");
                        row.append("<td class=\"" + color + "\">" + report.WorstStudent + "</td>");
                        row.append("<td class=\"" + color + "\">" + report.AvgScorePercentage + "</td>");
                        tableContent.append(row);
                    });
                }
            });
        };
        return ReportController;
    }());
    Facade.ReportController = ReportController;
})(Facade || (Facade = {}));
//# sourceMappingURL=App.js.map
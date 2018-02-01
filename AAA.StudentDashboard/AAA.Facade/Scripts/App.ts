/// <reference path="typings/jquery/jquery.d.ts" />

module Facade {
    export class GradeReport {
        public Grade: number;
        public BestSubject: string;
        public WorstSubject: string;
        public BestStudent: string;
        public WorstStudent: string;
        public AvgScorePercentage: string;
    }

    export class ReportController {
        public static LoadReport() {
            let tableContent = $("#table_data_container");

            $.ajax({
                url: 'api/Report',
                type: 'GET',
                beforeSend: function () {
                    tableContent
                        .html('<tr class="odd"><td valign="top" colspan="6" class="dataTables_empty">Loading...</td></tr>')
                },
                success: function (gradeReports: GradeReport[]) {
                    tableContent.empty();

                    gradeReports.forEach((report, index) => {

                        let isEven = index % 2 == 0
                        let color = isEven ? "lightblue" : "white";
                        let row = $('<tr></tr>');

                        row.append(`<td class="${color}">${report.Grade}</td>`);
                        row.append(`<td class="${color}">${report.BestSubject}</td>`);
                        row.append(`<td class="${color}">${report.WorstSubject}</td>`);
                        row.append(`<td class="${color}">${report.BestStudent}</td>`);
                        row.append(`<td class="${color}">${report.WorstStudent}</td>`);
                        row.append(`<td class="${color}">${report.AvgScorePercentage}</td>`);

                        tableContent.append(row);
                    });
                }
            });
        }
    }
}
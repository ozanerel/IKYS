using System.Reflection.Metadata;
using IK.BLL.Services.Abstracts;
using IK.ENTITIES.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace IK.BLL.Services.Concretes
{
    public class PayrollPdfService : IPayrollPdfService
    {
        public byte[] GeneratePayrollPdf(Payroll payroll)
        {
            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text("MAAŞ BORDROSU")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Çalışan: {payroll.Employee.FirstName} {payroll.Employee.LastName}");
                        column.Item().Text($"Dönem: {payroll.Period}");
                        column.Item().Text($"Toplam Saat: {payroll.TotalHours}");
                        column.Item().Text($"Brüt Maaş: {payroll.GrossSalary:C}");
                        column.Item().Text($"Kesinti: {payroll.Deductions:C}");
                        column.Item().Text($"Net Maaş: {payroll.NetSalary:C}")
                            .Bold();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Oluşturulma Tarihi: {DateTime.Now:dd.MM.yyyy}");
                });
            });

            return document.GeneratePdf();
        }
    }
}

using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartSupermarketFMartWPF
{
    /// <summary>
    /// Interaction logic for MonthlyReportsPage.xaml
    /// </summary>
    public partial class MonthlyReportsPage : Page
    {
        public MonthlyReportsPage()
        {
            InitializeComponent();
            LoadReports();
        }

        private async void LoadReports()
        {
            var reportData = await GetMonthlySalesReportAsync(DateTime.Today.AddMonths(-12), DateTime.Today);
            ReportsListView.ItemsSource = reportData;
        }

        public async Task<List<MonthlySalesReport>> GetMonthlySalesReportAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = new FmartDbContext())
            {
                return await context.Orders
                    .Where(s => s.OrderDate.HasValue && s.OrderDate >= startDate && s.OrderDate <= endDate)
                    .GroupBy(s => new { s.OrderDate.Value.Year, s.OrderDate.Value.Month })
                    .Select(g => new MonthlySalesReport
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalSales = g.Count(),
                        TotalRevenue = g.Sum(s => s.TotalAmount)
                    })
                    .OrderBy(r => r.Year).ThenBy(r => r.Month)
                    .ToListAsync();
            }
        }

        // Define a DTO for the monthly report
        public class MonthlySalesReport
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int TotalSales { get; set; }
            public decimal TotalRevenue { get; set; }
        }
    }
}

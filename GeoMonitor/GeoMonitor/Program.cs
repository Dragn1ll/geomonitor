using System;
using System.IO;
using System.Linq;

namespace GeoMonitor
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите дату для выгрузки данных (в формате ГГГГ-ММ-ДД) или оставьте поле пустым для " +
                              "выгрузки за весь период:");
            var input = Console.ReadLine();

            DateTime? filterDate = null;
            if (!string.IsNullOrWhiteSpace(input) && DateTime.TryParse(input, out var parsedDate))
            {
                filterDate = parsedDate.Date;
            }

            try
            {
                using var db = new DbConnection();
                var query =
                    from m in db.Measurements
                    join w in db.Wells on m.WellId equals w.Id
                    join d in db.Divisions on w.DivisionId equals d.Id
                    join mt in db.MeasurementTypes on m.TypeId equals mt.Id
                    where !filterDate.HasValue || m.Timestamp.Date == filterDate.Value
                    group m by new { d.Name, WellName = w.Name, Date = m.Timestamp.Date, TypeName = mt.Name } into g
                    orderby g.Key.Name, g.Key.WellName, g.Key.Date
                    select new
                    {
                        DivisionName = g.Key.Name,
                        WellName = g.Key.WellName,
                        MeasurementDate = g.Key.Date,
                        MeasurementType = g.Key.TypeName,
                        MinValue = g.Min(x => x.Value),
                        MaxValue = g.Max(x => x.Value),
                        Count = g.Count()
                    };

                var results = query.ToList();
                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), 
                    $"data_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv");

                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("Подразделение;Название скважины;Дата замера;Тип замера;Минимальное значение;" +
                                     "Максимальное значение;Количество замеров");
                    
                    foreach (var row in results)
                    {
                        writer.WriteLine($"{row.DivisionName};{row.WellName};{row.MeasurementDate:yyyy-MM-dd};" +
                                         $"{row.MeasurementType};{row.MinValue};{row.MaxValue};{row.Count}");
                    }
                }

                Console.WriteLine($"Данные успешно выгружены в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
using LinqToDB.Mapping;

namespace GeoMonitor.Tables
{
    [Table("measurement_types")]
    public class MeasurementType
    {
        [PrimaryKey, Identity]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
    }
}
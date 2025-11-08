using System;
using LinqToDB.Mapping;

namespace GeoMonitor.Tables
{
    [Table("measurements")]
    public class Measurement
    {
        [PrimaryKey, Identity]
        [Column("id")]
        public long Id { get; set; }
        
        [Column("well_id")]
        public int WellId { get; set; }
        
        [Column("type_id")]
        public int TypeId { get; set; }
        
        [Column("value")]
        public decimal Value { get; set; }
        
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
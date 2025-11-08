using System;
using LinqToDB.Mapping;

namespace GeoMonitor.Tables
{
    [Table("wells")]
    public class Well
    {
        [PrimaryKey, Identity]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("commissioning_date")]
        public DateTime CommissioningDate { get; set; }
        
        [Column("division_id")]
        public int DivisionId { get; set; }
    }
}
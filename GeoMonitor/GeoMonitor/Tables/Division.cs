using LinqToDB.Mapping;

namespace GeoMonitor.Tables
{
    [Table("divisions")]
    public class Division
    {
        [PrimaryKey, Identity]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
    }
}
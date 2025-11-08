using GeoMonitor.Tables;
using LinqToDB;
using LinqToDB.Data;

namespace GeoMonitor
{
    public class DbConnection : DataConnection
    {
        public DbConnection() : base(
            ProviderName.PostgreSQL, 
            "Host=localhost;Port=5433;Database=geo_monitor;Username=postgres;Password=postgres;Pooling=true;"
            )
        {}
        
        public ITable<Division> Divisions => this.GetTable<Division>();
        public ITable<Well> Wells => this.GetTable<Well>();
        public ITable<Measurement> Measurements => this.GetTable<Measurement>();
        public ITable<MeasurementType> MeasurementTypes => this.GetTable<MeasurementType>();
    }
}
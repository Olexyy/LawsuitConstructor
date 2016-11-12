using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("StationsDBs")]
    public class StationsDB
    {
        [Key]
        public int StationId { get; set; }
        public string Address { get; set; }
        public int MaxVolume92 { get; set; }
        public int MaxVolume95 { get; set; }
        public int MaxVolume98 { get; set; }
        public int CurrentVolume92 { get; set; }
        public int CurrentVolume95 { get; set; }
        public int CurrentVolume98 { get; set; }
    }
    public class StationsDBContext : DbContext
    {
        public DbSet<StationsDB> FetchStations { get; set; }
    }
}
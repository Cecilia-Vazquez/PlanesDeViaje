//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlanesDeViajes.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PlanDeViajeEntities : DbContext
    {
        public PlanDeViajeEntities()
            : base("name=PlanDeViajeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Hoteles> Hoteles { get; set; }
        public virtual DbSet<Plan_Detalle> Plan_Detalle { get; set; }
        public virtual DbSet<Planes> Planes { get; set; }
        public virtual DbSet<Reservaciones> Reservaciones { get; set; }
    }
}

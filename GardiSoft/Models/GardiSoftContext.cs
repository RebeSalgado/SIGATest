using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GardiSoft.Models
{
    public class GardiSoftContext : DbContext
    {

        public GardiSoftContext()
            :base("GardiSoftConnection")
        {
            Database.SetInitializer<GardiSoftContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //DONT DO THIS ANYMORE
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Vote>().ToTable("Votes")
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Entidades.Sys.SubProyecto> SubProyectoes { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Tipo> Tipo { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Aspecto> Aspecto { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Dnc> Dncs { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Trabajadores> Trabajadores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.TipoEfc> TipoEfc { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Capacitacion> Capacitacion { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.CursoTrabajadores> CursoTrabajadores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.TipoCapacitacion> TipoCapacitacion { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.HistoricoOtic> HistoricoOtic { get; set; }

        public System.Data.Entity.DbSet<Entidades.Global.Trabajador> Trabajador { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.GuiaDespacho> GuiaDespachoes { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.Referencia> Referencia { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.Detalle> Detalle { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.Evento> Evento { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.Situacion> Situacions { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Modulo> Modulo { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Menu> Menu { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.SubMenu> SubMenu { get; set; }

        public System.Data.Entity.DbSet<Entidades.Contabilidad.GuiaDespacho.Folios> Folio { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Tarja.Tarja> Tarja { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Tarja.TarjaCodigos> TarjaCodigos { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Tarja.TarjaTrabajadores> TarjaTrabajadores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Tarja.TarjaUbicaciones> TarjaUbicaciones { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Permisos> Permisos { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Usuario> Usuario { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Externa> Externas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.ExternaTrabajadores> ExternaTrabajadores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Comunas> Comunas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Escolaridad> Escolaridad { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.Nivel> Nivel { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Maquina> Maquinas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Plan> Plans { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.TipoMaquina> TipoMaquinas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Ubicacion> Ubicacions { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.PlanPartes> PlanPartes { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Actividades> Actividades { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Lecturas> Lecturas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.MaquinaActividad> MaquinaActividad { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.HistoricoComponentes> HistoricoComponente { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.TipoOt> tipoOt { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OT> Ot { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.DetalleCorrectivo> DetalleOt { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Aplicaciones> Aplicaciones { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.TipoSoporte> TipoSoporte { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.ControlDeCambios> ControlDeCambios { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Capacitaciones.DncEstados> DncEstados { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OtManoDeObra> OtManoDeObra { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OtDetalleCorrectivo> OtDetalleCorrectivo { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OrigenDeFalla> OrigenDeFalla { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.FallasOt> FallasOt { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.TiposDeFalla> TiposDeFalla { get; set; }
               
        public System.Data.Entity.DbSet<Entidades.Uma.RepuestoCorrectivosEntregados> RepuestoCorrectivosEntregados { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.RepuestosCorrectivosSolicitados> RepuestosCorrectivosSolicitados { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OtRepuestosSolicitados> OtRepuestosSolicitados { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.OtRepuestosEntregados> OtRepuestosEntregados { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.RepuestosOriginales> RepuestosOriginales { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.RepuestosAlternativos> RepuestosAlternativos { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.CalendarioOperacionesPermisos> CalendarioOperacionesPermisos { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.RepuestosActividades> RepuestosActividades { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.PermisosBodega> PermisosBodega { get; set; }

        public System.Data.Entity.DbSet<Entidades.Rrhh.Seleccion2016> Seleccion2016 { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Indicadores> Indicadores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Unidades> Unidades { get; set; }

        public  System.Data.Entity.DbSet<Entidades.Control.Valores> Valores { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Control> Control { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.Servicios> Servicios { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Kpi> Kpi { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.PlanMaster> PlanMaster { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.PlanPartesMaster> PlanPartesMaster { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.ActividadesMaster> ActividadesMaster { get; set; }

        public System.Data.Entity.DbSet<Entidades.Uma.RepuestosActividadesMaster> RepuestosActividadesMaster { get; set; }

        public System.Data.Entity.DbSet<Entidades.Sys.Perfil> Perfil { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Area> Areas { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Nivel_Control> Nivel_Control { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Turno> Turno { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.ControlIndicador> Control_Registro { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Indicador> Indicadors { get; set; }

        public System.Data.Entity.DbSet<Entidades.Control.Proyecto> Proyectoes { get; set; }
    }
}
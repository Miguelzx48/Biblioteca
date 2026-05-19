using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biblioteca.Models
{
    public class Prestamo 
    {
        public int IdPrestamo { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaEstimadaDevolucion { get; set; }
        public DateTime? FechaRealDevolucion { get; set; }
        public int Multa { get; set; }
        public int CantidadDiasMora { get; set; }
        public int Estado  { get; set; }
        public Prestamo(){}
        public Prestamo(int idPrestamo, int idUsuario, int idLibro, DateTime fechaPrestamo, DateTime fechaEstimadaDevolucion, DateTime fechaRealDevolucion, int multa, int cantidadDiasMora, int estado)
        {
            IdPrestamo = idPrestamo;
            IdUsuario = idUsuario;
            IdLibro = idLibro;
            FechaPrestamo = fechaPrestamo;
            FechaEstimadaDevolucion = fechaEstimadaDevolucion;
            FechaRealDevolucion = fechaRealDevolucion;
            Multa = multa;
            CantidadDiasMora = cantidadDiasMora;
            Estado = estado;
        }
    }
}

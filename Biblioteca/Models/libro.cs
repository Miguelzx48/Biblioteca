using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    internal class libro
    {
        public int IdLibro { get; set; }
        public string Nombre { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public string Editorial { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public int AnioPublicaion { get; set; }
        
     

        public libro() { }

        public libro(int idLibro , string nombre , string autor ,string categoria, string editorial , int stock , string descripcion , int anioPublicaion )
        {
            IdLibro = idLibro;
            Nombre = nombre;
            Autor = autor;
            Categoria = categoria;
            Editorial = editorial;
            Stock = stock;
            Descripcion = descripcion;
            AnioPublicaion = anioPublicaion;  
           
       
    }


}
}

    
       
namespace ApiRest.Models
{
    public class Libros
    {
        //clase modelo para el controlador que emula la tabla Libros de la db
        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Paginas { get; set; }
    }
}

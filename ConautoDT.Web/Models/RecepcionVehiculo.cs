namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class RecepcionVehiculo
    {

        public List<Formulario> listFormulario { get; set; }
    }



    public class Formulario
    {
        public string FormName { get; set; }


        public long IdForm { get; set; }

        public DateTime FechaRegistro { get; set; }
    }




}

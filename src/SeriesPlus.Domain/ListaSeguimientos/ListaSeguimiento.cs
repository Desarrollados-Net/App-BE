using SeriesPlus.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.ListaSeguimientos
{
    public class ListaSeguimiento : AggregateRoot<int> 
    {
        public List<Serie> Series { get; set; } //cuando se crea este objeto

        public ListaSeguimiento() //mediante este constructo creamos una listaSeries
        {
            Series = new List<Serie>();
        }

        public DateOnly FechaModificacion { get; set; }
    }
}

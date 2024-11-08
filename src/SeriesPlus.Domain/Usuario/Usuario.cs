using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesPlus.Usuario
{
    internal class Usuario
    {
        public string ApeNom { get; set; }
        public string NickName { get; set; }
        private string Key { get; set; }
        private int idUsuario { get; set; }
        public bool Activo { get; set; }
        public string imagen { get; set; } //Todavía ni idea de como se implementan imagenes

        public Usuario(string nombre, string nick, string clave)
        {
            ApeNom = nombre;
            NickName = nick;   
            Key = clave;
            Activo = true;
            idUsuario = 0; //Todos los id se inicializaran en 0 hasta que sepa como incrementarlo cada que se crea un usuario
        }

    }
}

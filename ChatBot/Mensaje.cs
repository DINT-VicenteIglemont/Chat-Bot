using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    class Mensaje
    {
        public string Origen { get; set; }
        public string Contenido { get; set; }

        public Mensaje(string origen, string contenido)
        {
            Origen = origen;
            Contenido = contenido;
        }

        public override string ToString()
        {
            return $"{Origen}: {Contenido} \n";
        }
    }
}

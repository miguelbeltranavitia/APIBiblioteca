﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class FILTROPROCESOSController : ApiController
    {
        public DataTable Get(string fecha_desde, string fecha_hasta, string tipoFiltro)
        {
           
            QueryData consulta = new QueryData();
            var query = consulta.GetFiltroProcesos(fecha_desde, fecha_hasta, tipoFiltro);

            return query;
        }
    }
}

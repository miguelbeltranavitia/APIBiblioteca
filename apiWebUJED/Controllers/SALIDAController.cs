using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class SALIDAController : ApiController
    {
        public Object Put(string matricula)
        {
            string FunctionData = "PROC_SALIDA";
            UpdateData update = new UpdateData();
            var query = update.RegistrarSalida(FunctionData, matricula);
            return query;

        }
    }
}

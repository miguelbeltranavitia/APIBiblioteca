using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class BUSCARController : ApiController
    {
        public DataTable Get(string paterno)
        {
            string FunctionData = "F_BIBLIOTECA_BUSCAR";
            QueryData consulta = new QueryData();
            var query = consulta.GetBuscarUsuarios(FunctionData, paterno);

            return query;
        }
    }
}

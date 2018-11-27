using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class PROCESOSController : ApiController
    {
       public DataTable Get()
        {
            string FunctionData = "F_PROCESOS";
            QueryData consulta = new QueryData();
            var query = consulta.ShowProcesos(FunctionData);

            return query;
        }

    }
}

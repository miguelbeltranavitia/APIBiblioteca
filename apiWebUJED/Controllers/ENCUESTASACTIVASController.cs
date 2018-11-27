using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class ENCUESTASACTIVASController : ApiController
    {
        public DataTable Get()
        {
            string FunctionData = "F_ENCUESTAS_ACTIVAS";
            QueryData consulta = new QueryData();
            var query = consulta.GetEncuestasActivas(FunctionData);

            return query;
        }
    }
}

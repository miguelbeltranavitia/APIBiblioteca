using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class PREGUNTASController : ApiController
    {
        public DataTable Get()
        {
            string FunctionData = "F_PREGUNTAS";
            QueryData consulta = new QueryData();
            var query = consulta.ShowPreguntas(FunctionData);

            return query;
        }

    }
}

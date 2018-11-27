using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class EXISTENCUESTAACTIVAController : ApiController
    {
        public int Get(string matricula)
        {
            QueryData consulta = new QueryData();
            var query = consulta.GetExistEncuestaActiva(matricula);

            return query;
        }
    }
}

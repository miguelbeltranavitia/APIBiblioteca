using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class TIPOSUSUARIOController : ApiController
    {
        public DataTable Get()
        {
            string FunctionData = "F_TIPOS_USUARIO";
            QueryData consulta = new QueryData();
            var query = consulta.ShowTiposUsuario(FunctionData);

            return query;
        }
    }
}

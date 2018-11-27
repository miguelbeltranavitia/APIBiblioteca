using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class INSERTARENCUESTASController : ApiController
    {
        public Object Post([FromBody]Object data)
        {
            InsertData insert = new InsertData();
            var query = insert.InsertEncuestas(data);
            return query;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GoM.Feeds.Results
{
    public class JsonResult
    {
        public readonly JObject Result;

        public readonly Exception NetworkException;

        public HttpStatusCode? StatusCode;

        public readonly JsonReaderException JsonException;

        public bool Success => Result != null;

        public JsonResult(Exception nE, HttpStatusCode? c, JsonReaderException rE, JObject r )
        {
            Result = r;
            NetworkException = nE;
            StatusCode = c;
            JsonException = rE;
        }

    }
}

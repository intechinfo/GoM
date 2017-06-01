using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GoM.Feeds.Results;

namespace GoM.Feeds
{
    public abstract class FeedReaderBase : IFeedReader
    {
        readonly HttpClient _client;

        public FeedReaderBase()
        {
            _client = new HttpClient();
        }

        public virtual void Dispose()
        {
            _client.Dispose();
        }

        protected HttpClient HttpClient => _client;

        /// <summary>
        /// Returns task of FeedMatchResult from Uri
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public abstract Task<FeedMatchResult> FeedMatch(Uri adress);
        /// <summary>
        /// Gets all versions from single package name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract Task<ReadPackagesResult> GetAllVersions(string name);
        /// <summary>
        /// Gets all dependencies from a package name and its version
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public abstract Task<ReadDependenciesResult> GetDependencies(string name, string version);
        /// <summary>
        /// Gets latest versions of a package from its name and version
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public abstract Task<ReadPackagesResult> GetNewestVersions(string name, string version);

        /// <summary>
        /// Gets JSON data from an Uri
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<JsonResult> GetJson( Uri url )
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new JsonResult(null, response.StatusCode, null, null);
                }
                if (response.Content.Headers.ContentType.MediaType == "text/html")
                {
                    return new JsonResult(null, HttpStatusCode.UnsupportedMediaType, null, null);
                }
                var cs = response.Content.Headers.ContentType.CharSet;
                if (cs != null && cs.Length > 2 && cs[0] == '\"' && cs[cs.Length - 1] == '\"')
                {
                    cs = cs.Substring(1, cs.Length - 2);
                    response.Content.Headers.ContentType.CharSet = cs;
                }
                string resp = await response.Content.ReadAsStringAsync();

                try
                {
                    return new JsonResult(null, HttpStatusCode.OK, null, JObject.Parse(resp));
                }
                catch (JsonReaderException ex)
                {
                    return new JsonResult(null, HttpStatusCode.OK, ex, null);
                }
            }
            catch ( Exception ex )
            {
                return new JsonResult(ex, null, null, null);
            }
        }
    }
}

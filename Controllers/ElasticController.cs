using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elasticsearch.Net;
using Nest;

//https://github.com/miroslavpopovic/miniblog-elasticsearch/blob/master/src/Controllers/SearchController.cs
// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest-getting-started.html

namespace legalserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        public ElasticController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet("Find")]
        public async Task<IEnumerable<string>> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Object>(
                s => s.Query(q => q.QueryString(d => d.Query(query)))
            );
        
                    

            //ViewData["Title"] = _settings.Value.Name + " - Search Results";
            //ViewData["Description"] = _settings.Value.Description;

            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException or response.ServerError properties
                //return View("Results", new Post[] { });
            }

            if (page > 1)
            {
                //ViewData["prev"] = GetSearchUrl(query, page - 1, pageSize);
            }

            if (response.IsValid && response.Total > page * pageSize)
            {
                //ViewData["next"] = GetSearchUrl(query, page + 1, pageSize);
            }

            return new string[] { "value1", "value2" };
        }
        // [HttpGet("MoreLikeThis")]
        // public ActionResult<IEnumerable<string>> MoreLikeThis( string value)
        // {
        //     return new string[] { "value1", "value2" };
        // }

        

    }
}

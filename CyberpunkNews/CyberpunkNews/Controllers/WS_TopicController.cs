using CyberpunkNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CyberpunkNews.Controllers
{
    public class WS_TopicController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public IList<topic> GetTopicList()
        {
            return db.topics.ToList();
        }
        
        [HttpPost]
        async public Task<HttpResponseMessage> Upvote(int id)
        {
            var item = db.topics.FirstOrDefault(t => t.id == id);
            if (item != null)
            {
                item.karma += 1;
                await db.SaveChangesAsync();
            }
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}

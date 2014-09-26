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
        
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Upvote(int id)
        {
            var item = db.topics.First(t => t.id == id);
            item.karma += 1;
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        public HttpResponseMessage Submit(topic item)
        {
            var modelStateErrors = ModelState.Values.ToList();

            List<string> errors = new List<string>();

            foreach (var s in modelStateErrors)
                foreach (var e in s.Errors)
                    if (e.ErrorMessage != null && e.ErrorMessage.Trim() != "")
                        errors.Add(e.ErrorMessage);

            if (errors.Count == 0)
            {
                item.submit_date = DateTimeOffset.UtcNow;

                db.topics.Add(item);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            else
            {
                return Request.CreateResponse<List<string>>(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}

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
            var email = Request.GetOwinContext().Authentication.User.Identity.Name;
            var existingVote = db.votes.FirstOrDefault(v => v.email == email && v.topic.id == id);
            if (existingVote == null)
            {

                var topic = db.topics.First(t => t.id == id);
                topic.karma += 1;

                var vote = db.votes.Create();
                vote.topic = topic;
                vote.email = email;
                db.votes.Add(vote);
                db.SaveChanges();
            }
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
                item.submitDate = DateTimeOffset.UtcNow;
                item.submitter = Request.GetOwinContext().Authentication.User.Identity.Name;

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

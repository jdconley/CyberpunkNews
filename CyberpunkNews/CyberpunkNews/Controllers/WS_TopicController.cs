using CyberpunkNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}

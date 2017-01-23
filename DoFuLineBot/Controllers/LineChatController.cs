using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoFuLineBot.Controllers
{
    public class LineChatController : ApiController
    {

        public class LineBot
        {
            public string id { get; set; }
            public string msg { get; set; }

            public override string ToString()
            {
                return String.Format("傳送成功:id: {0}, msg: {1}", id, msg);
            }

        }


        [HttpGet]
        public string PushMsg([FromUri]LineBot linebot)
        {
            string ChannelAccessToken = "OceNJZ8NpknVukpWa4qrd73C9Z2gmpPdsFUeTB19gC3I9XSep6tMoHMQONO5dSOxdYJJEDI3rn4YCr2mVuptu8PuYMHkwKOVaew7Q7LMQxP48znSkVscpGwUxyctj7G1nL8TH53kewNOt7rJRqZ1ygdB04t89/1O/w1cDnyilFU=";
            try
            {
                
                isRock.LineBot.Utility.PushMessage(linebot.id, linebot.msg, ChannelAccessToken);
                return linebot.ToString();
            }
            catch (Exception)
            {

                return "失敗";
            }

        }
        [HttpPost]
        public IHttpActionResult POST()
        {
            string ChannelAccessToken = "OceNJZ8NpknVukpWa4qrd73C9Z2gmpPdsFUeTB19gC3I9XSep6tMoHMQONO5dSOxdYJJEDI3rn4YCr2mVuptu8PuYMHkwKOVaew7Q7LMQxP48znSkVscpGwUxyctj7G1nL8TH53kewNOt7rJRqZ1ygdB04t89/1O/w1cDnyilFU=";

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;

                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string strReceive = ReceivedMessage.events[0].message.text;
                //回覆用戶
                switch (strReceive)
                {
                    case "/ShowUserId":

                        var UserID = ReceivedMessage.events[0].source.userId;
                        isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, "UserID:" + UserID, ChannelAccessToken);
                        break;
                    default:
                        isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, "你說了:"+strReceive, ChannelAccessToken);
                        break;
                }

                //回覆API OK
                return Ok();
               
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}

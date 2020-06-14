using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostSystem.Website
{
    public class WebsiteHelper
    {
        public static readonly string WEB_API_URI = "http://localhost:56831";
        public static readonly string JSON_MEDIA_TYPE = "application/json";
        public static readonly string AUTHORIZATION_HEADER_NAME = "Authorization";
        public static readonly Uri tokenUri = new Uri($"{WEB_API_URI}/api/login");
        public static readonly Uri citiesUri = new Uri($"{WEB_API_URI}/api/cities");
        public static readonly Uri mailsUri = new Uri($"{WEB_API_URI}/api/mails");
        public static readonly Uri postOfficesUri = new Uri($"{WEB_API_URI}/api/postOffices");
        public static readonly Uri deliveriesUri = new Uri($"{WEB_API_URI}/api/deliveries");
    }
}

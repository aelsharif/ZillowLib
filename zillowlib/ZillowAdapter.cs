using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using zillowlib.Schema;
using System.Linq;
using System.Configuration;


namespace zillowlib
{
    public class ZillowAdapter : IPropertyEngine
    {
        private static HttpClient Client = new HttpClient();

        private string _zwId = ZillowConstants.zwid;
        private string _propertyId = string.Empty;


        public decimal GetZestimate(uint propertyId)
        {
			var query = new Dictionary<string, string>()
				{
                    {"zws-id", _zwId},
					{"zpid", propertyId.ToString()}
				};

            var result = CallZillowUrl<zestimateResultType>(ZillowConstants.GetPricingUrl, query);

			if (result.message.code != "0")
				throw new Exception(result.message.text);

            return decimal.Parse(result.response.zestimate.amount.Value);

			
        }

        public Property SearchProperty(string address, string cityStateZip)
        {
            Contract.Ensures(Contract.Result<Property>() != null);

            var query = new Dictionary<string, string> (){
                { "zws-id" , _zwId }, 
                { "address", address }, 
                { "citystatezip", cityStateZip }
            };

            var result = CallZillowUrl<searchresults>(ZillowConstants.SearchPropertyUrl, query);

            if(result.message.code != "0")
				throw new Exception(result.message.text);

             return result.response.results.FirstOrDefault();

        }

        private T CallZillowUrl<T>(string url, Dictionary<string,string> parameters){
            try
            {
                var uriBuilder = new UriBuilder(url);

                var query = HttpUtility.ParseQueryString(string.Empty);

                foreach(var parameter in parameters.Keys){
                    query[parameter] = parameters[parameter];
                }

                uriBuilder.Query = query.ToString();

                var result = Client.GetStringAsync(uriBuilder.Uri).Result;

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(new StringReader(result));
            }catch(Exception ex){
                throw new Exception($"Exception occurred: {ex.Message}");
            }
        }
    }
}

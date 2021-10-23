﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus
{
    public static class FacePlusPlus
    {
        public static async Task<dynamic> GetFaceSets(HttpClient Client, FacePlusPlusConfiguration Configuration)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/faceset/getfacesets"))
                {
                    RequestMessage.Headers.Add("api_key", Configuration.ApiKey);
                    RequestMessage.Headers.Add("api_secret", Configuration.ApiSecret);

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

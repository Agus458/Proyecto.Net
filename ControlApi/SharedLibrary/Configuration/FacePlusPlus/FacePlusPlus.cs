using Microsoft.AspNetCore.Http;
using System;
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
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");

                    RequestMessage.Content = Content;

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<dynamic> CreateFaceSet(HttpClient Client, FacePlusPlusConfiguration Configuration)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/faceset/create"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StringContent(Configuration.OuterId), "\"outer_id\"");
                    Content.Add(new StringContent(Configuration.DisplayName), "\"display_name\"");

                    RequestMessage.Content = Content;

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<dynamic> DeleteFaceSet(HttpClient Client, FacePlusPlusConfiguration Configuration)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/faceset/delete"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StringContent(Configuration.OuterId), "\"outer_id\"");

                    RequestMessage.Content = Content;

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<dynamic> Detect(HttpClient Client, FacePlusPlusConfiguration Configuration, System.IO.Stream File, string FileName)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/detect"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StreamContent(File), "\"image_file\"", FileName);

                    RequestMessage.Content = Content;

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UploadImage(IFormFile ImageFile)
        {

        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration.FacePlusPlus.Returns;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FacePlusPlus
{
    public class FacePlusPlus
    {
        private readonly HttpClient Client;
        private readonly FacePlusPlusConfiguration Configuration;

        public FacePlusPlus(IOptions<FacePlusPlusConfiguration> ConfigurationOptions, IHttpClientFactory clientFactory)
        {
            this.Configuration = ConfigurationOptions.Value;
            this.Client = clientFactory.CreateClient();
        }

        public async Task<GetDetailResponse> GetDetail()
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/faceset/getdetail"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StringContent(Configuration.OuterId), "\"outer_id\"");

                    RequestMessage.Content = Content;

                    var Result = await Client.SendAsync(RequestMessage);

                    return await Result.Content.ReadAsAsync<GetDetailResponse>(new[] { new JsonMediaTypeFormatter() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<dynamic> CreateFaceSet(HttpClient Client, FacePlusPlusConfiguration Configuration)
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

        public async Task<dynamic> DeleteFaceSet()
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

        public async Task<SetUserIDResponse> SetUserID(string FaceToken, string UserId)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/face/setuserid"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StringContent(FaceToken), "\"face_token\"");
                    Content.Add(new StringContent(UserId), "\"user_id\"");

                    RequestMessage.Content = Content;

                    var Response = await Client.SendAsync(RequestMessage);

                    return await Response.Content.ReadAsAsync<SetUserIDResponse>(new[] { new JsonMediaTypeFormatter() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AddFaceResponse> AddFace(string FaceToken)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/faceset/addface"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StringContent(FaceToken), "\"face_tokens\"");
                    Content.Add(new StringContent(Configuration.OuterId), "\"outer_id\"");

                    RequestMessage.Content = Content;

                    var Response = await Client.SendAsync(RequestMessage);
                    var Result = await Response.Content.ReadAsAsync<AddFaceResponse>(new[] { new JsonMediaTypeFormatter() });

                    if(Result.error_message != null && Result.error_message == ApiConstants.InvalidOuterId)
                    {
                        await CreateFaceSet(Client, Configuration);
                        return await AddFace(FaceToken);
                    }

                    return Result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DetectResponse> Detect(System.IO.Stream File, string FileName)
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

                    var Response = await Client.SendAsync(RequestMessage);

                    return await Response.Content.ReadAsAsync<DetectResponse>(new[] { new JsonMediaTypeFormatter() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SearchResponse> Search(System.IO.Stream File, string FileName)
        {
            try
            {
                using (var RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-us.faceplusplus.com/facepp/v3/search"))
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(Configuration.ApiKey), "\"api_key\"");
                    Content.Add(new StringContent(Configuration.ApiSecret), "\"api_secret\"");
                    Content.Add(new StreamContent(File), "\"image_file\"", FileName);
                    Content.Add(new StringContent(Configuration.OuterId), "\"outer_id\"");

                    RequestMessage.Content = Content;

                    var Response = await Client.SendAsync(RequestMessage);

                    return await Response.Content.ReadAsAsync<SearchResponse>(new[] { new JsonMediaTypeFormatter() });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SearchResponse> Identify(IFormFile ImageFile)
        {
            try
            {
                var SearchResponse = await Search(ImageFile.OpenReadStream(), ImageFile.FileName);
                if (SearchResponse.error_message != null) throw new ApiError(SearchResponse.error_message, (int)HttpStatusCode.BadRequest);

                return SearchResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UploadImage(IFormFile ImageFile, string UserId)
        {
            try
            {
                var DetectResponse = await Detect(ImageFile.OpenReadStream(), UserId + Path.GetExtension(ImageFile.FileName));
                if (DetectResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);

                var FaceToken = DetectResponse.faces[0]?.face_token;
                if (FaceToken == null) throw new ApiError("Face not found", (int)HttpStatusCode.BadRequest);

                var SetUserIDResponse = await SetUserID(FaceToken, UserId);
                if (SetUserIDResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);

                var AddFaceResponse = await AddFace(FaceToken);
                if (AddFaceResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
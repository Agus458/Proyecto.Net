using Microsoft.AspNetCore.Http;
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
    public static class FacePlusPlus
    {
        public static async Task<GetDetailResponse> GetDetail(HttpClient Client, FacePlusPlusConfiguration Configuration)
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

        public static async Task<SetUserIDResponse> SetUserID(HttpClient Client, FacePlusPlusConfiguration Configuration, string FaceToken, string UserId)
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

        public static async Task<AddFaceResponse> AddFace(HttpClient Client, FacePlusPlusConfiguration Configuration, string FaceToken)
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
                        return await AddFace(Client, Configuration, FaceToken);
                    }

                    return Result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<DetectResponse> Detect(HttpClient Client, FacePlusPlusConfiguration Configuration, System.IO.Stream File, string FileName)
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

        public static async Task UploadImage(HttpClient Client, FacePlusPlusConfiguration Configuration, IFormFile ImageFile, string UserId)
        {
            try
            {
                var DetectResponse = await Detect(Client, Configuration, ImageFile.OpenReadStream(), UserId + Path.GetExtension(ImageFile.FileName));
                if (DetectResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);

                var FaceToken = DetectResponse.faces[0]?.face_token;
                if (FaceToken == null) throw new ApiError("Face not found", (int)HttpStatusCode.BadRequest);

                var SetUserIDResponse = await SetUserID(Client, Configuration, FaceToken, UserId);
                if (SetUserIDResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);

                var AddFaceResponse = await AddFace(Client, Configuration, FaceToken);
                if (AddFaceResponse.error_message != null) throw new ApiError(DetectResponse.error_message, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
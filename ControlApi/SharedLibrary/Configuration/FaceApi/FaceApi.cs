using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Options;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.FaceApi
{
    public class FaceApi
    {
        private readonly IFaceClient FaceClient;
        private readonly FaceApiConfiguration Configuration;
        private readonly string recognitionModel;
        private readonly string personGroupId;

        public FaceApi(IOptions<FaceApiConfiguration> ConfigurationOptions)
        {
            Configuration = ConfigurationOptions.Value;
            FaceClient = new FaceClient(new ApiKeyServiceClientCredentials(Configuration.SubscriptionKey)) { Endpoint = Configuration.Endpoint };
            recognitionModel = RecognitionModel.Recognition01;
            personGroupId = "persons";
        }

        public async Task<IList<DetectedFace>> Detect(IFormFile formFile)
        {
            try
            {
                return await FaceClient.Face.DetectWithStreamAsync(formFile.OpenReadStream());
            }
            catch (Exception Exception)
            {
                throw new ApiError(Exception.Message);
            }
        }

        public async Task CreateGroup()
        {
            try
            {
                await FaceClient.PersonGroup.CreateAsync(personGroupId, personGroupId, recognitionModel);
            }
            catch (Exception) { }
        }

        public async Task Train()
        {
            try
            {
                await FaceClient.PersonGroup.TrainAsync(personGroupId);

                while (true)
                {
                    await Task.Delay(1000);
                    var trainingStatus = await FaceClient.PersonGroup.GetTrainingStatusAsync("persons");
                    if (trainingStatus.Status == TrainingStatusType.Succeeded) { break; }
                }
            }
            catch (Exception Exception)
            {
                throw new ApiError(Exception.Message);
            }
        }

        public async Task Add(IFormFile formFile, string personId)
        {
            try
            {
                Person Person = await FaceClient.PersonGroupPerson.CreateAsync(personGroupId, personId);

                await FaceClient.PersonGroupPerson.AddFaceFromStreamAsync(personGroupId, Person.PersonId, formFile.OpenReadStream());
            }
            catch (Exception Exception)
            {
                throw new ApiError(Exception.Message);
            }
        }

        public async Task<IList<IdentifyResult>> Identify(IFormFile formFile)
        {
            try
            {
                List<Guid> sourceFaceIds = new();
                var detectedFaces = await this.Detect(formFile);
                foreach (var detectedFace in detectedFaces) { sourceFaceIds.Add(detectedFace.FaceId.Value); }

                return await FaceClient.Face.IdentifyAsync(sourceFaceIds, personGroupId);
            }
            catch (Exception Exception)
            {
                throw new ApiError(Exception.Message);
            }
        }

        public async Task AddPerson(IFormFile formFile, string personId)
        {
            try
            {
                await CreateGroup();
                await Add(formFile, personId);
                await Train();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

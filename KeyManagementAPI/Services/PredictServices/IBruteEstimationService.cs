using KeyManagementAPI.Entities;

namespace KeyManagementAPI.Services.PredictServices
{
    public interface IBruteEstimationService
    {
        // trains and saves models from CSV format
        void TrainModels(string datasetPath);

        // runs the input model and returns the predictions
        BruteForceEstimateOutput Predict(BruteForceEstimateInput input);
    }
}

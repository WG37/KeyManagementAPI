using KeyManagementAPI.Entities;
using Microsoft.ML;
using System.Drawing.Text;

namespace KeyManagementAPI.Services.PredictServices
{
    public class BruteEstimationService : IBruteEstimationService
    {
        private readonly MLContext _mlContext;
        
        private ITransformer _crackTimeModel, _energyModel;
        
        const string CrackPath = "cracktime_model.zip";
        const string EnergyPath = "energy_model.zip";

        public BruteEstimationService(MLContext mlContext) => _mlContext = mlContext;


        public void TrainModels(string datasetPath)
        {
            // loads the CSV data into IDataView of ModelInput type
            var data = _mlContext.Data.LoadFromTextFile<ModelInput>(datasetPath, hasHeader: true, separatorChar: ',');

            // convert categorical algorithm => numerical key // concatenates all inputs into feature vector "Features"
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Algorithm")
                .Append(_mlContext.Transforms.Concatenate("Features", "Algorithm", "KeyLength", "Entropy", "HardwareScore"));

            // TRAIN MODELS//

            // train cracktime model
            _crackTimeModel = pipeline.Append(_mlContext.Regression.Trainers
                .FastTree(labelColumnName: "EstimatedCrackTime")).Fit(data);

            // train energy model
            _energyModel = pipeline.Append(_mlContext.Regression.Trainers
                .FastTree(labelColumnName: "EstimatedEnergy")).Fit(data);

            // save models
            _mlContext.Model.Save(_crackTimeModel, data.Schema, CrackPath);
            _mlContext.Model.Save(_energyModel, data.Schema, EnergyPath);
        }


        public BruteForceEstimateOutput Predict(BruteForceEstimateInput input)
        {
            // load models (if not in memory)
            if (_crackTimeModel == null)
            {
                _crackTimeModel = _mlContext.Model.Load(CrackPath, out _);
                _energyModel = _mlContext.Model.Load(EnergyPath, out _);
            }

            // create the prediction engines
            var crackEngine = _mlContext.Model.CreatePredictionEngine<BruteForceEstimateInput, CrackPred>(_crackTimeModel);
            var energyEngine = _mlContext.Model.CreatePredictionEngine<BruteForceEstimateInput, EnergyPred>(_energyModel);

            // run predictions and return the output
            return new BruteForceEstimateOutput
            {
                EstimatedCrackTime = crackEngine.Predict(input).EstimatedCrackTime,
                EstimatedEnergy = energyEngine.Predict(input).EstimatedEnergy
            };
        }

        // MODEL TRAINING ONLY
        private class ModelInput : BruteForceEstimateInput
        {
            public float EstimatedCrackTime { get; set; }
            public float EstimatedEnergy { get; set; }
        }

        private class CrackPred { public float EstimatedCrackTime { get; set; } };
        private class EnergyPred { public float EstimatedEnergy { get; set; } };
    }
}

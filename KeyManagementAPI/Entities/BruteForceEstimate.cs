namespace KeyManagementAPI.Entities
{
    public class BruteForceEstimateInput
    {
        public string Algorithm { get; set; }
        public float KeyLength { get; set; }
        public float Entropy { get; set; }
        public float Hardware { get; set; }
    }

    public class BruteForceEstimateOutput
    {
        public float EstimatedCrackTime { get; set; }
        public float EstimatedEnergy { get; set; }
    }
}

namespace KeyManagementAPI.DTOs.Asymmetric
{
    public class RsaEncryptRequest
    {
        public string PlainText { get; set; }
    }
    public class RsaEncryptResponse
    {
        public string CipherText { get; set; }
    }

    public class RsaDecryptRequest
    {
        public string CipherText { get; set; }
    }

    public class RsaDecryptResponse
    {
        public string PlainText { get; set; }
    }
}

namespace KeyManagementAPI.DTOs
{

    public class EncryptRequest 
    {
        public string PlainText { get; set; }
    }

    public class EncryptResponse
    {
        public string CipherText { get; set; }
        public string Iv { get; set; }
    }

    public class DecryptRequest
    {
        public string CipherText { get; set; }
        public string Iv { get; set; }
    }

    public class DecryptResponse
    {
        public string PlainText { get; set; }
    }
}


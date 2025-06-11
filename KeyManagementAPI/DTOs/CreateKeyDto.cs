namespace KeyManagementAPI.DTOs
{
    public class CreateKeyDto
    {
        public string Name { get; set; }
        public int KeySize { get; set; }
        public string Algorithm { get; set; }
    }
}

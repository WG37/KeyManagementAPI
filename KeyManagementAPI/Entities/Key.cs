namespace KeyManagementAPI.Entities
{
    public enum KeyStatus { Active, Deleted, Disabled }

    public class Key
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Algorithm { get; set; }
        public byte[]? KeyBytes { get; set; }
        public int KeySize { get; set; }
        public byte[]? PubKey { get; set; }
        public byte[]? PrivKey { get; set; }
        public KeyStatus Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

namespace KeyManagementAPI.Entities
{
    public enum KeyStatus { Active, Deleted, Disabled }

    public class Key
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Keybytes { get; set; }
        public int KeySize { get; set; }    
        public KeyStatus Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

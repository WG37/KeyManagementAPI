using KeyManagementAPI.Entities;

namespace KeyManagementAPI.DTOs
{
   
    public class KeyDto
    {
       public Guid Id { get; set; }
       public string Name { get; set; }
       public int KeySize { get; set; }
       public byte[] KeyBytes { get; set; }
       public KeyStatus Status { get; set; }
       public int Version { get; set; }
       public DateTime CreatedOn { get; set; }
    }
}

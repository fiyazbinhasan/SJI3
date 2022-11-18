using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Fido2NetLib.Objects;

namespace Fido2Identity;

public class FidoStoredCredential
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
    
    public virtual string UserName { get; set; }

    public virtual byte[] UserId { get; set; }
    
    public virtual byte[] PublicKey { get; set; }
    
    public virtual byte[] UserHandle { get; set; }

    public virtual uint SignatureCounter { get; set; }

    public virtual string CredType { get; set; }
    
    public virtual DateTime RegDate { get; set; }
    
    public virtual Guid AaGuid { get; set; }

    [NotMapped]
    public PublicKeyCredentialDescriptor Descriptor
    {
        get => string.IsNullOrWhiteSpace(DescriptorJson) ? null : JsonSerializer.Deserialize<PublicKeyCredentialDescriptor>(DescriptorJson);
        set => DescriptorJson = JsonSerializer.Serialize(value);
    }

    public virtual string DescriptorJson { get; set; }
}
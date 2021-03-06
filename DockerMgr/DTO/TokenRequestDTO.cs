using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DockerMgr.DTO
{
    public class TokenRequestDTO
    {
        [Required] 
        [JsonProperty("email")]
        public string Email { get; set; }
        
        
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
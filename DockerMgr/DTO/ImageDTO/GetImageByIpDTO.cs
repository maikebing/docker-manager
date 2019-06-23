using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DockerMgr.DTO.ImageDTO
{
    public class GetImageByIpDTO
    {
        [Required] 
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}
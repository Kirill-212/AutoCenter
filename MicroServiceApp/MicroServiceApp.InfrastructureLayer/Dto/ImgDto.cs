using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class ImgDto
    {
        [Required]
        public string Url { get; set; }
    }
}

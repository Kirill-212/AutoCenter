using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroServiceApp.InfrastructureLayer.Dto
{
    public class NewWrapperDto<T>
    {
        [Required]
        public T New { get; set; }

        [Required]
        public List<ImgDto> Imgs { get; set; }
    }
}

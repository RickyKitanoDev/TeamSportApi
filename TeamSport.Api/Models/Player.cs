using System;
using System.ComponentModel.DataAnnotations;

namespace TeamSportApi.Models
{

    public class Player : Person
    {
      
        [Required(ErrorMessage = "Posição deve ser preenchida")]
        public string Position { get; set; }
    }
}

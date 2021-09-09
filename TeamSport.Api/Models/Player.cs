using System;
using System.ComponentModel.DataAnnotations;

namespace TeamSportApi.Models
{

    public class Player : Person
    {
      
        [Required(ErrorMessage = "Posição deve ser preenchido")]
        public string Position { get; set; }
    }
}

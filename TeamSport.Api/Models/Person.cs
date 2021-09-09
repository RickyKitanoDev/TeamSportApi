using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamSportApi.Models
{
    public abstract class Person
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage="Nome deve ser preenchido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Data de nascimento deve ser preenchida")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Idade deve ser preenchida")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Nacionalidade deve ser preenchida")]
        public string Nationality { get; set; }
    }
}

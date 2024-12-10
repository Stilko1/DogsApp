using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsProject.Infrastructure.Data.Entities
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(30)]
        public string Name { get; set; } = null!;
        [Range(0, 30)]
        public int Age { get; set; }

        [Required]
        public int BreedId { get; set; }
        [ForeignKey(nameof(BreedId))]
        public virtual Breed Breed { get; set; } = null!;
        public string? Picture { get; set; }
    }
}

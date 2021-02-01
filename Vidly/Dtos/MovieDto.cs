using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        [Required]
        public byte GenreTypeId { get; set; }

        public GenreTypeDto GenreType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleseDate { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
    }
}
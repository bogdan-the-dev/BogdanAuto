using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bogdan_Auto.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}


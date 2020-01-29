using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Customer
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "é obrigatório 2")] //
        [StringLength(10, ErrorMessage = "Your sthing is too long")]
        [DisplayName("Digite seu nome:")]
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}
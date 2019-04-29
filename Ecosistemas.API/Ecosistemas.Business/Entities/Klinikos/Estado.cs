using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Estado
    {

        [Key]
        public Guid EstadoId { get; set; }

        [Required(ErrorMessage = "O nome do estado é obrigatório")]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O uf é obrigatório")]
        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string Uf { get; set; }

        public bool Ativo { get; set; } = true;

        //public List<Cidade> Cidades { get; set; }
    }
}
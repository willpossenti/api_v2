using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class ReacaoAlergia
    {

        [Key]
        public Guid ReacaoAlergiaId { get; set; }

        [Required(ErrorMessage = "A descrição da reacão alérgica é obrigatória")]
        [StringLength(40, ErrorMessage = "{0} Precisa ter no máximo 40")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class TipoCertidao
    {
        [Key]
        public Guid TipoCertidaoId { get; set; }

        [Required(ErrorMessage = "O código do tipo da certidão é obrigatório")]
        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]

        public string CodigoTipoCertidao { get; set; }

        [Required(ErrorMessage = "A descrição do tipo da certidão é obrigatória")]
        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; } 
    }
}
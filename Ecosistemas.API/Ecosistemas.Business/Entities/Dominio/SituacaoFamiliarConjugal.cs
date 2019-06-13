﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class SituacaoFamiliarConjugal
    {
        [Key]
        public Guid SituacaoFamiliarConjugalId { get; set; }

        [Required(ErrorMessage = "O código da situação familiar/conjulgar é obrigatório")]
        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]

        public string CodigoSituacaoFamiliarConjugal { get; set; }

        [Required(ErrorMessage = "A descrição da situação é obrigatória")]
        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<PessoaPaciente> PessoaPacientes { get; set; }
    }
}
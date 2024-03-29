﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Especialidade
    {

        [Key]
        public Guid EspecialidadeId { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        //public virtual List<Pessoa> Pessoas { get; set; }


    }
}

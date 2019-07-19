﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaAtendimento
    {

        [Key]
        public Guid FilaAtendimentoId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataEntradaFilaAtendimento { get; set; }

        public virtual ClassificacaoRisco ClassificacaoRisco { get; set; }
        public virtual Acolhimento Acolhimento { get; set; }

        public bool Preferencial { get; set; }

        public bool Idoso80 { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
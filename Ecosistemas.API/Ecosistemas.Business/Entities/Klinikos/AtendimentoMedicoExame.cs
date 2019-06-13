﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AtendimentoMedicoExame
    {

        [Key]
        public Guid AtendimentoMedicoExameId { get; set; }

        public virtual AtendimentoMedico AtendimentoMedico { get; set; }

<<<<<<< HEAD
        public bool ExameLaboratorial { get; set; }

        public bool ExameImagem { get; set; }

        public Guid GrupoExameId { get; set; }
=======
        public virtual GrupoExame GrupoExame { get; set; }
>>>>>>> sprint_yl_25052019

        public Guid ExameId { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string ObservacaoExame { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataExame { get; set; }

        public virtual PessoaProfissional Profissional { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
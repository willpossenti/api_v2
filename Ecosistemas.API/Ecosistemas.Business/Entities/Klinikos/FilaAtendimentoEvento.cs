using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaAtendimentoEvento
    {

        [Key]
        public Guid FilaAtendimentoEventoId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataFilaAtendimentoEvento { get; set; }

        public virtual FilaAtendimento FilaAtendimento { get; set; }

        public virtual PessoaProfissional PessoaProfissional { get; set; }


        public Guid EventoId { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
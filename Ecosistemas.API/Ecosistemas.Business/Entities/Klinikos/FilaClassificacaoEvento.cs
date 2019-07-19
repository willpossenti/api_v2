using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaClassificacaoEvento
    {

        [Key]
        public Guid FilaClassificacaoEventoId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataFilaClassificacaoEvento { get; set; }

        public virtual FilaClassificacao FilaClassificacao { get; set; }

        public virtual PessoaProfissional PessoaProfissional { get; set; }


        public Guid EventoId { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
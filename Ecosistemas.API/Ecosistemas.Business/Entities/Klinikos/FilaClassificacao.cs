using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaClassificacao
    {

        [Key]
        public Guid FilaClassificacaoId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataEntradaFilaClassificacao { get; set; }

        public virtual RegistroBoletim RegistroBoletim { get; set; }
        public virtual Acolhimento Acolhimento { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
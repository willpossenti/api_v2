using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaRegistroHistorico
    {

        [Key]
        public Guid FilaRegistroHistoricoId { get; set; }

        public virtual FilaRegistro FilaRegistro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataEntradaFilaRegistroHistorico { get; set; }

        public string Profissional { get; set; }

        [StringLength(90, ErrorMessage = "{0} Precisa ter no máximo 90")]
        [DataType(DataType.Text)]
        public string FilaStatus { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
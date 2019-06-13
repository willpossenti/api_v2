using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AtendimentoMedicoExameHistorico
    {

        [Key]
        public Guid AtendimentoMedicoExameHistoricoId { get; set; }

        public virtual AtendimentoMedicoExame AtendimentoMedicoExame { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string GrupoExame { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Exame { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string ObservacaoExame { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string Profissional { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataExame { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
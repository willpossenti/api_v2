using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ClassificacaoRiscoAlergiaHistorico
    {

        [Key]
        public Guid ClassificacaoRiscoAlergiaHistoricoId { get; set; }

        public virtual ClassificacaoRiscoAlergia ClassificacaoRiscoAlergia { get; set; }

        [StringLength(60, ErrorMessage = "{0} Precisa ter no máximo 60")]
        [DataType(DataType.Text)]
        public string Alergia { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string TipoAlergia { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string LocalizacaoAlergia { get; set; }

        [StringLength(40, ErrorMessage = "{0} Precisa ter no máximo 40")]
        [DataType(DataType.Text)]
        public string ReacaoAlergia { get; set; }

        [StringLength(40, ErrorMessage = "{0} Precisa ter no máximo 40")]
        [DataType(DataType.Text)]
        public string SeveridadeAlergia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataSintomas { get; set; }

        public bool AlergiaSituacao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
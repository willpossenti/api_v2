using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ClassificacaoRiscoAlergia
    {

        [Key]
        public Guid ClassificacaoRiscoAlergiaId { get; set; }

        public virtual ClassificacaoRisco ClassificacaoRisco { get; set; }

        [Required(ErrorMessage = "O nome da alergia é obrigatório")]
        public virtual Alergia Alergia { get; set; }

        [Required(ErrorMessage = "O nome do tipo de alegia é obrigatório")]
        public virtual TipoAlergia TipoAlergia { get; set; }

        public virtual LocalizacaoAlergia LocalizacaoAlergia { get; set; }

        public virtual ReacaoAlergia ReacaoAlergia { get; set; }

        public virtual SeveridadeAlergia SeveridadeAlergia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataSintomas { get; set; }

        public bool AlergiaSituacao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
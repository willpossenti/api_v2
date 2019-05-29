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
        public Guid AlergiaId { get; set; }

        [Required(ErrorMessage = "O nome do tipo de alegia é obrigatório")]
        public Guid TipoAlergiaId { get; set; }

        public Guid LocalizacaoAlergiaId { get; set; }

        public Guid ReacaoAlergiaId { get; set; }

        public Guid SeveridadeAlergiaId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataSintomas { get; set; }

        public bool AlergiaSituacao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
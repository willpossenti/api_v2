using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AcolhimentoHistorico
    {

        [Key]
        public Guid AcolhimentoHistoricoId { get; set; }

        public virtual Acolhimento Acolhimento { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public string CPF { get; set; }

        [StringLength(15, ErrorMessage = "{0} Precisa ter no máximo 15")]
        [DataType(DataType.Text)]
        public string CNS { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeSocial { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Especialidade { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string Prioridade { get; set; }

        public bool Risco { get; set; }

        public double Temperatura { get; set; }

        public int PressaoArterialSistolica { get; set; }

        public int PressaoArterialDiastolica { get; set; }

        public int Pulso { get; set; }

        public int FrequenciaRespiratoria { get; set; }

        public int Saturacao { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string PessoaAlteracao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataAlteracao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
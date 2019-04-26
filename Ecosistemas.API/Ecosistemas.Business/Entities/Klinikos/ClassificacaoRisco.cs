using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ClassificacaoRisco
    {

        public ClassificacaoRisco() { this.ClassificacoesRiscoAlergia = new List<ClassificacaoRiscoAlergia>(); }

        [Key]
        public Guid ClassificacaoRiscoId { get; set; }

        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string DescricaoQueixa { get; set; }

        public virtual CausaExterna CausaExterna { get; set; }

        public virtual NivelConsciencia NivelConsciencia { get; set; }

        public virtual EscalaDor EscalaDor { get; set; }

        public bool Sutura { get; set; }

        public double Peso { get; set; }

        public int Altura { get; set; }

        public double IMC { get; set; }

        public double Temperatura { get; set; }

        public int PressaoArterial { get; set; }

        public int Pulso { get; set; }

        public int FrequenciaRespiratoria { get; set; }

        public int Saturacao { get; set; }

        public virtual DoencaPreExistente DoencaPreExistente { get; set; }

        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 200")]
        [DataType(DataType.Text)]
        public string Avaliacao { get; set; }

        [Required(ErrorMessage = "O tipo de chegada obrigatório")]
        public virtual TipoChegada TipoChegada { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória")]
        public virtual Especialidade Especialidade { get; set; }

        [Required(ErrorMessage = "O risco é obrigatório")]
        public virtual Risco Risco { get; set; }

        public virtual List<ClassificacaoRiscoAlergia> ClassificacoesRiscoAlergia { get; set; }

        public virtual AberturaOcular AberturaOcular { get; set; }

        public virtual RespostaVerbal RespostaVerbal { get; set; }

        public virtual RespostaMotora RespostaMotora { get; set; }

        public virtual Trauma Trauma { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Procedencia { get; set; }

        public virtual TipoOcorrencia TipoOcorrencia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataOcorrencia { get; set; }

        public bool Pab { get; set; }

        public bool Paf { get; set; }

        [StringLength(8, ErrorMessage = "{0} Precisa ter no máximo 8")]
        [DataType(DataType.Text)]
        public string Cep { get; set; }

        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string Logradouro { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Numero { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Complemento { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string Bairro { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Cidade Cidade { get; set; }




        public bool Ativo { get; set; } = true;

    }
}
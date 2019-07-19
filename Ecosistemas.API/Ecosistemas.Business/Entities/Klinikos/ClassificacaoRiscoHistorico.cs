using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class ClassificacaoRiscoHistorico
    {

        [Key]
        public Guid ClassificacaoRiscoHistoricoId { get; set; }

        public virtual ClassificacaoRisco ClassificacaoRisco { get; set; }

        public string Paciente { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataClassificaoRisco { get; set; }

        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string DescricaoQueixa { get; set; }

        [StringLength(90, ErrorMessage = "{0} Precisa ter no máximo 90")]
        [DataType(DataType.Text)]
        public string CausaExterna { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string NivelConsciencia { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string EscalaDor { get; set; }

        public bool Sutura { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Peso { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Altura { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Imc { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Temperatura { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string PressaoArterialDiastolica { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string PressaoArterialSistolica { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Pulso { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string FrequenciaRespiratoria { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Saturacao { get; set; }

        public bool Cardiopata { get; set; }

        public bool Diabete { get; set; }

        public bool Hipertensao { get; set; }

        public bool Outros { get; set; }

        public string ObservacaoOutros { get; set; }

        public bool RenalCronico { get; set; }

        public bool RespiratoriaCronica { get; set; }

        public string ObservacaoRespiratoriaCronica { get; set; }

        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 500")]
        [DataType(DataType.Text)]
        public string Avaliacao { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string TipoChegada { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Especialidade { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Risco { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string AberturaOcular { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string RespostaVerbal { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string RespostaMotora { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Trauma { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Procedencia { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string TipoOcorrencia { get; set; }

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

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Estado { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string Cidade { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string PessoaAlteracao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataAlteracao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
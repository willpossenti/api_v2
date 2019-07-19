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

        [DataType(DataType.DateTime)]
        public DateTime? DataClassificaoRisco { get; set; }
        public virtual PessoaPaciente PessoaPaciente { get; set; }

        public virtual PessoaProfissional PessoaProfissional { get; set; }

        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string DescricaoQueixa { get; set; }

        public Guid CausaExternaId { get; set; }

        public Guid NivelConscienciaId { get; set; }

        public Guid EscalaDorId { get; set; }

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

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string ObservacaoOutros { get; set; }

        public bool RenalCronico { get; set; }

        public bool RespiratoriaCronica { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string ObservacaoRespiratoriaCronica { get; set; }


        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 200")]
        [DataType(DataType.Text)]
        public string Avaliacao { get; set; }

        [Required(ErrorMessage = "O tipo de chegada obrigatório")]
        public Guid TipoChegadaId { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória")]
        public Guid EspecialidadeId { get; set; }

        [Required(ErrorMessage = "O risco é obrigatório")]
        public Guid RiscoId { get; set; }

        public virtual List<ClassificacaoRiscoAlergia> ClassificacoesRiscoAlergia { get; set; }

        public Guid AberturaOcularId { get; set; }

        public Guid RespostaVerbalId { get; set; }

        public Guid RespostaMotoraId { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Status { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Procedencia { get; set; }

        public Guid TipoOcorrenciaId { get; set; }

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

        public Guid EstadoId { get; set; }

        public Guid CidadeId { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
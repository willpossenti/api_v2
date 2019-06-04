using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class AtendimentoMedico
    {

        public AtendimentoMedico() { this.AtendimentoMedicoAlergia = new List<AtendimentoMedicoAlergia>();
            this.AtendimentoMedicoExame = new List<AtendimentoMedicoExame>(); this.AtendimentoMedicoPrescricaoReceita = new List<AtendimentoMedicoPrescricaoReceita>(); }

        [Key]
        public Guid AtendimentoMedicoId { get; set; }

        [StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        [DataType(DataType.Text)]
        public string Anamnese { get; set; }

        public virtual List<AtendimentoMedicoAlergia> AtendimentoMedicoAlergia { get; set; }

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

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string CampoObservacao { get; set; }

        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public string SuspeitaDiagnostico { get; set; }

        public virtual CID CID { get; set; }

        public virtual ConsultaCID ConsultaCID { get; set; }

        public bool CondutaExames { get; set; }

        public bool CondutaPrescricao { get; set; }

        public bool CondutaAtestado { get; set; }

        public virtual List<AtendimentoMedicoExame> AtendimentoMedicoExame { get; set; }

        public virtual ModeloPrescricaoReceitaDetalhe ModeloPrescricaoReceitaDetalhe { get; set; }

        public virtual List<AtendimentoMedicoPrescricaoReceita> AtendimentoMedicoPrescricaoReceita { get; set; }

        public virtual ModeloAtestado ModeloAtestado { get; set; }

        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 500")]
        [DataType(DataType.Text)]
        public string Atestado { get; set; }

        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]
        public string ValidadeAtestado { get; set; }

        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        [DataType(DataType.Text)]
        public string TipoSaida { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataSaida { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
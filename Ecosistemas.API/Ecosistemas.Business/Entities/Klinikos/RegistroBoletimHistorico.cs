using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class RegistroBoletimHistorico
    {

        [Key]
        public Guid RegistroBoletimHistoricoId { get; set; }

        public virtual RegistroBoletim RegistroBoletim { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroBoletim { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataBoletim { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string TipoChegada { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Especialidade { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string NomeInformante { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string EnderecoInformante { get; set; }

        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public string TelefoneInformante { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string GrauParentesco { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string Procedencia { get; set; }

        [StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        [DataType(DataType.Text)]
        public string TipoOcorrencia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataOcorrencia { get; set; }

        [StringLength(3, ErrorMessage = "{0} Precisa ter no máximo 3")]
        [DataType(DataType.Text)]
        public string TipoPerfuracao { get; set; }

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

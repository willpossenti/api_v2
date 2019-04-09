using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class RegistroBoletim
    {

        [Key]
        public Guid RegistroBoletimId { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroBoletim { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataBoletim { get; set; }

        public virtual PessoaPaciente Pessoa { get; set; }

        public virtual TipoChegada TipoChegada { get; set; }

        public virtual Especialidade Especialidade { get; set; }

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

        public virtual TipoOcorrencia TipoOcorrencia { get; set; }

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

        public virtual Estado Estado { get; set; }

        public virtual Cidade Cidade { get; set; }

        public bool Ativo { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class PessoaPaciente: Pessoa
    {

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeSocial { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroProntuario { get; set; }


        [StringLength(500, ErrorMessage = "{0} Precisa ter no máximo 500")]
        [DataType(DataType.Text)]
        public string DescricaoNaoIdentificado { get; set; }

        public bool Recemnascido { get; set; }


        [StringLength(15, ErrorMessage = "{0} Precisa ter no máximo 15")]
        [DataType(DataType.Text)]
        public string Cns { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Nascimento { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string IdadeAparente { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime Obito { get; set; }

        public virtual Raca Raca { get; set; }

        public virtual Etnia Etnia { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomePai { get; set; }


        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeMae { get; set; }

        public virtual Justificativa Justificativa { get; set; }
        public virtual Nacionalidade Nacionalidade { get; set; }
        public virtual Cidade Naturalidade { get; set; }
        public virtual OrgaoEmissor OrgaoEmissor { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Identidade { get; set; }

        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string Uf { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Emissao { get; set; }

        //[StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        //[DataType(DataType.Text)]
        //public string Telefone { get; set; }

        //[StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        //[DataType(DataType.Text)]
        //public string Celular { get; set; }

        //[StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        //[DataType(DataType.Text)]
        //public string Email { get; set; }

        //[StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        //[DataType(DataType.Text)]
        //public string Cep { get; set; }

        //[StringLength(150, ErrorMessage = "{0} Precisa ter no máximo 150")]
        //[DataType(DataType.Text)]
        //public string Logradouro { get; set; }

        //[StringLength(5, ErrorMessage = "{0} Precisa ter no máximo 5")]
        //[DataType(DataType.Text)]
        //public string Numero { get; set; }

        //[StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        //[DataType(DataType.Text)]
        //public string Complemento { get; set; }
        //[StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        //[DataType(DataType.Text)]
        //public string Bairro { get; set; }
        //[StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        //public string Municipio { get; set; }
        //[StringLength(50, ErrorMessage = "{0} Precisa ter no máximo 50")]
        //public string Estado { get; set; }

    }
}

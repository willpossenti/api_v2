using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Pessoa
    {

        public Pessoa() { }


        [Key]
        public Guid PessoaId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeCompleto { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeSocial { get; set; }

        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        [DataType(DataType.Text)]
        public virtual string Sexo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Nascimento { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string IdadeAparente { get; set; }
        
        public Guid RacaId { get; set; }

        public Guid EtniaId { get; set; }

        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomePai { get; set; }


        [StringLength(70, ErrorMessage = "{0} Precisa ter no máximo 70")]
        [DataType(DataType.Text)]
        public string NomeMae { get; set; }

        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public string Cpf { get; set; }

        public Guid JustificativaId { get; set; }
        public Guid NacionalidadeId { get; set; }
        public Guid NaturalidadeId { get; set; }
        public Guid OrgaoEmissorId { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string Identidade { get; set; }

        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string Uf { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Emissao { get; set; }

        [StringLength(15, ErrorMessage = "{0} Precisa ter no máximo 15")]
        [DataType(DataType.Text)]
        public string Cns { get; set; }


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

        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public virtual string Contato1 { get; set; }
        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public virtual string Contato2 { get; set; }
        [StringLength(11, ErrorMessage = "{0} Precisa ter no máximo 11")]
        [DataType(DataType.Text)]
        public virtual string Contato3 { get; set; }
        [StringLength(100, ErrorMessage = "{0} Precisa ter no máximo 100")]
        [DataType(DataType.Text)]
        public virtual string Email { get; set; }


        [StringLength(15, ErrorMessage = "{0} Precisa ter no máximo 15")]
        [DataType(DataType.Text)]
        public string PisPasep { get; set; }

        public Guid OcupacaoId { get; set; }

        public Guid PaisOrigemId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataEntradaPis { get; set; }

        public Guid TipoCertidaoId { get; set; }

        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 30")]
        [DataType(DataType.Text)]
        public string NomeCartorio { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroLivro { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroFolha { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string NumeroTermo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataEmissaoCertidao { get; set; }

        [StringLength(15, ErrorMessage = "{0} Precisa ter no máximo 15")]
        [DataType(DataType.Text)]
        public string NumeroCtps { get; set; }

        [StringLength(10, ErrorMessage = "{0} Precisa ter no máximo 10")]
        [DataType(DataType.Text)]
        public string SerieCtps { get; set; }

        [StringLength(2, ErrorMessage = "{0} Precisa ter no máximo 2")]
        [DataType(DataType.Text)]
        public string UfCtps { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataEmissaoCtps { get; set; }

        [StringLength(12, ErrorMessage = "{0} Precisa ter no máximo 12")]
        [DataType(DataType.Text)]
        public string TituloEleitor { get; set; }

        [StringLength(4, ErrorMessage = "{0} Precisa ter no máximo 4")]
        [DataType(DataType.Text)]
        public string Zona { get; set; }

        [StringLength(4, ErrorMessage = "{0} Precisa ter no máximo 4")]
        [DataType(DataType.Text)]
        public string Secao { get; set; }

        public bool FrequentaEscola { get; set; }

        public Guid EscolaridadeId { get; set; }

        public Guid SituacaoFamiliarConjugalId { get; set; }

        public bool PacienteProfissional { get; set; }

        [StringLength(5, ErrorMessage = "{0} Precisa ter no máximo 5")]
        [DataType(DataType.Text)]
        public string CodigoLogin { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        [DataType(DataType.Text)]
        public string Senha { get; set; }

        public bool Master { get; set; } = false;

        [Column(TypeName = "varchar(MAX)")]
        [DataType(DataType.Text)]
        public string Foto { get; set; }

        public bool Ativo { get; set; } = true;


    }
}

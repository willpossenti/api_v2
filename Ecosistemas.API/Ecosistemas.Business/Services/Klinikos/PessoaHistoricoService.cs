using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Contexto.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;
using Ecosistemas.Business.Contexto.Api;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaHistoricoService : BaseService<PessoaHistorico>, IPessoaHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public PessoaHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;
        }
        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoPaciente(PessoaPaciente pessoaPaciente, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _pessoaPacienteHistorico = new PessoaHistorico
                {
                    Ativo = pessoaPaciente.Ativo,
                    Cep = pessoaPaciente.Cep,
                    Bairro = pessoaPaciente.Bairro,
                    Cidade = pessoaPaciente.CidadeId != Guid.Empty? _contextDominio.Cidades.FindAsync(pessoaPaciente.CidadeId).Result.Nome: null,
                    Contato1 = pessoaPaciente.Contato1,
                    Contato2 = pessoaPaciente.Contato2,
                    Contato3 = pessoaPaciente.Contato3,
                    Email = pessoaPaciente.Email,
                    Cns = pessoaPaciente.Cns,
                    CodigoLogin = pessoaPaciente.CodigoLogin,
                    Complemento = pessoaPaciente.Complemento,
                    Cpf = pessoaPaciente.Cpf,
                    DataEmissaoCertidao = pessoaPaciente.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaPaciente.DataEmissaoCtps,
                    DataEntradaPis = pessoaPaciente.DataEntradaPis,
                    DescricaoNaoIdentificado = pessoaPaciente.DescricaoNaoIdentificado,
                    Emissao = pessoaPaciente.Emissao,
                    Escolaridade = pessoaPaciente.EscolaridadeId != Guid.Empty ? _contextDominio.Escolaridades.FindAsync(pessoaPaciente.EscolaridadeId).Result.Descricao : null,
                    Estado = pessoaPaciente.EstadoId != Guid.Empty ? _contextDominio.Estados.FindAsync(pessoaPaciente.EstadoId).Result.Nome : null,
                    Etnia = pessoaPaciente.EtniaId != Guid.Empty ? _contextDominio.Etnias.FindAsync(pessoaPaciente.EtniaId).Result.Nome : null,
                    FrequentaEscola = pessoaPaciente.FrequentaEscola,
                    IdadeAparente = pessoaPaciente.IdadeAparente,
                    Identidade = pessoaPaciente.Identidade,
                    Justificativa = pessoaPaciente.JustificativaId != Guid.Empty ? _contextDominio.Justificativas.FindAsync(pessoaPaciente.JustificativaId).Result.Descricao : null,
                    Login = pessoaPaciente.Login,
                    Logradouro = pessoaPaciente.Logradouro,
                    Nacionalidade = pessoaPaciente.NacionalidadeId != Guid.Empty ? _contextDominio.Nacionalidades.FindAsync(pessoaPaciente.NacionalidadeId).Result.Descricao : null,
                    Nascimento = pessoaPaciente.Nascimento,
                    Naturalidade = pessoaPaciente.NaturalidadeId != Guid.Empty ? _contextDominio.Estados.FindAsync(pessoaPaciente.NaturalidadeId).Result.Nome : null,
                    NomeCartorio = pessoaPaciente.NomeCartorio,
                    NomeCompleto = pessoaPaciente.NomeCompleto,
                    NomeMae = pessoaPaciente.NomeMae,
                    NomePai = pessoaPaciente.NomePai,
                    NomeSocial = pessoaPaciente.NomeSocial,
                    Numero = pessoaPaciente.Numero,
                    NumeroCtps = pessoaPaciente.NumeroCtps,
                    NumeroFolha = pessoaPaciente.NumeroFolha,
                    NumeroLivro = pessoaPaciente.NumeroLivro,
                    NumeroProntuario = pessoaPaciente.NumeroProntuario,
                    NumeroTermo = pessoaPaciente.NumeroTermo,
                    Ocupacao = pessoaPaciente.OcupacaoId != Guid.Empty ? _contextDominio.Ocupacoes.FindAsync(pessoaPaciente.OcupacaoId).Result.Descricao : null,
                    OrgaoEmissor = pessoaPaciente.OrgaoEmissorId != Guid.Empty ? _contextDominio.OrgaosEmissores.FindAsync(pessoaPaciente.OrgaoEmissorId).Result.Descricao : null,
                    PacienteProfissional = pessoaPaciente.PacienteProfissional,
                    PaisOrigem = pessoaPaciente.PaisOrigemId != Guid.Empty ? _contextDominio.Paises.FindAsync(pessoaPaciente.PaisOrigemId).Result.Descricao : null,
                    PisPasep = pessoaPaciente.PisPasep,
                    Raca = pessoaPaciente.RacaId != Guid.Empty ? _contextDominio.Racas.FindAsync(pessoaPaciente.RacaId).Result.Nome : null,
                    Recemnascido = pessoaPaciente.Recemnascido,
                    Secao = pessoaPaciente.Secao,
                    Senha = pessoaPaciente.Senha,
                    SerieCtps = pessoaPaciente.SerieCtps,
                    Sexo = pessoaPaciente.Sexo,
                    SituacaoFamiliarConjugal = pessoaPaciente.SituacaoFamiliarConjugalId != Guid.Empty ? _contextDominio.SituacoesFamiliaresConjugais.FindAsync(pessoaPaciente.SituacaoFamiliarConjugalId).Result.Descricao: null,
                    TipoCertidao = pessoaPaciente.TipoCertidaoId != Guid.Empty ? _contextDominio.TiposCertidao.FindAsync(pessoaPaciente.TipoCertidaoId).Result.Descricao : null,
                    TituloEleitor = pessoaPaciente.TituloEleitor,
                    Uf = pessoaPaciente.Uf,
                    UfCtps = pessoaPaciente.UfCtps,
                    Zona = pessoaPaciente.Zona,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    TipoPessoa = "PessoaPaciente",
                    Pessoa = pessoaPaciente
                };

                await base.Adicionar(_pessoaPacienteHistorico, pessoaProfissionalCadastro.PessoaId);


                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoProfissional(PessoaProfissional pessoaProfissional, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _pessoaProfissionalHistorico = new PessoaHistorico
                {
                    Ativo = pessoaProfissional.Ativo,
                    Cep = pessoaProfissional.Cep,
                    Bairro = pessoaProfissional.Bairro,
                    Cidade = _contextDominio.Cidades.FindAsync(pessoaProfissional.CidadeId).Result.Nome,
                    Contato1 = pessoaProfissional.Contato1,
                    Contato2 = pessoaProfissional.Contato2,
                    Contato3 = pessoaProfissional.Contato3,
                    Email = pessoaProfissional.Email,
                    Cns = pessoaProfissional.Cns,
                    CodigoLogin = pessoaProfissional.CodigoLogin,
                    Complemento = pessoaProfissional.Complemento,
                    Cpf = pessoaProfissional.Cpf,
                    DataEmissaoCertidao = pessoaProfissional.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaProfissional.DataEmissaoCtps,
                    DataEntradaPis = pessoaProfissional.DataEntradaPis,
                    Emissao = pessoaProfissional.Emissao,
                    Escolaridade = _contextDominio.Escolaridades.FindAsync(pessoaProfissional.EscolaridadeId).Result.Descricao,
                    Estado = _contextDominio.Estados.FindAsync(pessoaProfissional.EstadoId).Result.Nome,
                    Etnia = _contextDominio.Etnias.FindAsync(pessoaProfissional.EtniaId).Result.Nome,
                    FrequentaEscola = pessoaProfissional.FrequentaEscola,
                    IdadeAparente = pessoaProfissional.IdadeAparente,
                    Identidade = pessoaProfissional.Identidade,
                    Justificativa = _contextDominio.Justificativas.FindAsync(pessoaProfissional.JustificativaId).Result.Descricao,
                    Login = pessoaProfissional.Login,
                    Logradouro = pessoaProfissional.Logradouro,
                    Nacionalidade = _contextDominio.Nacionalidades.FindAsync(pessoaProfissional.NacionalidadeId).Result.Descricao,
                    Nascimento = pessoaProfissional.Nascimento,
                    Naturalidade = _contextDominio.Estados.FindAsync(pessoaProfissional.NaturalidadeId).Result.Nome,
                    NomeCartorio = pessoaProfissional.NomeCartorio,
                    NomeCompleto = pessoaProfissional.NomeCompleto,
                    NomeMae = pessoaProfissional.NomeMae,
                    NomePai = pessoaProfissional.NomePai,
                    NomeSocial = pessoaProfissional.NomeSocial,
                    Numero = pessoaProfissional.Numero,
                    NumeroCtps = pessoaProfissional.NumeroCtps,
                    NumeroFolha = pessoaProfissional.NumeroFolha,
                    NumeroLivro = pessoaProfissional.NumeroLivro,
                    NumeroTermo = pessoaProfissional.NumeroTermo,
                    Ocupacao = _contextDominio.Ocupacoes.FindAsync(pessoaProfissional.OcupacaoId).Result.Descricao,
                    OrgaoEmissor = _contextDominio.OrgaosEmissores.FindAsync(pessoaProfissional.OrgaoEmissorId).Result.Descricao,
                    PacienteProfissional = pessoaProfissional.PacienteProfissional,
                    PaisOrigem = _contextDominio.Paises.FindAsync(pessoaProfissional.PaisOrigemId).Result.Descricao,
                    PisPasep = pessoaProfissional.PisPasep,
                    Raca = _contextDominio.Racas.FindAsync(pessoaProfissional.RacaId).Result.Nome,
                    Secao = pessoaProfissional.Secao,
                    Senha = pessoaProfissional.Senha,
                    SerieCtps = pessoaProfissional.SerieCtps,
                    Sexo = pessoaProfissional.Sexo,
                    SituacaoFamiliarConjugal = _contextDominio.SituacoesFamiliaresConjugais.FindAsync(pessoaProfissional.SituacaoFamiliarConjugalId).Result.Descricao,
                    TipoCertidao = _contextDominio.TiposCertidao.FindAsync(pessoaProfissional.TipoCertidaoId).Result.Descricao,
                    TituloEleitor = pessoaProfissional.TituloEleitor,
                    Uf = pessoaProfissional.Uf,
                    UfCtps = pessoaProfissional.UfCtps,
                    Zona = pessoaProfissional.Zona,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    TipoPessoa = "PessoaProfissional",
                    Pessoa = pessoaProfissional
                };


                await base.Adicionar(_pessoaProfissionalHistorico, pessoaProfissionalCadastro.PessoaId);

                return _response;
            }
            catch (Exception ex)
            {

                _response.Message = ex.InnerException.Message;
                Error.LogError(ex);

            }

            return _response;
        }
    }
}
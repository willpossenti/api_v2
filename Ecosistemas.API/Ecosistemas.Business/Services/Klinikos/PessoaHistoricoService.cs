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
                    Contato1 = pessoaPaciente.Contato1,
                    Contato2 = pessoaPaciente.Contato2,
                    Contato3 = pessoaPaciente.Contato3,
                    Email = pessoaPaciente.Email,
                    Cns = pessoaPaciente.Cns,
                    Complemento = pessoaPaciente.Complemento,
                    Cpf = pessoaPaciente.Cpf,
                    DataEmissaoCertidao = pessoaPaciente.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaPaciente.DataEmissaoCtps,
                    DataEntradaPis = pessoaPaciente.DataEntradaPais,
                    DescricaoNaoIdentificado = pessoaPaciente.DescricaoNaoIdentificado,
                    Emissao = pessoaPaciente.Emissao,
                    FrequentaEscola = pessoaPaciente.FrequentaEscola,
                    IdadeAparente = pessoaPaciente.IdadeAparente,
                    Identidade = pessoaPaciente.Identidade,
                    Login = pessoaPaciente.Login,
                    Logradouro = pessoaPaciente.Logradouro,
                    Nascimento = pessoaPaciente.Nascimento,
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
                    PacienteProfissional = pessoaPaciente.PacienteProfissional,
                    PisPasep = pessoaPaciente.PisPasep,
                    Recemnascido = pessoaPaciente.Recemnascido,
                    Secao = pessoaPaciente.Secao,
                    Senha = pessoaPaciente.Senha,
                    SerieCtps = pessoaPaciente.SerieCtps,
                    Sexo = pessoaPaciente.Sexo,
                    TituloEleitor = pessoaPaciente.TituloEleitor,
                    Uf = pessoaPaciente.Uf,
                    UfCtps = pessoaPaciente.UfCtps,
                    Zona = pessoaPaciente.Zona,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    TipoPessoa = "PessoaPaciente",
                    Pessoa = pessoaPaciente
                };

                if (pessoaPaciente.CidadeId != Guid.Empty)
                    _pessoaPacienteHistorico.Cidade = _contextDominio.Cidades.FindAsync(pessoaPaciente.CidadeId).Result.Nome;

                if (pessoaPaciente.EscolaridadeId != Guid.Empty)
                    _pessoaPacienteHistorico.Escolaridade = _contextDominio.Escolaridades.FindAsync(pessoaPaciente.EscolaridadeId).Result.Descricao;

                if (pessoaPaciente.EstadoId != Guid.Empty)
                    _pessoaPacienteHistorico.Estado = _contextDominio.Estados.FindAsync(pessoaPaciente.EstadoId).Result.Nome;

                if (pessoaPaciente.EtniaId != Guid.Empty)
                    _pessoaPacienteHistorico.Etnia = _contextDominio.Etnias.FindAsync(pessoaPaciente.EtniaId).Result.Nome;

                if (pessoaPaciente.JustificativaId != Guid.Empty)
                    _pessoaPacienteHistorico.Justificativa = _contextDominio.Justificativas.FindAsync(pessoaPaciente.JustificativaId).Result.Descricao;

                if (pessoaPaciente.NacionalidadeId != Guid.Empty)
                    _pessoaPacienteHistorico.Nacionalidade = _contextDominio.Nacionalidades.FindAsync(pessoaPaciente.NacionalidadeId).Result.Descricao;

                if (pessoaPaciente.NaturalidadeId != Guid.Empty)
                    _pessoaPacienteHistorico.Naturalidade = _contextDominio.Estados.FindAsync(pessoaPaciente.NaturalidadeId).Result.Nome;

                if (pessoaPaciente.OcupacaoId != Guid.Empty)
                    _pessoaPacienteHistorico.Ocupacao = _contextDominio.Ocupacoes.FindAsync(pessoaPaciente.OcupacaoId).Result.Descricao;

                if (pessoaPaciente.OrgaoEmissorId != Guid.Empty)
                    _pessoaPacienteHistorico.OrgaoEmissor = _contextDominio.OrgaosEmissores.FindAsync(pessoaPaciente.OrgaoEmissorId).Result.Descricao;


                if (pessoaPaciente.PaisOrigemId != Guid.Empty)
                    _pessoaPacienteHistorico.PaisOrigem = _contextDominio.Paises.FindAsync(pessoaPaciente.PaisOrigemId).Result.Descricao;

                if (pessoaPaciente.RacaId != Guid.Empty)
                    _pessoaPacienteHistorico.Raca = _contextDominio.Racas.FindAsync(pessoaPaciente.RacaId).Result.Nome;

                if (pessoaPaciente.SituacaoFamiliarConjugalId != Guid.Empty)
                    _pessoaPacienteHistorico.SituacaoFamiliarConjugal = _contextDominio.SituacoesFamiliaresConjugais.FindAsync(pessoaPaciente.SituacaoFamiliarConjugalId).Result.Descricao;

                if (pessoaPaciente.TipoCertidaoId != Guid.Empty)
                    _pessoaPacienteHistorico.TipoCertidao = _contextDominio.TiposCertidao.FindAsync(pessoaPaciente.TipoCertidaoId).Result.Descricao;

                if (pessoaPaciente.PessoaStatusId != Guid.Empty)
                    _pessoaPacienteHistorico.PessoaStatus = _contextDominio.PessoaStatus.FindAsync(pessoaPaciente.PessoaStatusId).Result.Descricao;


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
                    Contato1 = pessoaProfissional.Contato1,
                    Contato2 = pessoaProfissional.Contato2,
                    Contato3 = pessoaProfissional.Contato3,
                    Email = pessoaProfissional.Email,
                    Cns = pessoaProfissional.Cns,
                    Complemento = pessoaProfissional.Complemento,
                    Cpf = pessoaProfissional.Cpf,
                    DataEmissaoCertidao = pessoaProfissional.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaProfissional.DataEmissaoCtps,
                    DataEntradaPis = pessoaProfissional.DataEntradaPais,
                    Emissao = pessoaProfissional.Emissao,
                    FrequentaEscola = pessoaProfissional.FrequentaEscola,
                    IdadeAparente = pessoaProfissional.IdadeAparente,
                    Identidade = pessoaProfissional.Identidade,
                    Login = pessoaProfissional.Login,
                    Logradouro = pessoaProfissional.Logradouro,
                    Nascimento = pessoaProfissional.Nascimento,
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
                    PacienteProfissional = pessoaProfissional.PacienteProfissional,
                    PisPasep = pessoaProfissional.PisPasep,
                    Secao = pessoaProfissional.Secao,
                    Senha = pessoaProfissional.Senha,
                    SerieCtps = pessoaProfissional.SerieCtps,
                    Sexo = pessoaProfissional.Sexo,
                    TituloEleitor = pessoaProfissional.TituloEleitor,
                    Uf = pessoaProfissional.Uf,
                    UfCtps = pessoaProfissional.UfCtps,
                    Zona = pessoaProfissional.Zona,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    TipoPessoa = "PessoaProfissional",
                    Pessoa = pessoaProfissional
                };


                if (pessoaProfissional.CidadeId != Guid.Empty)
                    _pessoaProfissionalHistorico.Cidade = _contextDominio.Cidades.FindAsync(pessoaProfissional.CidadeId).Result.Nome;

                if (pessoaProfissional.EscolaridadeId != Guid.Empty)
                    _pessoaProfissionalHistorico.Escolaridade = _contextDominio.Escolaridades.FindAsync(pessoaProfissional.EscolaridadeId).Result.Descricao;

                if (pessoaProfissional.EstadoId != Guid.Empty)
                    _pessoaProfissionalHistorico.Estado = _contextDominio.Estados.FindAsync(pessoaProfissional.EstadoId).Result.Nome;

                if (pessoaProfissional.EtniaId != Guid.Empty)
                    _pessoaProfissionalHistorico.Etnia = _contextDominio.Etnias.FindAsync(pessoaProfissional.EtniaId).Result.Nome;

                if (pessoaProfissional.JustificativaId != Guid.Empty)
                    _pessoaProfissionalHistorico.Justificativa = _contextDominio.Justificativas.FindAsync(pessoaProfissional.JustificativaId).Result.Descricao;

                if (pessoaProfissional.NacionalidadeId != Guid.Empty)
                    _pessoaProfissionalHistorico.Nacionalidade = _contextDominio.Nacionalidades.FindAsync(pessoaProfissional.NacionalidadeId).Result.Descricao;

                if (pessoaProfissional.NaturalidadeId != Guid.Empty)
                    _pessoaProfissionalHistorico.Naturalidade = _contextDominio.Estados.FindAsync(pessoaProfissional.NaturalidadeId).Result.Nome;

                if (pessoaProfissional.OcupacaoId != Guid.Empty)
                    _pessoaProfissionalHistorico.Ocupacao = _contextDominio.Ocupacoes.FindAsync(pessoaProfissional.OcupacaoId).Result.Descricao;

                if (pessoaProfissional.OrgaoEmissorId != Guid.Empty)
                    _pessoaProfissionalHistorico.OrgaoEmissor = _contextDominio.OrgaosEmissores.FindAsync(pessoaProfissional.OrgaoEmissorId).Result.Descricao;

                if (pessoaProfissional.PaisOrigemId != Guid.Empty)
                    _pessoaProfissionalHistorico.PaisOrigem = _contextDominio.Paises.FindAsync(pessoaProfissional.PaisOrigemId).Result.Descricao;

                if (pessoaProfissional.RacaId != Guid.Empty)
                    _pessoaProfissionalHistorico.Raca = _contextDominio.Racas.FindAsync(pessoaProfissional.RacaId).Result.Nome;

                if (pessoaProfissional.SituacaoFamiliarConjugalId != Guid.Empty)
                    _pessoaProfissionalHistorico.SituacaoFamiliarConjugal = _contextDominio.SituacoesFamiliaresConjugais.FindAsync(pessoaProfissional.SituacaoFamiliarConjugalId).Result.Descricao;

                if (pessoaProfissional.TipoCertidaoId != Guid.Empty)
                    _pessoaProfissionalHistorico.TipoCertidao = _contextDominio.TiposCertidao.FindAsync(pessoaProfissional.TipoCertidaoId).Result.Descricao;


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
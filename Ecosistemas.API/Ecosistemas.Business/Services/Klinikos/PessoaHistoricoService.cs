using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
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


        public PessoaHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {

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
                    Cidade = pessoaPaciente.Cidade?.Nome,
                    Contato1 = pessoaPaciente.Contato1,
                    Contato2 = pessoaPaciente.Contato2,
                    Contato3 = pessoaPaciente.Contato3,
                    Email = pessoaPaciente.Email,
                    Cns = pessoaPaciente.Cns,
                    Complemento = pessoaPaciente.Complemento,
                    Cpf = pessoaPaciente.Cpf,
                    DataEmissaoCertidao = pessoaPaciente.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaPaciente.DataEmissaoCtps,
                    DataEntradaPis = pessoaPaciente.DataEntradaPis,
                    DescricaoNaoIdentificado = pessoaPaciente.DescricaoNaoIdentificado,
                    Emissao = pessoaPaciente.Emissao,
                    Escolaridade = pessoaPaciente.Escolaridade?.Descricao,
                    Estado = pessoaPaciente.Estado?.Nome,
                    Etnia = pessoaPaciente.Etnia?.Nome,
                    FrequentaEscola = pessoaPaciente.FrequentaEscola,
                    IdadeAparente = pessoaPaciente.IdadeAparente,
                    Identidade = pessoaPaciente.Identidade,
                    Justificativa = pessoaPaciente.Justificativa?.Descricao,
                    Login = pessoaPaciente.Login,
                    Logradouro = pessoaPaciente.Logradouro,
                    Nacionalidade = pessoaPaciente.Nacionalidade?.Descricao,
                    Nascimento = pessoaPaciente.Nascimento,
                    Naturalidade = pessoaPaciente.Naturalidade?.Nome,
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
                    Ocupacao = pessoaPaciente.Ocupacao?.Descricao,
                    OrgaoEmissor = pessoaPaciente.OrgaoEmissor?.Descricao,
                    PacienteProfissional = pessoaPaciente.PacienteProfissional,
                    PaisOrigem = pessoaPaciente.PaisOrigem?.Descricao,
                    PisPasep = pessoaPaciente.PisPasep,
                    Raca = pessoaPaciente.Raca?.Nome,
                    Recemnascido = pessoaPaciente.Recemnascido,
                    Secao = pessoaPaciente.Secao,
                    Senha = pessoaPaciente.Senha,
                    SerieCtps = pessoaPaciente.SerieCtps,
                    Sexo = pessoaPaciente.Sexo,
                    SituacaoFamiliarConjugal = pessoaPaciente.SituacaoFamiliarConjugal?.CodigoSituacaoFamiliarConjugal,
                    TipoCertidao = pessoaPaciente.TipoCertidao?.Descricao,
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
                    Cidade = pessoaProfissional.Cidade?.Nome,
                    Contato1 = pessoaProfissional.Contato1,
                    Contato2 = pessoaProfissional.Contato2,
                    Contato3 = pessoaProfissional.Contato3,
                    Email = pessoaProfissional.Email,
                    Cns = pessoaProfissional.Cns,
                    Complemento = pessoaProfissional.Complemento,
                    Cpf = pessoaProfissional.Cpf,
                    DataEmissaoCertidao = pessoaProfissional.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaProfissional.DataEmissaoCtps,
                    DataEntradaPis = pessoaProfissional.DataEntradaPis,
                    Emissao = pessoaProfissional.Emissao,
                    Escolaridade = pessoaProfissional.Escolaridade?.Descricao,
                    Estado = pessoaProfissional.Estado?.Nome,
                    Etnia = pessoaProfissional.Etnia?.Nome,
                    FrequentaEscola = pessoaProfissional.FrequentaEscola,
                    IdadeAparente = pessoaProfissional.IdadeAparente,
                    Identidade = pessoaProfissional.Identidade,
                    Justificativa = pessoaProfissional.Justificativa?.Descricao,
                    Login = pessoaProfissional.Login,
                    Logradouro = pessoaProfissional.Logradouro,
                    Nacionalidade = pessoaProfissional.Nacionalidade?.Descricao,
                    Nascimento = pessoaProfissional.Nascimento,
                    Naturalidade = pessoaProfissional.Naturalidade?.Nome,
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
                    Ocupacao = pessoaProfissional.Ocupacao?.Descricao,
                    OrgaoEmissor = pessoaProfissional.OrgaoEmissor?.Descricao,
                    PacienteProfissional = pessoaProfissional.PacienteProfissional,
                    PaisOrigem = pessoaProfissional.PaisOrigem?.Descricao,
                    PisPasep = pessoaProfissional.PisPasep,
                    Raca = pessoaProfissional.Raca?.Nome,
                    Secao = pessoaProfissional.Secao,
                    Senha = pessoaProfissional.Senha,
                    SerieCtps = pessoaProfissional.SerieCtps,
                    Sexo = pessoaProfissional.Sexo,
                    SituacaoFamiliarConjugal = pessoaProfissional.SituacaoFamiliarConjugal?.CodigoSituacaoFamiliarConjugal,
                    TipoCertidao = pessoaProfissional.TipoCertidao?.Descricao,
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
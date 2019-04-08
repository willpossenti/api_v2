using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecosistemas.Business.Utility;

namespace Ecosistemas.Business.Services.Klinikos
{
    public class PessoaHistoricoService : BaseService<PessoaHistorico>, IPessoaHistoricoService
    {
        private readonly KlinikosDbContext _context;
        private IPessoaContatoHistoricoService _servicePessoaContatoHistorico;

        public PessoaHistoricoService(KlinikosDbContext context) : base(context)
        {
            _context = context;
            _servicePessoaContatoHistorico = new PessoaContatoHistoricoService(context);
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
                    Cidade = pessoaPaciente.Cidade == null ? null : pessoaPaciente.Cidade.Nome,
                    Cns = pessoaPaciente.Cns,
                    CodigoLogin = pessoaPaciente.CodigoLogin,
                    Complemento = pessoaPaciente.Complemento,
                    Cpf = pessoaPaciente.Cpf,
                    DataEmissaoCertidao = pessoaPaciente.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaPaciente.DataEmissaoCtps,
                    DataEntradaPis = pessoaPaciente.DataEntradaPis,
                    DescricaoNaoIdentificado = pessoaPaciente.DescricaoNaoIdentificado,
                    Emissao = pessoaPaciente.Emissao,
                    Escolaridade = pessoaPaciente.Escolaridade == null ? null : pessoaPaciente.Escolaridade.Descricao,
                    Estado = pessoaPaciente.Estado == null ? null : pessoaPaciente.Estado.Nome,
                    Etnia = pessoaPaciente.Etnia == null ? null : pessoaPaciente.Etnia.Nome,
                    FrequentaEscola = pessoaPaciente.FrequentaEscola,
                    IdadeAparente = pessoaPaciente.IdadeAparente,
                    Identidade = pessoaPaciente.Identidade,
                    Justificativa = pessoaPaciente.Justificativa == null ? null : pessoaPaciente.Justificativa.Descricao,
                    Login = pessoaPaciente.Login,
                    Logradouro = pessoaPaciente.Logradouro,
                    Nacionalidade = pessoaPaciente.Nacionalidade == null ? null : pessoaPaciente.Nacionalidade.Descricao,
                    Nascimento = pessoaPaciente.Nascimento,
                    Naturalidade = pessoaPaciente.Naturalidade == null ? null : pessoaPaciente.Naturalidade.Nome,
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
                    Ocupacao = pessoaPaciente.Ocupacao == null ? null : pessoaPaciente.Ocupacao.Descricao,
                    OrgaoEmissor = pessoaPaciente.OrgaoEmissor,
                    PacienteProfissional = pessoaPaciente.PacienteProfissional,
                    PaisOrigem = pessoaPaciente.PaisOrigem == null ? null : pessoaPaciente.PaisOrigem.Descricao,
                    PisPasep = pessoaPaciente.PisPasep,
                    Raca = pessoaPaciente.Raca == null ? null : pessoaPaciente.Raca.Nome,
                    Recemnascido = pessoaPaciente.Recemnascido,
                    Secao = pessoaPaciente.Secao,
                    Senha = pessoaPaciente.Senha,
                    SerieCtps = pessoaPaciente.SerieCtps,
                    Sexo = pessoaPaciente.Sexo,
                    SituacaoFamiliarConjugal = pessoaPaciente.SituacaoFamiliarConjugal == null ? null : pessoaPaciente.SituacaoFamiliarConjugal.CodigoSituacaoFamiliarConjugal,
                    TipoCertidao = pessoaPaciente.TipoCertidao == null ? null : pessoaPaciente.TipoCertidao.Descricao,
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


                var listaPessoaContato = new List<PessoaContatoHistorico>();

                for (int i = 0; i < pessoaPaciente.PessoaContatos.Count; i++)
                {
                    var _pessoaContatoHistorico = new PessoaContatoHistorico
                    {
                        Ativo = true,
                        Celular = pessoaPaciente.PessoaContatos[i].Celular,
                        DataAlteracao = DateTime.Now,
                        Email = pessoaPaciente.PessoaContatos[i].Email,
                        PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                        PessoaContato = pessoaPaciente.PessoaContatos[i],
                        Telefone = pessoaPaciente.PessoaContatos[i].Telefone,
                        TipoPessoa = "PessoaPaciente"
                    };
                    listaPessoaContato.Add(_pessoaContatoHistorico);

                }

                await _servicePessoaContatoHistorico.AdicionarRange(listaPessoaContato, pessoaProfissionalCadastro.PessoaId);


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
                    Cidade = pessoaProfissional.Cidade == null ? null : pessoaProfissional.Cidade.Nome,
                    Cns = pessoaProfissional.Cns,
                    CodigoLogin = pessoaProfissional.CodigoLogin,
                    Complemento = pessoaProfissional.Complemento,
                    Cpf = pessoaProfissional.Cpf,
                    DataEmissaoCertidao = pessoaProfissional.DataEmissaoCertidao,
                    DataEmissaoCtps = pessoaProfissional.DataEmissaoCtps,
                    DataEntradaPis = pessoaProfissional.DataEntradaPis,
                    Emissao = pessoaProfissional.Emissao,
                    Escolaridade = pessoaProfissional.Escolaridade == null ? null : pessoaProfissional.Escolaridade.Descricao,
                    Estado = pessoaProfissional.Estado == null ? null : pessoaProfissional.Estado.Nome,
                    Etnia = pessoaProfissional.Etnia == null ? null : pessoaProfissional.Etnia.Nome,
                    FrequentaEscola = pessoaProfissional.FrequentaEscola,
                    IdadeAparente = pessoaProfissional.IdadeAparente,
                    Identidade = pessoaProfissional.Identidade,
                    Justificativa = pessoaProfissional.Justificativa == null ? null : pessoaProfissional.Justificativa.Descricao,
                    Login = pessoaProfissional.Login,
                    Logradouro = pessoaProfissional.Logradouro,
                    Nacionalidade = pessoaProfissional.Nacionalidade == null ? null : pessoaProfissional.Nacionalidade.Descricao,
                    Nascimento = pessoaProfissional.Nascimento,
                    Naturalidade = pessoaProfissional.Naturalidade == null ? null : pessoaProfissional.Naturalidade.Nome,
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
                    Ocupacao = pessoaProfissional.Ocupacao == null ? null : pessoaProfissional.Ocupacao.Descricao,
                    OrgaoEmissor = pessoaProfissional.OrgaoEmissor,
                    PacienteProfissional = pessoaProfissional.PacienteProfissional,
                    PaisOrigem = pessoaProfissional.PaisOrigem == null ? null : pessoaProfissional.PaisOrigem.Descricao,
                    PisPasep = pessoaProfissional.PisPasep,
                    Raca = pessoaProfissional.Raca == null ? null : pessoaProfissional.Raca.Nome,
                    Secao = pessoaProfissional.Secao,
                    Senha = pessoaProfissional.Senha,
                    SerieCtps = pessoaProfissional.SerieCtps,
                    Sexo = pessoaProfissional.Sexo,
                    SituacaoFamiliarConjugal = pessoaProfissional.SituacaoFamiliarConjugal == null ? null : pessoaProfissional.SituacaoFamiliarConjugal.CodigoSituacaoFamiliarConjugal,
                    TipoCertidao = pessoaProfissional.TipoCertidao == null ? null : pessoaProfissional.TipoCertidao.Descricao,
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


                var listaPessoaContato = new List<PessoaContatoHistorico>();

                for (int i = 0; i < pessoaProfissional.PessoaContatos.Count; i++)
                {
                    var _pessoaContatoHistorico = new PessoaContatoHistorico
                    {
                        Ativo = true,
                        Celular = pessoaProfissional.PessoaContatos[i].Celular,
                        DataAlteracao = DateTime.Now,
                        Email = pessoaProfissional.PessoaContatos[i].Email,
                        PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                        PessoaContato = pessoaProfissional.PessoaContatos[i],
                        Telefone = pessoaProfissional.PessoaContatos[i].Telefone,
                        TipoPessoa = "PessoaProfissional"
                    };
                    listaPessoaContato.Add(_pessoaContatoHistorico);

                }

                if (listaPessoaContato.Count > 0)
                    await _servicePessoaContatoHistorico.AdicionarRange(listaPessoaContato, pessoaProfissionalCadastro.PessoaId);

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
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
    public class RegistroBoletimHistoricoService : BaseService<RegistroBoletimHistorico>, IRegistroBoletimHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public RegistroBoletimHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;
        }
        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoRegistroBoletim(RegistroBoletim registroBoletim, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _registroBoletimHistorico = new RegistroBoletimHistorico
                {
                    Ativo = registroBoletim.Ativo,
                    Bairro = registroBoletim.Bairro,
                    Cep = registroBoletim.Cep,
                    Cidade = registroBoletim.Cidade == null? null: registroBoletim.Cidade.Nome,
                    Complemento = registroBoletim.Complemento,
                    DataBoletim = registroBoletim.DataBoletim,
                    DataOcorrencia = registroBoletim.DataOcorrencia,
                    EnderecoInformante = registroBoletim.EnderecoInformante,
                    Especialidade = registroBoletim.Especialidade == null? null: registroBoletim.Especialidade.Descricao,
                    Estado = registroBoletim.Estado == null? null: registroBoletim.Estado.Nome,
                    GrauParentesco = registroBoletim.GrauParentesco,
                    Logradouro = registroBoletim.Logradouro,
                    NomeInformante = registroBoletim.NomeInformante,
                    Numero = registroBoletim.Numero,
                    NumeroBoletim = registroBoletim.NumeroBoletim,
                    Procedencia = registroBoletim.Procedencia,
                    RegistroBoletim = registroBoletim,
                    TelefoneInformante = registroBoletim.TelefoneInformante,
                    TipoChegada = registroBoletim.TipoChegada == null? null: registroBoletim.TipoChegada.Descricao,
                    TipoOcorrencia = registroBoletim.TipoOcorrencia == null? null: registroBoletim.TipoOcorrencia.Descricao,
                    TipoPerfuracao = registroBoletim.TipoPerfuracao,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto
                };

                await base.Adicionar(_registroBoletimHistorico, pessoaProfissionalCadastro.PessoaId);


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
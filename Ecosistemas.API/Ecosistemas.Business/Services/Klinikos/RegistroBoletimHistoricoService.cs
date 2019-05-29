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
    public class RegistroBoletimHistoricoService : BaseService<RegistroBoletimHistorico>, IRegistroBoletimHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public RegistroBoletimHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;
        }
        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoRegistroBoletim(RegistroBoletim registroBoletim, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _registroBoletimHistorico = new RegistroBoletimHistorico
                {
                    Ativo = registroBoletim.Ativo,
                    DataBoletim = registroBoletim.DataBoletim,
                    EnderecoInformante = registroBoletim.EnderecoInformante,
                    Especialidade = _contextDominio.Especialidades.FindAsync(registroBoletim.EspecialidadeId).Result.Descricao,
                    GrauParentesco = registroBoletim.GrauParentesco,
                    NomeInformante = registroBoletim.NomeInformante,
                    NumeroBoletim = registroBoletim.NumeroBoletim,
                    RegistroBoletim = registroBoletim,
                    TelefoneInformante = registroBoletim.TelefoneInformante,
                    TipoChegada = _contextDominio.TiposChegada.FindAsync(registroBoletim.TipoChegadaId).Result.Descricao,
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
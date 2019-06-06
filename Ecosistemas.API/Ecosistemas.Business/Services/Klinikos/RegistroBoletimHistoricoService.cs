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
                    GrauParentesco = registroBoletim.GrauParentesco,
                    NomeInformante = registroBoletim.NomeInformante,
                    NumeroBoletim = registroBoletim.NumeroBoletim,
                    RegistroBoletim = registroBoletim,
                    TelefoneInformante = registroBoletim.TelefoneInformante,
                    DataAlteracao = DateTime.Now,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto
                };

                if (registroBoletim.EspecialidadeId != Guid.Empty)
                    _registroBoletimHistorico.Especialidade = _contextDominio.Especialidades.FindAsync(registroBoletim.EspecialidadeId).Result.Descricao;

                if (registroBoletim.TipoChegadaId != Guid.Empty)
                    _registroBoletimHistorico.TipoChegada = _contextDominio.TiposChegada.FindAsync(registroBoletim.TipoChegadaId).Result.Descricao;


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
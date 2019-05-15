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
    public class AtendimentoMedicoHistoricoService : BaseService<AtendimentoMedicoHistorico>, IAtendimentoMedicoHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public AtendimentoMedicoHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAtendimentoMedico(AtendimentoMedico atendimentoMedico, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AtendimentoMedicoHistorico = new AtendimentoMedicoHistorico
                {
                    AtendimentoMedico = atendimentoMedico,
                    Peso = atendimentoMedico.Peso,
                    Altura = atendimentoMedico.Altura,
                    Imc = atendimentoMedico.Imc,
                    Temperatura = atendimentoMedico.Temperatura,
                    PressaoArterialDiastolica = atendimentoMedico.PressaoArterialDiastolica,
                    PressaoArterialSistolica = atendimentoMedico.PressaoArterialSistolica,
                    Pulso = atendimentoMedico.Pulso,
                    FrequenciaRespiratoria = atendimentoMedico.FrequenciaRespiratoria,
                    Saturacao = atendimentoMedico.Saturacao,
                    Anamnese = atendimentoMedico.Anamnese,
                    CID = atendimentoMedico.CID?.Nome,
                    CampoObservacao = atendimentoMedico.CampoObservacao,
                    CondutaExames = atendimentoMedico.CondutaExames,
                    CondutaAtestado = atendimentoMedico.CondutaAtestado,
                    CondutaPrescricao = atendimentoMedico.CondutaPrescricao,
                    Atestado = atendimentoMedico.ModeloAtestado?.Atestado,
                    SuspeitaDiagnostico = atendimentoMedico.SuspeitaDiagnostico,                    
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    DataAlteracao = DateTime.Now,
                    Ativo = atendimentoMedico.Ativo,
                };

                await base.Adicionar(_AtendimentoMedicoHistorico, pessoaProfissionalCadastro.PessoaId);


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
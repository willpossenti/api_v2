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
    public class AtendimentoMedicoHistoricoService : BaseService<AtendimentoMedicoHistorico>, IAtendimentoMedicoHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public AtendimentoMedicoHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

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
                    CID = _contextDominio.CID.FindAsync(atendimentoMedico.CIDId).Result.Nome,
                    CampoObservacao = atendimentoMedico.CampoObservacao,
                    Receita = atendimentoMedico.Receita,
                    Prescricao = atendimentoMedico.Prescricao,
                    Atestado = atendimentoMedico.ModeloAtestado?.Atestado,
                    ValidadeAtestado = atendimentoMedico.ValidadeAtestado,
                    SuspeitaDiagnostico = atendimentoMedico.SuspeitaDiagnostico,
                    TipoSaida = atendimentoMedico.TipoSaida,
                    DataSaida = atendimentoMedico.DataSaida,
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
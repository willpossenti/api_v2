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
    public class AcolhimentoHistoricoService : BaseService<AcolhimentoHistorico>, IAcolhimentoHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public AcolhimentoHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoAcolhimento(Acolhimento acolhimento, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _AcolhimentoHistorico = new AcolhimentoHistorico
                {
                    Acolhimento = acolhimento,
                    Nome = acolhimento.PessoaPaciente == null ? null : acolhimento.PessoaPaciente.NomeCompleto,
                    CPF = acolhimento.PessoaPaciente == null ? null : acolhimento.PessoaPaciente.Cpf,
                    CNS = acolhimento.PessoaPaciente == null ? null : acolhimento.PessoaPaciente.Cns,
                    NomeSocial = acolhimento.PessoaPaciente == null ? null : acolhimento.PessoaPaciente.NomeSocial,
                    Especialidade = acolhimento.Especialidade == null ? null : acolhimento.Especialidade.Descricao,
                    Prioridade = acolhimento.Prioridade == null ? null : acolhimento.Prioridade.Nome,
                    Risco = acolhimento.Risco,
                    Peso = acolhimento.Peso,
                    Altura = acolhimento.Altura,
                    IMC = acolhimento.IMC,
                    Temperatura = acolhimento.Temperatura,
                    PressaoArterial = acolhimento.PressaoArterial,
                    Pulso = acolhimento.Pulso,
                    FrequenciaRespiratoria = acolhimento.FrequenciaRespiratoria,
                    Saturacao = acolhimento.Saturacao,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    DataAlteracao = DateTime.Now,
                    Ativo = acolhimento.Ativo,
                };

                await base.Adicionar(_AcolhimentoHistorico, pessoaProfissionalCadastro.PessoaId);


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
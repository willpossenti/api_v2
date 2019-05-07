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
    public class ClassificacaoRiscoHistoricoService : BaseService<ClassificacaoRiscoHistorico>, IClassificacaoRiscoHistoricoService
    {
        private readonly KlinikosDbContext _contextKlinikos;
        private readonly ApiDbContext _context;

        public ClassificacaoRiscoHistoricoService(KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextKlinikos = contextKlinikos;
            _context = context;

        }

        public async Task<CustomResponse<PessoaHistorico>> AdicionarHistoricoClassificacaoRisco(ClassificacaoRisco classificacaoRisco, PessoaProfissional pessoaProfissionalCadastro)
        {
            var _response = new CustomResponse<PessoaHistorico>();


            try
            {
                var _ClassificacaoRiscoHistorico = new ClassificacaoRiscoHistorico
                {
                    ClassificacaoRisco = classificacaoRisco,
                    Peso = classificacaoRisco.Peso,
                    Altura = classificacaoRisco.Altura,
                    Imc = classificacaoRisco.Imc,
                    Temperatura = classificacaoRisco.Temperatura,
                    PressaoArterialDiastolica = classificacaoRisco.PressaoArterialDiastolica,
                    PressaoArterialSistolica = classificacaoRisco.PressaoArterialSistolica,
                    Pulso = classificacaoRisco.Pulso,
                    FrequenciaRespiratoria = classificacaoRisco.FrequenciaRespiratoria,
                    Saturacao = classificacaoRisco.Saturacao,
                    EscalaDor = classificacaoRisco.EscalaDor?.Descricao,
                    DescricaoQueixa = classificacaoRisco.DescricaoQueixa,
                    NivelConsciencia = classificacaoRisco.NivelConsciencia?.Descricao,
                    TipoChegada = classificacaoRisco.TipoChegada?.Descricao,
                    Avaliacao = classificacaoRisco.Avaliacao,
                    Sutura = classificacaoRisco.Sutura,
                    CausaExterna = classificacaoRisco.CausaExterna?.Descricao,
                    Cardiopata = classificacaoRisco.Cardiopata,
                    Diabete = classificacaoRisco.Diabete,
                    Hipertensao = classificacaoRisco.Hipertensao,
                    Outros = classificacaoRisco.Outros,
                    ObservacaoOutros = classificacaoRisco.ObservacaoOutros,
                    RenalCronico = classificacaoRisco.RenalCronico,
                    RespiratoriaCronica = classificacaoRisco.RespiratoriaCronica,
                    ObservacaoRespiratoriaCronica = classificacaoRisco.ObservacaoRespiratoriaCronica,
                    Especialidade = classificacaoRisco.Especialidade?.Descricao,
                    Risco = classificacaoRisco.Risco?.Descricao,
                    AberturaOcular = classificacaoRisco.AberturaOcular?.Variavel,
                    RespostaVerbal = classificacaoRisco.RespostaVerbal?.Variavel,
                    RespostaMotora = classificacaoRisco.RespostaMotora?.Variavel,
                    Procedencia = classificacaoRisco.Procedencia,
                    TipoOcorrencia = classificacaoRisco.TipoOcorrencia?.Descricao,
                    DataOcorrencia = classificacaoRisco.DataOcorrencia,
                    Pab = classificacaoRisco.Pab,
                    Paf = classificacaoRisco.Paf,
                    Cep = classificacaoRisco.Cep,
                    Logradouro = classificacaoRisco.Logradouro,
                    Numero = classificacaoRisco.Numero,
                    Complemento = classificacaoRisco.Complemento,
                    Bairro = classificacaoRisco.Bairro,
                    Estado = classificacaoRisco.Estado?.Nome,
                    Cidade = classificacaoRisco.Cidade?.Nome,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    DataAlteracao = DateTime.Now,
                    Ativo = classificacaoRisco.Ativo,
                };

                await base.Adicionar(_ClassificacaoRiscoHistorico, pessoaProfissionalCadastro.PessoaId);


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
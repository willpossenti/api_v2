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
    public class ClassificacaoRiscoHistoricoService : BaseService<ClassificacaoRiscoHistorico>, IClassificacaoRiscoHistoricoService
    {
        private readonly DominioDbContext _contextDominio;

        public ClassificacaoRiscoHistoricoService(DominioDbContext contextDominio, KlinikosDbContext contextKlinikos, ApiDbContext context) : base(contextKlinikos, context)
        {
            _contextDominio = contextDominio;

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
                    EscalaDor = _contextDominio.EscalasDor.FindAsync(classificacaoRisco.EscalaDorId).Result.Descricao,
                    DescricaoQueixa = classificacaoRisco.DescricaoQueixa,
                    NivelConsciencia = _contextDominio.NiveisConsciencia.FindAsync(classificacaoRisco.NivelConscienciaId).Result.Descricao,
                    TipoChegada = _contextDominio.TiposChegada.FindAsync(classificacaoRisco.TipoChegadaId).Result.Descricao,
                    Avaliacao = classificacaoRisco.Avaliacao,
                    Sutura = classificacaoRisco.Sutura,
                    CausaExterna = _contextDominio.CausasExternas.FindAsync(classificacaoRisco.CausaExternaId).Result.Descricao,
                    Cardiopata = classificacaoRisco.Cardiopata,
                    Diabete = classificacaoRisco.Diabete,
                    Hipertensao = classificacaoRisco.Hipertensao,
                    Outros = classificacaoRisco.Outros,
                    ObservacaoOutros = classificacaoRisco.ObservacaoOutros,
                    RenalCronico = classificacaoRisco.RenalCronico,
                    RespiratoriaCronica = classificacaoRisco.RespiratoriaCronica,
                    ObservacaoRespiratoriaCronica = classificacaoRisco.ObservacaoRespiratoriaCronica,
                    Especialidade = _contextDominio.Especialidades.FindAsync(classificacaoRisco.EspecialidadeId).Result.Descricao,
                    Risco = _contextDominio.Riscos.FindAsync(classificacaoRisco.RiscoId).Result.Descricao,
                    AberturaOcular = _contextDominio.AberturasOculares.FindAsync(classificacaoRisco.AberturaOcularId).Result.Variavel,
                    RespostaVerbal = _contextDominio.RespostasVerbais.FindAsync(classificacaoRisco.RespostaVerbalId).Result.Variavel,
                    RespostaMotora = _contextDominio.RespostasMotoras.FindAsync(classificacaoRisco.RespostaMotoraId).Result.Variavel,
                    Procedencia = classificacaoRisco.Procedencia,
                    TipoOcorrencia = _contextDominio.TiposOcorrencia.FindAsync(classificacaoRisco.TipoOcorrenciaId).Result.Descricao,
                    DataOcorrencia = classificacaoRisco.DataOcorrencia,
                    Pab = classificacaoRisco.Pab,
                    Paf = classificacaoRisco.Paf,
                    Cep = classificacaoRisco.Cep,
                    Logradouro = classificacaoRisco.Logradouro,
                    Numero = classificacaoRisco.Numero,
                    Complemento = classificacaoRisco.Complemento,
                    Bairro = classificacaoRisco.Bairro,
                    Estado = _contextDominio.Estados.FindAsync(classificacaoRisco.EstadoId).Result.Nome,
                    Cidade = _contextDominio.Cidades.FindAsync(classificacaoRisco.CidadeId).Result.Nome,
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
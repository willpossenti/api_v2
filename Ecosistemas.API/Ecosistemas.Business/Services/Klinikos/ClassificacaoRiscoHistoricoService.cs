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
                    Paciente = classificacaoRisco.PessoaPaciente.NomeCompleto,
                    DataClassificaoRisco = classificacaoRisco.DataClassificaoRisco,
                    Peso = classificacaoRisco.Peso,
                    Altura = classificacaoRisco.Altura,
                    Imc = classificacaoRisco.Imc,
                    Temperatura = classificacaoRisco.Temperatura,
                    PressaoArterialDiastolica = classificacaoRisco.PressaoArterialDiastolica,
                    PressaoArterialSistolica = classificacaoRisco.PressaoArterialSistolica,
                    Pulso = classificacaoRisco.Pulso,
                    FrequenciaRespiratoria = classificacaoRisco.FrequenciaRespiratoria,
                    Saturacao = classificacaoRisco.Saturacao,
                    DescricaoQueixa = classificacaoRisco.DescricaoQueixa,
                    Avaliacao = classificacaoRisco.Avaliacao,
                    Sutura = classificacaoRisco.Sutura,
                    Cardiopata = classificacaoRisco.Cardiopata,
                    Diabete = classificacaoRisco.Diabete,
                    Hipertensao = classificacaoRisco.Hipertensao,
                    Outros = classificacaoRisco.Outros,
                    ObservacaoOutros = classificacaoRisco.ObservacaoOutros,
                    RenalCronico = classificacaoRisco.RenalCronico,
                    RespiratoriaCronica = classificacaoRisco.RespiratoriaCronica,
                    ObservacaoRespiratoriaCronica = classificacaoRisco.ObservacaoRespiratoriaCronica,
                    Procedencia = classificacaoRisco.Procedencia,
                    DataOcorrencia = classificacaoRisco.DataOcorrencia,
                    Pab = classificacaoRisco.Pab,
                    Paf = classificacaoRisco.Paf,
                    Cep = classificacaoRisco.Cep,
                    Logradouro = classificacaoRisco.Logradouro,
                    Numero = classificacaoRisco.Numero,
                    Complemento = classificacaoRisco.Complemento,
                    Bairro = classificacaoRisco.Bairro,
                    PessoaAlteracao = pessoaProfissionalCadastro.NomeCompleto,
                    DataAlteracao = DateTime.Now,
                    Ativo = classificacaoRisco.Ativo,
                };


                if (classificacaoRisco.EscalaDorId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.EscalaDor = _contextDominio.EscalasDor.FindAsync(classificacaoRisco.EscalaDorId).Result.Descricao;

                if (classificacaoRisco.NivelConscienciaId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.NivelConsciencia = _contextDominio.NiveisConsciencia.FindAsync(classificacaoRisco.NivelConscienciaId).Result.Descricao;

                if (classificacaoRisco.TipoChegadaId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.TipoChegada = _contextDominio.TiposChegada.FindAsync(classificacaoRisco.TipoChegadaId).Result.Descricao;

                if (classificacaoRisco.CausaExternaId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.CausaExterna = _contextDominio.CausasExternas.FindAsync(classificacaoRisco.CausaExternaId).Result.Descricao;

                if (classificacaoRisco.EspecialidadeId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.Especialidade = _contextDominio.Especialidades.FindAsync(classificacaoRisco.EspecialidadeId).Result.Descricao;

                if (classificacaoRisco.RiscoId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.Risco = _contextDominio.Riscos.FindAsync(classificacaoRisco.RiscoId).Result.Descricao;

                if (classificacaoRisco.AberturaOcularId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.AberturaOcular = _contextDominio.AberturasOculares.FindAsync(classificacaoRisco.AberturaOcularId).Result.Variavel;

                if (classificacaoRisco.RespostaVerbalId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.RespostaVerbal = _contextDominio.RespostasVerbais.FindAsync(classificacaoRisco.RespostaVerbalId).Result.Variavel;

                if (classificacaoRisco.RespostaMotoraId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.RespostaMotora = _contextDominio.RespostasMotoras.FindAsync(classificacaoRisco.RespostaMotoraId).Result.Variavel;

                if (classificacaoRisco.TipoOcorrenciaId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.TipoOcorrencia = _contextDominio.TiposOcorrencia.FindAsync(classificacaoRisco.TipoOcorrenciaId).Result.Descricao;

                if (classificacaoRisco.EstadoId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.Estado = _contextDominio.Estados.FindAsync(classificacaoRisco.EstadoId).Result.Nome;

                if (classificacaoRisco.CidadeId != Guid.Empty)
                    _ClassificacaoRiscoHistorico.Cidade = _contextDominio.Cidades.FindAsync(classificacaoRisco.CidadeId).Result.Nome;


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
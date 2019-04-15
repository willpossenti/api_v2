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
    public class ClassificacaoRiscoHistoricoService : BaseService<ClassificacaoRiscoHistorico>, IClassificacaoRiscoHistoricoService
    {
        private readonly KlinikosDbContext _context;


        public ClassificacaoRiscoHistoricoService(KlinikosDbContext context) : base(context)
        {
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
                    IMC = classificacaoRisco.IMC,
                    Temperatura = classificacaoRisco.Temperatura,
                    PressaoArterial = classificacaoRisco.PressaoArterial,
                    Pulso = classificacaoRisco.Pulso,
                    FrequenciaRespiratoria = classificacaoRisco.FrequenciaRespiratoria,
                    Saturacao = classificacaoRisco.Saturacao,
                    EscalaDor = classificacaoRisco.EscalaDor == null ? null : classificacaoRisco.EscalaDor.Descricao,
                    DescricaoQueixa = classificacaoRisco.DescricaoQueixa,
                    NivelConsciencia = classificacaoRisco.NivelConsciencia == null ? null : classificacaoRisco.NivelConsciencia.Descricao,
                    Alergia = classificacaoRisco.Alergia,
                    Sutura = classificacaoRisco.Sutura,
                    CausaExterna = classificacaoRisco.CausaExterna == null ? null : classificacaoRisco.CausaExterna.Descricao,
                    DoencaPreExistente = classificacaoRisco.DoencaPreExistente == null ? null : classificacaoRisco.DoencaPreExistente.Descricao,
                    Especialidade = classificacaoRisco.Especialidade == null ? null : classificacaoRisco.Especialidade.Descricao,
                    Risco = classificacaoRisco.Risco == null ? null : classificacaoRisco.Risco.Descricao,
                    AberturaOcular = classificacaoRisco.AberturaOcular == null ? null : classificacaoRisco.AberturaOcular.Variavel,
                    RespostaVerbal = classificacaoRisco.RespostaVerbal == null ? null : classificacaoRisco.RespostaVerbal.Variavel,
                    RespostaMotora = classificacaoRisco.RespostaMotora == null ? null : classificacaoRisco.RespostaMotora.Variavel,
                    Trauma = classificacaoRisco.Trauma == null ? null : classificacaoRisco.Trauma.Descricao,
                    Procedencia = classificacaoRisco.Procedencia,
                    TipoOcorrencia = classificacaoRisco.TipoOcorrencia == null ? null : classificacaoRisco.TipoOcorrencia.Descricao,
                    DataOcorrencia = classificacaoRisco.DataOcorrencia,
                    TipoPerfuracao = classificacaoRisco.TipoPerfuracao,
                    Cep = classificacaoRisco.Cep,
                    Logradouro = classificacaoRisco.Logradouro,
                    Numero = classificacaoRisco.Numero,
                    Complemento = classificacaoRisco.Complemento,
                    Bairro = classificacaoRisco.Bairro,
                    Estado = classificacaoRisco.Estado == null ? null : classificacaoRisco.Estado.Nome,
                    Cidade = classificacaoRisco.Cidade == null ? null : classificacaoRisco.Cidade.Nome,
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
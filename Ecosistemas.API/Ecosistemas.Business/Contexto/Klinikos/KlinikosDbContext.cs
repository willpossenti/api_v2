using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Entities.Klinikos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Contexto.Klinikos
{
    public class KlinikosDbContext: DbContext
    {
        public KlinikosDbContext()
        {
        }

        public KlinikosDbContext(DbContextOptions<KlinikosDbContext> options) : base(options)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaPaciente> PessoaPacientes { get; set; }
        public DbSet<PessoaProfissional> PessoaProfissionais { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Etnia> Etnias { get; set; }
        public DbSet<Raca> Racas { get; set; }
        public DbSet<Justificativa> Justificativas { get; set; }
        public DbSet<Nacionalidade> Nacionalidades { get; set; }
        public DbSet<OrgaoEmissor> OrgaosEmissores { get; set; }
        public DbSet<Ocupacao> Ocupacoes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<TipoCertidao> TiposCertidao { get; set; }
        public DbSet<Escolaridade> Escolaridades { get; set; }
        public DbSet<SituacaoFamiliarConjugal> SituacoesFamiliaresConjugais { get; set; }
        public DbSet<TipoProfissional> TiposProfissional { get; set; }
        public DbSet<LotacaoProfissional> LotacoesProfissional { get; set; }
        public DbSet<PessoaHistorico> PessoaHistorico { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<TipoChegada> TiposChegada { get; set; }
        public DbSet<TipoOcorrencia> TiposOcorrencia { get; set; }
        public DbSet<RegistroBoletim> RegistrosBoletim { get; set; }
        public DbSet<RegistroBoletimHistorico> RegistroBoletimHistorico { get; set; }
        public DbSet<EscalaDor> EscalasDor { get; set; }
        public DbSet<NivelConsciencia> NiveisConsciencia { get; set; }
        public DbSet<Risco> Riscos { get; set; }
        public DbSet<Alergia> Alergias { get; set; }
        public DbSet<TipoAlergia> TiposAlergia { get; set; }
        public DbSet<LocalizacaoAlergia> LocalizacoesAlergia { get; set; }
        public DbSet<ReacaoAlergia> ReacoesAlergia { get; set; }
        public DbSet<SeveridadeAlergia> SeveridadesAlergia { get; set; }
        public DbSet<AberturaOcular> AberturasOculares { get; set; }
        public DbSet<RespostaVerbal> RespostasVerbais { get; set; }
        public DbSet<RespostaMotora> RespostasMotoras { get; set; }
        public DbSet<CausaExterna> CausasExternas { get; set; }
        public DbSet<ClassificacaoRiscoAlergia> ClassificacaoRiscoAlergias { get; set; }
        public DbSet<ClassificacaoRiscoAlergiaHistorico> ClassificacaoRiscoAlergiaHistorico { get; set; }
        public DbSet<ClassificacaoRisco> ClassificacoesRisco { get; set; }
        public DbSet<ClassificacaoRiscoHistorico> ClassificacaoRiscoHistorico { get; set; }
        public DbSet<Preferencial> Preferenciais { get; set; }
        public DbSet<Acolhimento> Acolhimentos { get; set; }
        public DbSet<AcolhimentoHistorico> AcolhimentoHistorico { get; set; }
        public DbSet<TipoSaida> TiposSaida { get; set; }
        public DbSet<GrupoExame> GruposExame { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<AtendimentoMedicoExame> AtendimentoMedicoExames { get; set; }
        public DbSet<AtendimentoMedicoExameHistorico> AtendimentoMedicoExameHistorico { get; set; }
        public DbSet<CID> CID { get; set; }
        public DbSet<ModeloPrescricaoReceita> ModelosPrescricaoReceita { get; set; }
        public DbSet<ModeloPrescricaoReceitaDetalhe> ModeloPrescricaoReceitaDetalhes { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<GrupoMedicamento> GruposMedicamento { get; set; }
        public DbSet<ViaAdministracaoMedicamento> ViasAdministracaoMedicamento { get; set; }
        public DbSet<UnidadeMedicamento> UnidadesMedicamento { get; set; }
        public DbSet<IntervaloMedicamento> IntervalosMedicamento { get; set; }
        public DbSet<ModeloAtestado> ModelosAtestado { get; set; }
        public DbSet<AtendimentoMedicoPrescricaoReceita> AtendimentoMedicoPrescricoesReceitas { get; set; }
        public DbSet<AtendimentoMedicoPrescricaoReceitaHistorico> AtendimentoMedicoPrescricaoReceitaHistorico { get; set; }
        public DbSet<AtendimentoMedicoAlergia> AtendimentoMedicoAlergias { get; set; }
        public DbSet<AtendimentoMedicoAlergiaHistorico> AtendimentoMedicoAlergiaHistorico { get; set; }
        public DbSet<AtendimentoMedico> AtendimentosMedicos { get; set; }
        public DbSet<AtendimentoMedicoHistorico> AtendimentoMedicoHistorico { get; set; }
        public DbSet<ConsultaCID> ConsultasCID { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

      

    }
}

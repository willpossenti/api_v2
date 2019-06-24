
using Ecosistemas.Business.Entities.Klinikos;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<LotacaoProfissional> LotacoesProfissional { get; set; }
        public DbSet<PessoaHistorico> PessoaHistorico { get; set; }
        public DbSet<RegistroBoletim> RegistrosBoletim { get; set; }
        public DbSet<RegistroBoletimHistorico> RegistroBoletimHistorico { get; set; }
        public DbSet<ClassificacaoRiscoAlergia> ClassificacaoRiscoAlergias { get; set; }
        public DbSet<ClassificacaoRiscoAlergiaHistorico> ClassificacaoRiscoAlergiaHistorico { get; set; }
        public DbSet<ClassificacaoRisco> ClassificacoesRisco { get; set; }
        public DbSet<ClassificacaoRiscoHistorico> ClassificacaoRiscoHistorico { get; set; }
        public DbSet<Acolhimento> Acolhimentos { get; set; }
        public DbSet<AcolhimentoHistorico> AcolhimentoHistorico { get; set; }
        public DbSet<AtendimentoMedicoExame> AtendimentoMedicoExames { get; set; }
        public DbSet<AtendimentoMedicoExameHistorico> AtendimentoMedicoExameHistorico { get; set; }
        public DbSet<ModeloPrescricaoReceita> ModelosPrescricaoReceita { get; set; }
        public DbSet<ModeloPrescricaoReceitaDetalhe> ModeloPrescricaoReceitaDetalhes { get; set; }
        public DbSet<GrupoExameDetalhe> GrupoExameDetalhes { get; set; }
        public DbSet<GrupoMedicamentoDetalhe> GrupoMedicamentoDetalhes { get; set; }
        public DbSet<ModeloAtestado> ModelosAtestado { get; set; }
        public DbSet<AtendimentoMedicoPrescricaoReceita> AtendimentoMedicoPrescricoesReceitas { get; set; }
        public DbSet<AtendimentoMedicoPrescricaoReceitaDetalhe> AtendimentoMedicoPrescricaoReceitaDetalhes { get; set; }
        public DbSet<AtendimentoMedicoPrescricaoReceitaDetalheHistorico> AtendimentoMedicoPrescricaoReceitaDetalheHistorico { get; set; }
        public DbSet<AtendimentoMedicoAlergia> AtendimentoMedicoAlergias { get; set; }
        public DbSet<AtendimentoMedicoAlergiaHistorico> AtendimentoMedicoAlergiaHistorico { get; set; }
        public DbSet<AtendimentoMedico> AtendimentosMedicos { get; set; }
        public DbSet<AtendimentoMedicoHistorico> AtendimentoMedicoHistorico { get; set; }
        public DbSet<GrupoExame> GruposExame { get; set; }
        public DbSet<GrupoMedicamento> GruposMedicamento { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

      

    }
}

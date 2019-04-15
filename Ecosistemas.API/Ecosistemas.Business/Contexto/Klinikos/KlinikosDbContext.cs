using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Entities.Klinikos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Contexto.Klinikos
{
    public class KlinikosDbContext : DbContext
    {
        public KlinikosDbContext(
            DbContextOptions<KlinikosDbContext> options) : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}

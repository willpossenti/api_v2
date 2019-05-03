using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class Acolhimento
    {

        [Key]
        public Guid AcolhimentoId { get; set; }

        public virtual PessoaPaciente PessoaPaciente { get; set; }

        public virtual Especialidade Especialidade { get; set; }

        public virtual Prioridade Prioridade { get; set; }

        public bool Risco { get; set; }

        public double Peso { get; set; }

        public int Altura { get; set; }

        public double IMC { get; set; }

        public double Temperatura { get; set; }

        public int PressaoArterialSistolica { get; set; }

        public int PressaoArterialDiastolica { get; set; }

        public int Pulso { get; set; }

        public int FrequenciaRespiratoria { get; set; }

        public int Saturacao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
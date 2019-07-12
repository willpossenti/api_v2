using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Klinikos
{
    public class FilaRegistroEvento
    {

        [Key]
        public Guid FilaRegistroEventosId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataFilaRegistroEvento { get; set; }

        public virtual FilaRegistro FilaRegistro { get; set; }

        public virtual PessoaProfissional PessoaProfissional { get; set; }


        public Guid EventoId { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecosistemas.Business.Entities.Dominio
{
    public class Evento
    {

        [Key]
        public Guid EventoId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "{0} Precisa ter no máximo 20")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
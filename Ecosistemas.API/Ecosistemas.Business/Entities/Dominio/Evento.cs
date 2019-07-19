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
        [StringLength(30, ErrorMessage = "{0} Precisa ter no máximo 20")]
        public string Descricao { get; set; }

        [DataType(DataType.Text)]
        [StringLength(1, ErrorMessage = "{0} Precisa ter no máximo 1")]
        public string Sigla { get; set; }

        public bool Ativo { get; set; } = true;


    }
}
using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Dominio
{
    public interface IEventoService : IBaseService<Evento>
    {
        Task<CustomResponse<Evento>> GetByDescricao(string descricao);

    }
}

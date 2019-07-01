using Ecosistemas.Business.Entities.Dominio;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Dominio
{
    public interface IPessoaStatusService : IBaseService<PessoaStatus>
    {
        Task<CustomResponse<PessoaStatus>> GetByName(string descricao);
    }
}
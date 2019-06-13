using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Ecosistemas.Business.Interfaces.Klinikos
{
    public interface ICIDService : IBaseService<CID>
    {
        Task<CustomResponse<IList<CID>>> GetCIDByCapitulo(CID CID);
        Task<CustomResponse<List<CID>>> ConsultaCIDs(string nome, Guid userId);
    }
}
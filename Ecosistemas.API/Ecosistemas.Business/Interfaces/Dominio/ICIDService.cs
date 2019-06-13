<<<<<<< HEAD:Ecosistemas.API/Ecosistemas.Business/Interfaces/Dominio/ICIDService.cs
﻿using Ecosistemas.Business.Entities.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosistemas.Business.Interfaces.Dominio
=======
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Ecosistemas.Business.Interfaces.Klinikos
>>>>>>> sprint_yl_25052019:Ecosistemas.API/Ecosistemas.Business/Interfaces/Klinikos/ICIDService.cs
{
    public interface ICIDService : IBaseService<CID>
    {
        Task<CustomResponse<IList<CID>>> GetCIDByCapitulo(CID CID);
        Task<CustomResponse<List<CID>>> ConsultaCIDs(string nome, Guid userId);
    }
}
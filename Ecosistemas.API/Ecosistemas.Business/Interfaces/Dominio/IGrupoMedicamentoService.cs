
﻿using Ecosistemas.Business.Entities.Dominio;
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces.Dominio
{
    public interface IGrupoMedicamentoService : IBaseService<GrupoMedicamento>
    {
        Task<CustomResponse<GrupoMedicamento>> AdicionarGrupoMedicamento(GrupoMedicamento grupoMedicamento, Guid userId);
    }
}

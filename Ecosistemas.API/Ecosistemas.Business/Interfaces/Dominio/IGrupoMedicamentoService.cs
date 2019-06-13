<<<<<<< HEAD:Ecosistemas.API/Ecosistemas.Business/Interfaces/Dominio/IGrupoMedicamentoService.cs
﻿using Ecosistemas.Business.Entities.Dominio;
=======
﻿using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Utility;
>>>>>>> sprint_yl_25052019:Ecosistemas.API/Ecosistemas.Business/Interfaces/Klinikos/IGrupoMedicamentoService.cs
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

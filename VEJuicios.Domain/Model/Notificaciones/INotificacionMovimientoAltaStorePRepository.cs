﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public interface INotificacionMovimientoAltaStorePRepository
    {
        Task<int> AltaMovimiento(int notificacionId);
    }
}

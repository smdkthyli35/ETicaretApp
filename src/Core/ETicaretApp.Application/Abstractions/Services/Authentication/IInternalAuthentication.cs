﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task LoginAsync();
    }
}
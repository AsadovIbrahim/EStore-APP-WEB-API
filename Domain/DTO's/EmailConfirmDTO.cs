﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class EmailConfirmDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WEBFORMS.Views.PessoaDto
{
    public class PessoaListDto 
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Salario { get; set; }
    }
}
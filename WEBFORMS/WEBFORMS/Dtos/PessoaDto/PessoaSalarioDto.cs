using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBFORMS.Dtos.PessoaDto
{
    public class PessoaSalarioDto
    {
        public string salario_bruto { get; set; }
        public string descontos { get; set; }
        public string salario_liquido { get; set; }
    }
}
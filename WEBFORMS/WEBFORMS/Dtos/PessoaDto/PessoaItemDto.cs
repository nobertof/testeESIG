using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBFORMS.Dtos.PessoaDto
{
    public class PessoaItemDto
    {
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Pais { get; set; }
        public string Usuario { get; set; }
        public string Telefone { get; set; }
        public string Data_Nascimento { get; set; }
        public string Cargo_Id { get; set; }
    }
}
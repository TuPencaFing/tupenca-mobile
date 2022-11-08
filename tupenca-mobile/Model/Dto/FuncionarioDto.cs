﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_mobile.Model.Dto;

namespace tupenca_mobile.Model.Dto
{
    public class FuncionarioDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 40 character in length.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 40 character in length.")]

        public string Password { get; set; }
        [Required]
        public EmpresaDto? Empresa { get; set; }

    }
}

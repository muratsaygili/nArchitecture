using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Dtos;

namespace Application.Features.Users.Dtos
{
    public class RegisterUserDto:RefreshedTokenDto
    {
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.Entities;


namespace Classlibrary1.Intefaces
{
    public interface ITokenService
    {
         string? CreateToken(User appUser);
    }
}
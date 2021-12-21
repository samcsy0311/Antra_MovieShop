using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class CastService : ICastService
     {
          private readonly ICastRepository _castRepository;
          public CastService(ICastRepository castRepository)
          {
               _castRepository = castRepository;
          }
          
          public async Task<Cast> GetCast(int id)
          {
               var cast = await _castRepository.GetById(id);
               return cast;
          }
     }
}

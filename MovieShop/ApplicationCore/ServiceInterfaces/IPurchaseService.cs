using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
     public interface IPurchaseService
     {
          Task<int> AddPurchases(PurchaseRequestModel purchaseRequestModel);
          Task<Purchase> GetPurchases(int id);
     }
}

using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class PurchaseService : IPurchaseService
     {
          private readonly IPurchaseRepository _purchaseRepository;
          public PurchaseService(IPurchaseRepository purchaseRepository)
          {
               _purchaseRepository = purchaseRepository;
          }

          public async Task<int> AddPurchases(PurchaseRequestModel purchaseRequestModel)
          {
               var purchased = await _purchaseRepository.GetByUserMovieId(purchaseRequestModel.userId, purchaseRequestModel.movieId);
               if (purchased != null)
               {
                    return -1;
               }

               var newPurchase = new Purchase
               {
                    UserId = purchaseRequestModel.userId,
                    MovieId = purchaseRequestModel.movieId,
                    PurchaseNumber = purchaseRequestModel.purchaseNumber,
                    PurchaseDateTime = purchaseRequestModel.purchaseDateTime,
                    TotalPrice = purchaseRequestModel.totalPrice
               };
               newPurchase = await _purchaseRepository.Add(newPurchase);
               return newPurchase.Id;
          }
          public async Task<Purchase> GetPurchases (int id)
          {
               var purchase = await _purchaseRepository.GetById(id);
               return purchase;
          }
     }
}

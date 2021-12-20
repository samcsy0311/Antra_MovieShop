﻿namespace ApplicationCore.Entities
{
     public class User
     {
          public int Id { get; set; }
          public string? FirstName { get; set; }
          public string? LastName { get; set; }
          public DateTime? DateOfBirth { get; set; }
          public string? Email { get; set; }
          public string? HashedPassword { get; set; }
          public string? Salt { get; set; }
          public string? PhoneNumber { get; set; }
          public bool? TwoFactorEnabled { get; set; }
          public DateTime? LockOutEndDate { get; set; }
          public DateTime? LastLoginDateTime { get; set; }
          public bool? isLocked { get; set; }
          public int? AccessFailedCount { get; set; }

          //Navigation
          public List<Favorite> Favorites { get; set; }
          public List<Purchase> Purchases { get; set; }
          public List <Review> Reviews { get; set; }
     }
}

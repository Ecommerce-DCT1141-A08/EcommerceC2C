﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteThuongMaiDienTu.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EcommerceC2CA08Entities : DbContext
    {
        public EcommerceC2CA08Entities()
            : base("name=EcommerceC2CA08Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<ActivityMerchant> ActivityMerchants { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CoinPack> CoinPacks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DeliveryLocation> DeliveryLocations { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<DiscountGift> DiscountGifts { get; set; }
        public virtual DbSet<DiscountPrice> DiscountPrices { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Guarantee> Guarantees { get; set; }
        public virtual DbSet<HistoryLocked> HistoryLockeds { get; set; }
        public virtual DbSet<HistoryUseCoin> HistoryUseCoins { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<LaptopConfig> LaptopConfigs { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<MerchantOrder> MerchantOrders { get; set; }
        public virtual DbSet<MerchantOrderDetail> MerchantOrderDetails { get; set; }
        public virtual DbSet<MerchantPayment> MerchantPayments { get; set; }
        public virtual DbSet<NoticeCustomer> NoticeCustomers { get; set; }
        public virtual DbSet<NoticeMerchant> NoticeMerchants { get; set; }
        public virtual DbSet<OrderA> OrderAs { get; set; }
        public virtual DbSet<OrderDelivery> OrderDeliveries { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PhoneConfig> PhoneConfigs { get; set; }
        public virtual DbSet<Poster> Posters { get; set; }
        public virtual DbSet<PosterCategory> PosterCategories { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Webmaster> Webmasters { get; set; }
    }
}

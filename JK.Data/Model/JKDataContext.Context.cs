﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JK.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JKDataEntities : DbContext
    {
        public JKDataEntities()
            : base("name=JKDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuthorityFunction> AuthorityFunction { get; set; }
        public virtual DbSet<AuthorityRole> AuthorityRole { get; set; }
        public virtual DbSet<AuthorityRoleInFunction> AuthorityRoleInFunction { get; set; }
        public virtual DbSet<AuthorityUserInRole> AuthorityUserInRole { get; set; }
        public virtual DbSet<FXJLSetting> FXJLSetting { get; set; }
        public virtual DbSet<LotteryActivity> LotteryActivity { get; set; }
        public virtual DbSet<LotteryHistory> LotteryHistory { get; set; }
        public virtual DbSet<LotteryJackpot> LotteryJackpot { get; set; }
        public virtual DbSet<LotteryPrize> LotteryPrize { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderEvaluation> OrderEvaluation { get; set; }
        public virtual DbSet<OrderEvaluationPic> OrderEvaluationPic { get; set; }
        public virtual DbSet<OrderEvalutionReply> OrderEvalutionReply { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<OrderRefund> OrderRefund { get; set; }
        public virtual DbSet<OrderShippingMethod> OrderShippingMethod { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAlbum> ProductAlbum { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductClassification> ProductClassification { get; set; }
        public virtual DbSet<ProductParameters> ProductParameters { get; set; }
        public virtual DbSet<ProductPurchaseRecords> ProductPurchaseRecords { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<SmsRecords> SmsRecords { get; set; }
        public virtual DbSet<UserAccountWechat> UserAccountWechat { get; set; }
        public virtual DbSet<UserDeliveryAddress> UserDeliveryAddress { get; set; }
        public virtual DbSet<UserLoginRecords> UserLoginRecords { get; set; }
        public virtual DbSet<UserOperationRecords> UserOperationRecords { get; set; }
        public virtual DbSet<UserShoppingCart> UserShoppingCart { get; set; }
        public virtual DbSet<WechatPayNotify> WechatPayNotify { get; set; }
        public virtual DbSet<WechatPayRecords> WechatPayRecords { get; set; }
        public virtual DbSet<WechatPayRefundNotify> WechatPayRefundNotify { get; set; }
        public virtual DbSet<WechatPayRefundRecords> WechatPayRefundRecords { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<FriendlyLink> FriendlyLink { get; set; }
        public virtual DbSet<UserAccountFinance> UserAccountFinance { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<WithdrawCashOrder> WithdrawCashOrder { get; set; }
        public virtual DbSet<WithdrawCashRecords> WithdrawCashRecords { get; set; }
        public virtual DbSet<MyLotteryV> MyLotteryV { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<StatisticsDays> StatisticsDays { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<UserAccountBalanceLog> UserAccountBalanceLog { get; set; }
        public virtual DbSet<UserAccountFinanceChangeRecords> UserAccountFinanceChangeRecords { get; set; }
    }
}

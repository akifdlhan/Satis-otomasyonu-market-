﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DalhanSatisOtomasyon
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BarkodDBEntities : DbContext
    {
        public BarkodDBEntities()
            : base("name=BarkodDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<Terazi> Terazi { get; set; }
        public virtual DbSet<HizliUrun> HizliUrun { get; set; }
        public virtual DbSet<Islem> Islem { get; set; }
        public virtual DbSet<IslemOzet> IslemOzet { get; set; }
        public virtual DbSet<Satis> Satis { get; set; }
        public virtual DbSet<UrunGrup> UrunGrup { get; set; }
        public virtual DbSet<Barkod> Barkod { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarCodeApi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ChaominEntities : DbContext
    {
        public ChaominEntities()
            : base("name=ChaominEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VW_盤點廠商_客戶訂單資料> VW_盤點廠商_客戶訂單資料 { get; set; }
    
        public virtual int usp_盤點_資料記錄_PUT(Nullable<int> p01, Nullable<int> p02, string p03, Nullable<float> p04, Nullable<float> p05, ObjectParameter returnValue01)
        {
            var p01Parameter = p01.HasValue ?
                new ObjectParameter("P01", p01) :
                new ObjectParameter("P01", typeof(int));
    
            var p02Parameter = p02.HasValue ?
                new ObjectParameter("P02", p02) :
                new ObjectParameter("P02", typeof(int));
    
            var p03Parameter = p03 != null ?
                new ObjectParameter("P03", p03) :
                new ObjectParameter("P03", typeof(string));
    
            var p04Parameter = p04.HasValue ?
                new ObjectParameter("P04", p04) :
                new ObjectParameter("P04", typeof(float));
    
            var p05Parameter = p05.HasValue ?
                new ObjectParameter("P05", p05) :
                new ObjectParameter("P05", typeof(float));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_盤點_資料記錄_PUT", p01Parameter, p02Parameter, p03Parameter, p04Parameter, p05Parameter, returnValue01);
        }
    
        public virtual int usp_盤點_取得資料11(Nullable<int> p01, Nullable<System.DateTime> p02, Nullable<int> p03, Nullable<int> p04)
        {
            var p01Parameter = p01.HasValue ?
                new ObjectParameter("P01", p01) :
                new ObjectParameter("P01", typeof(int));
    
            var p02Parameter = p02.HasValue ?
                new ObjectParameter("P02", p02) :
                new ObjectParameter("P02", typeof(System.DateTime));
    
            var p03Parameter = p03.HasValue ?
                new ObjectParameter("P03", p03) :
                new ObjectParameter("P03", typeof(int));
    
            var p04Parameter = p04.HasValue ?
                new ObjectParameter("P04", p04) :
                new ObjectParameter("P04", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_盤點_取得資料11", p01Parameter, p02Parameter, p03Parameter, p04Parameter);
        }
    
        public virtual int usp_盤點_取得資料12(Nullable<int> p01, Nullable<System.DateTime> p02, Nullable<int> p03, Nullable<int> p04)
        {
            var p01Parameter = p01.HasValue ?
                new ObjectParameter("P01", p01) :
                new ObjectParameter("P01", typeof(int));
    
            var p02Parameter = p02.HasValue ?
                new ObjectParameter("P02", p02) :
                new ObjectParameter("P02", typeof(System.DateTime));
    
            var p03Parameter = p03.HasValue ?
                new ObjectParameter("P03", p03) :
                new ObjectParameter("P03", typeof(int));
    
            var p04Parameter = p04.HasValue ?
                new ObjectParameter("P04", p04) :
                new ObjectParameter("P04", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_盤點_取得資料12", p01Parameter, p02Parameter, p03Parameter, p04Parameter);
        }
    
        public virtual int usp_盤點_取得資料13(Nullable<int> p01, Nullable<System.DateTime> p02, string p03, Nullable<int> p04)
        {
            var p01Parameter = p01.HasValue ?
                new ObjectParameter("P01", p01) :
                new ObjectParameter("P01", typeof(int));
    
            var p02Parameter = p02.HasValue ?
                new ObjectParameter("P02", p02) :
                new ObjectParameter("P02", typeof(System.DateTime));
    
            var p03Parameter = p03 != null ?
                new ObjectParameter("P03", p03) :
                new ObjectParameter("P03", typeof(string));
    
            var p04Parameter = p04.HasValue ?
                new ObjectParameter("P04", p04) :
                new ObjectParameter("P04", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_盤點_取得資料13", p01Parameter, p02Parameter, p03Parameter, p04Parameter);
        }
    
        public virtual int usp_盤點_取得資料14(Nullable<int> p01, Nullable<System.DateTime> p02, Nullable<int> p03, Nullable<int> p04, Nullable<int> p05, string p06)
        {
            var p01Parameter = p01.HasValue ?
                new ObjectParameter("P01", p01) :
                new ObjectParameter("P01", typeof(int));
    
            var p02Parameter = p02.HasValue ?
                new ObjectParameter("P02", p02) :
                new ObjectParameter("P02", typeof(System.DateTime));
    
            var p03Parameter = p03.HasValue ?
                new ObjectParameter("P03", p03) :
                new ObjectParameter("P03", typeof(int));
    
            var p04Parameter = p04.HasValue ?
                new ObjectParameter("P04", p04) :
                new ObjectParameter("P04", typeof(int));
    
            var p05Parameter = p05.HasValue ?
                new ObjectParameter("P05", p05) :
                new ObjectParameter("P05", typeof(int));
    
            var p06Parameter = p06 != null ?
                new ObjectParameter("P06", p06) :
                new ObjectParameter("P06", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_盤點_取得資料14", p01Parameter, p02Parameter, p03Parameter, p04Parameter, p05Parameter, p06Parameter);
        }
    }
}

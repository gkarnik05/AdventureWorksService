using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdventureWorksService.WebApi.Data;

namespace AdventureWorksService.WebApi.Models.Map
{
    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> entity)
        {
            entity.ToTable("SalesOrderHeader", "Sales");
            entity.HasKey(e => e.SalesOrderId)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.HasComment("General sales order information.");

            entity.HasIndex(e => e.CustomerId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesOrderHeader_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.SalesOrderNumber)
                .HasName("AK_SalesOrderHeader_SalesOrderNumber")
                .IsUnique();

            entity.HasIndex(e => e.SalesPersonId);

            entity.Property(e => e.SalesOrderId).HasComment("Primary key.");

            entity.Property(e => e.AccountNumber).HasComment("Financial accounting number reference.");

            entity.Property(e => e.BillToAddressId).HasComment("Customer billing address. Foreign key to Address.AddressID.");

            entity.Property(e => e.Comment).HasComment("Sales representative comments.");

            entity.Property(e => e.CreditCardApprovalCode)
                .IsUnicode(false)
                .HasComment("Approval code provided by the credit card company.");

            entity.Property(e => e.CreditCardId).HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");

            entity.Property(e => e.CurrencyRateId).HasComment("Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.");

            entity.Property(e => e.CustomerId).HasComment("Customer identification number. Foreign key to Customer.BusinessEntityID.");

            entity.Property(e => e.DueDate).HasComment("Date the order is due to the customer.");

            entity.Property(e => e.Freight)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Shipping cost.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OnlineOrderFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Dates the sales order was created.");

            entity.Property(e => e.PurchaseOrderNumber).HasComment("Customer purchase order number reference. ");

            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesOrderNumber)
                .HasComment("Unique sales order identification number.")
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))");

            entity.Property(e => e.SalesPersonId).HasComment("Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.ShipDate).HasComment("Date the order was shipped to the customer.");

            entity.Property(e => e.ShipMethodId).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

            entity.Property(e => e.ShipToAddressId).HasComment("Customer shipping address. Foreign key to Address.AddressID.");

            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

            entity.Property(e => e.SubTotal)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

            entity.Property(e => e.TaxAmt)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax amount.");

            entity.Property(e => e.TerritoryId).HasComment("Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.Property(e => e.TotalDue)
                .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.")
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

            entity.HasOne(d => d.BillToAddress)
                .WithMany(p => p.SalesOrderHeaderBillToAddress)
                .HasForeignKey(d => d.BillToAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrderHeader)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipMethod)
                .WithMany(p => p.SalesOrderHeader)
                .HasForeignKey(d => d.ShipMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress)
                .WithMany(p => p.SalesOrderHeaderShipToAddress)
                .HasForeignKey(d => d.ShipToAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

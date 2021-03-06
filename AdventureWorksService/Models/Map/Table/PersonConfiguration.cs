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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Person", "Person");
            entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Person_BusinessEntityID");

            entity.HasComment("Human beings involved with AdventureWorks: employees, customer contacts, and vendor contacts.");

            entity.HasIndex(e => e.AdditionalContactInfo)
                .HasName("PXML_Person_AddContact");

            entity.HasIndex(e => e.Demographics)
                .HasName("XMLVALUE_Person_Demographics");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Person_rowguid")
                .IsUnique();

            entity.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName });

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Primary key for Person records.")
                .ValueGeneratedNever();

            entity.Property(e => e.AdditionalContactInfo).HasComment("Additional contact information about the person stored in xml format. ");

            entity.Property(e => e.Demographics).HasComment("Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.");

            entity.Property(e => e.EmailPromotion).HasComment("0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners. ");

            entity.Property(e => e.FirstName).HasComment("First name of the person.");

            entity.Property(e => e.LastName).HasComment("Last name of the person.");

            entity.Property(e => e.MiddleName).HasComment("Middle name or middle initial of the person.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");

            entity.Property(e => e.PersonType)
                .IsFixedLength()
                .HasComment("Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee (non-sales), VC = Vendor contact, GC = General contact");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.Suffix).HasComment("Surname suffix. For example, Sr. or Jr.");

            entity.Property(e => e.Title).HasComment("A courtesy title. For example, Mr. or Ms.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

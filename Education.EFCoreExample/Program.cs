using Education.EFCoreExample.Data;
using Education.EFCoreExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

using var context = new BloggingContext("Server=LINKPC;Database=Blogging;Trusted_Connection=true;TrustServerCertificate=True;");
await context.Database.EnsureCreatedAsync(); 



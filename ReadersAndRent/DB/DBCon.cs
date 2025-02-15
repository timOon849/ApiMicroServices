﻿using Microsoft.EntityFrameworkCore;
using ReadersAndRent.Model;


namespace ReadersAndRent.DB
{
    public class DBCon : DbContext
    {
        public DBCon(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Readers> Readers { get; set; }
        public DbSet<Rent> Rent { get; set; }
    }
}

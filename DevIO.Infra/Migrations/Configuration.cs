﻿

using DevIO.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace DevIO.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MeuDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}

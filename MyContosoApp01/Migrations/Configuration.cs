namespace MyContosoApp01.Migrations
{
    using MyContosoApp01.Entities_V1;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyContosoApp01.DAL.ContosoAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        
        }
    }

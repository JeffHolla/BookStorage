﻿using BLL;
using DAL;
using System;

namespace DependenciesResolver
{
    public static class DR
    {
        public static InMemoryDAL DAO { get; set; }

        public static SimpleBLL BLL { get; set; }

        static DR()
        {
            DAO = new InMemoryDAL();
            BLL = new SimpleBLL(DAO);
        }
    }
}

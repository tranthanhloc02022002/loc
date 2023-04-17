using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webbanhang.Context;

namespace Webbanhang.Models
{
    public class ProSearch
    {
        locEntities objlocEntities = new locEntities();

        public List<Product> SearchByKey(string key)
        {
            return objlocEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
    }
}
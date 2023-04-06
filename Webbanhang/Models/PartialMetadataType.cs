using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Webbanhang.Models;
namespace Webbanhang.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class UserMasterData
    {
        internal string Password;
    }
    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}
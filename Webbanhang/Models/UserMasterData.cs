using System;

namespace Webbanhang.Models
{
    public partial class UserMasterData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private string password;
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> Sex { get; set; }
    }
}


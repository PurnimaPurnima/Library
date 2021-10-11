using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            ReadBooks = new HashSet<ReadBook>();
        }

        public string Username { get; set; }
        public string PassWord { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public virtual ICollection<ReadBook> ReadBooks { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models
{
    public partial class ReadBook
    {
        public string Username { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual UserInfo UsernameNavigation { get; set; }
    }
}

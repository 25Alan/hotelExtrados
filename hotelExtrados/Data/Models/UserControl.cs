using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace hotelExtrados.Models
{
    public class UserInput
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name_User { get; set; }

        public SqlBinary Salt_Password { get; set; }

        public SqlBinary Hash_Password { get; set; }

        public bool Status_Aap { get; set; }

        public bool Status_Admin { get; set; }
    }

    public class PasswordI
    {
        public string Password { get; set; }    
    }
}

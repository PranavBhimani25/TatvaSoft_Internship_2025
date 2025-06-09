using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;

namespace User.Entities.ViewModels
{
    public class UserDetail
    {
        public UserDetail(UserDetails user)
        {
            //Id = user.Id;
            //FirstName = user.FirstName;
            //LastName = user.LastName;
            //PhoneNumber = user.PhoneNumber.;
            //EmailAddress = user.EmailAddress;
            //UserType = user.UserType;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }

    }
}

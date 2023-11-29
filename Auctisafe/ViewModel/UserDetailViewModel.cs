using Auctisafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Login = Auctisafe.Models.Login;

namespace Auctisafe.ViewModel
{
    public class UserDetailViewModel
    {

    }
    public class userviewmodel 
    {
        public UserdetaiandproductslViewModel userdetailandproduct { get; set; }
    }
    public class UserdetaiandproductslViewModel
    {
        public List<AuctionListViewModel> auctions { get; set; }
        public List<biddings> Biddings { get; set; }
        public Login usercridentail { get; set; }
        public Signup userdetails { get; set; }
    }
}
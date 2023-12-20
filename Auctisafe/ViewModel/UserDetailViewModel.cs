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
    public class PendingPaymentViewModel
    {
        public Product product { get; set; }
        public float Amount { get; set; }
        public string isReceived { get; set; }
    }
    public class ReceivePaymentVewmodel
    {
        public Product  Products{ get; set; }
        public float totalamount { get; set; }
        public float tax { get; set; }
        public float fees { get; set; }
        public float netamount { get; set; }
  
    }
    public class TransactionViewmodel
    {
        public Transaction transaction { get; set; }
        public payment pay { get; set; }
    }
}
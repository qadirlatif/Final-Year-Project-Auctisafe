using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Auctisafe.Models
{
    public class MD5_Algo
    {
        public string hashedvalue(string value)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(value));
                StringBuilder returnValue = new StringBuilder();
                for (int i = 0; i < hashData.Length; i++)
                {
                    returnValue.Append(hashData[i].ToString());
                }
                return returnValue.ToString();
            }
            catch(Exception x)
            {
                return x.ToString();
            }
        }
    }
}
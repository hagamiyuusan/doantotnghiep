using doan.Entities;
using Newtonsoft.Json;
using ZaloPay.Helper.Crypto;
using ZaloPay.Helper;
using doan.DTO.Subscription;
using doan.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doan.Helpers
{
    public static class generateAPIZaloPay
    {
        static string appid = "554";
        static string key1 = "8NdU5pG5R2spGHGhyO99HN1OhD8IQJBn";
        static string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";

        public static async Task<string> makePayment(ProductDuration itemObject, AppUser user)
        {
            var transid = Guid.NewGuid().ToString();
            var embeddata = new { merchantinfo = "embeddata123" };
            var items = new[]{
                new { itemid = itemObject.Id.ToString(), itemname = itemObject.product.Name.ToString() , itemprice = (long)Convert.ToInt32(itemObject.price), itemquantity = 1 }
            };

            var param = new Dictionary<string, string>();

            param.Add("appid", appid);
            param.Add("appuser", user.UserName);
            param.Add("apptime", Utils.GetTimeStamp().ToString());
            param.Add("amount", Convert.ToInt32(itemObject.price).ToString());
            param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transid); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embeddata", JsonConvert.SerializeObject(embeddata));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "ZaloPay demo");
            param.Add("bankcode", "");

            var data = appid + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));
            var result = await HttpHelper.PostFormAsync(createOrderUrl, param);
            return JsonConvert.SerializeObject(result);
        }
    }
}

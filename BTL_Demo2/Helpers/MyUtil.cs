using BTL_Demo2.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BTL_Demo2.Helpers
{
    public class MyUtil
    {
        public static async Task<string> GenerateCustomerKeyAsync(QuanLyCafeContext dbContext)
        {
            // Retrieve the latest customer code
            var lastCustomer = await dbContext.KhachHang.OrderByDescending(kh => kh.MaKH).FirstOrDefaultAsync();
            if (lastCustomer == null)
            {
                return "KH001"; // Start if no customers exist
            }
            // Extract and increment the number part of the last customer code
            int lastNumber = int.Parse(lastCustomer.MaKH.Substring(2));
            int nextNumber = lastNumber + 1;

            // Format the new code with leading zeros (e.g., KH002, KH010)
            return $"KH{nextNumber:D3}";
        }
    }
}


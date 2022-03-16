using Discount.GRPC.Protos;
using System;
using System.Threading.Tasks;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Basket.API.GrpcServices
{
    public class DiscountGRPCService
    {
        private readonly DiscountProtoServiceClient _discountProtoService;

        public DiscountGRPCService(DiscountProtoServiceClient discountProtoService)
        {
            this._discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}

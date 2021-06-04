using Microsoft.AspNetCore.Authorization;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Pedido.Api.Controllers
{
    [Authorize]
    public class VoucherController : MainController
    {
    }
}

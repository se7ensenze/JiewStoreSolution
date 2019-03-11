using JiewStore.Models;

namespace JiewStore.Controllers
{
    public class AddBuyAmountResultDto
    {
        public bool IsSuccess { get; internal set; }
        public string Message { get; internal set; }
        public string Code { get; internal set; }
        public float Amount { get; internal set; }
        public CustomerDto Customer { get; set; }
    }
}
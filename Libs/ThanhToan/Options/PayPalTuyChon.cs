using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.ThanhToan.Options
{
    public sealed class PayPalTuyChon
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public string Environment { get; set; } = "Sandbox";

        // URL API PayPal sandbox
        public string ApiBaseUrl { get; set; } = "https://api-m.sandbox.paypal.com";

        public string ReturnUrl { get; set; } = default!;
        public string CancelUrl { get; set; } = default!;
        public string WebhookId { get; set; } = default!;
    }

}

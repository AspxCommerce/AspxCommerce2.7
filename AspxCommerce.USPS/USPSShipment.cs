using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.USPS
{
    public class USPSShipment
    {
        public OriginAddress FromAddress { get; set; }

        public DestinationAddress ToAddress { get; set; }

        public List<ItemDetail> Items { get; set; }
        public decimal WeightInOunces { get; set; }

        public string Option { get; set; }
        public ServiceType ServiceType { get; set; }
        public string POZipCode { get; set; }
        public ImageType ImageType { get; set; }
        public ImageLayout ImageLayout { get; set; }
        public ContentType ContentType { get; set; }
        public NonDeliveryOption NonDeliveryOption { get; set; }
        public Container Container { get; set; }

        public DateTime LabelDate { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Grith { get; set; }
        public decimal GrossPound { get; set; }
        public decimal GrossOunce { get; set; }
        public string Api { get; set; }

        public bool AddressServiceRequested { get; set; }

        public USPSShipment()
        {
            SeparateReceiptPage = false;
        }

        public bool SeparateReceiptPage { get; set; }


    }

    public class ItemDetail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
    }
}

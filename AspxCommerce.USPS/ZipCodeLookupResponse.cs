using System;
using System.Collections.Generic;
using System.Text;

namespace AspxCommerce.USPS
{
    public class AddressValidateResponse
    {
        private AddressValidateResponse()
        {

        }

        private List<DestinationAddress> _Addresses;
        /// <summary>
        /// A collection of Addresses return from the Address Validation routine.
        /// </summary>
        public List<DestinationAddress> Addresses
        {
            get { return _Addresses; }
            set { _Addresses = value; }
        }



    }
}

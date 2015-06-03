using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core.Entity.RequestUrl
{
    public class RegistrationInfo
    {
        private int registrationType;
        private int status;
        public int RegistrationType
        {
            get
            {
                return this.registrationType;
            }
            set
            {
                if ((this.registrationType != value))
                {
                    this.registrationType = value;
                }
            }
        }

        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if ((this.status != value))
                {
                    this.status = value;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInvoker
{
   public  enum ErrorType
    {
       INVALID_METHOD_CALL,
       INVALID_PARAMETERS,
       INSUFFICIENT_PARAMS,
       INVALID_CLASS,
       INVALID_OBJECT_TYPE,
       SERVER_ERROR_PROCESSING,
       METHOD_NOT_FOUND

    }
}

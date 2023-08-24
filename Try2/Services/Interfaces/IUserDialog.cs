using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Context;

namespace Try2.Services.Interfaces
{
    public interface IUserDialog
    {
        public bool Add<T> (T item);

        public bool Edit<T>(T item);

        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);

    }
}

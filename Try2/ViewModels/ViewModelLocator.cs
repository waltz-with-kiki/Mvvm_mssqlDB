using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try2.ViewModels
{
    class ViewModelLocator
    {
        public AuthViewModel AuthModel => App.Services.GetRequiredService<AuthViewModel>();
        public MainViewModel MainModel => App.Services.GetRequiredService<MainViewModel>();
    }
}

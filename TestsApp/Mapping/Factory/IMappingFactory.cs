using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestsApp.Mapping
{
    public interface IMappingFactory<TEnum, TViewModel, TModel>
    {
        TModel Map(TViewModel viewModel);
    }
}

using BookStore.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Presentation.Messages
{
    internal record SelectedStoreMessage(Store SelectedStore);
    
}

//https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger
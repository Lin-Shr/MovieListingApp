using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MovieApp.Seed
{
    public interface IdbInitializer
    {
        Task Initialize();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Entities.Repositories
{
    public interface IunitOfWork:IDisposable
    {
        ICategoriesRepository Categories { get; }
        IProductsRepository Products { get; }
        IShoppingCartRepository ShoppingCarts { get; }

        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        int complete();
    }
}

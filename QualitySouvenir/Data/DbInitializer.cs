using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenir.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QualitySouvenir.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            //Look for any souvenirs
            if (context.Souvenirs.Any())
            {
                return;
            }

            var categories = new Category[] {
            new Category{Name="Maori Gifts", Description="Traditional maori souvenirs" },
            new Category{Name="Mugs", Description="Different kinds of mugs" },
            new Category{Name="T-Shirts", Description="T-Shirts made in New Zealand " },
            new Category{Name="Home&Living", Description="Home decorations" }
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]{
            new Supplier{Name="Ajay Kumar",PhoneNumber="0212345679",Email="ajay@hotmail.com"},
            new Supplier{Name="Drego Alejaro ",PhoneNumber="0243453263",Email="dalejoarroyave@gmail.com"},
            new Supplier{Name="Carl Flay",PhoneNumber="0211212123",Email="msg_me@gmail.com"},
            new Supplier{Name="Andres Villacreces",PhoneNumber="0276353535",Email="andres@hotmail.com"},
            new Supplier{Name="Camila Rod",PhoneNumber="02536374954",Email="camilarod@gmail.com"},
            new Supplier{Name="Carmen Maluenda",PhoneNumber="0267834657",Email="carmenmaluenda@gmail.com"},
            new Supplier{Name="Cory Robert",PhoneNumber="0275853565",Email="willkerk@gmail.com"},
            new Supplier{Name="Daniel Puentes",PhoneNumber="0220664614",Email="401060774@qq.com"},
            new Supplier{Name="Diana Nino",PhoneNumber="0210451297",Email="jdymrm@sina.com"},
            new Supplier{Name="Erika Abril",PhoneNumber="0210515510",Email="maldi_123@hotmail.com"},
            new Supplier{Name="Douglas Baquero",PhoneNumber="02108536902",Email="dm.np@hotmail.com"}
            };

            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();


            var souvenirs = new Souvenir[]{
            //Maori gifts
            new Souvenir{Name="Maori Patu War Club 1",Price=68, Description="Traditional maori patu war club souvenir",SupplierID=1 ,CategoryID=1,
        Image="/images/products/maorigifts/patu-war-club1.jpg"},
            new Souvenir{Name="Maori Patu War Club 2",Price=68,Description="Traditional maori patu war club souvenir",SupplierID=1,CategoryID=1,
        Image="/images/products/maorigifts/patu-war-club2.jpg"},
            new Souvenir{Name="Maori Patu War Club 3",Price=68,Description="Traditional maori patu war club souvenir",SupplierID=1,CategoryID=1,
        Image="/images/products/maorigifts/patu-war-club3.jpg"},
            new Souvenir{Name="Maori Wall Art 1",Price=22,Description="Maori wall art decorations",SupplierID=1, CategoryID=1,
        Image="/images/products/maorigifts/wall-arts-1.jpg"},
            new Souvenir{Name="Maori Wall Art2",Price=22,Description="Maori wall art decorations",SupplierID=1, CategoryID=1,
        Image="/images/products/maorigifts/wall-arts-2.jpg"},
            new Souvenir{Name="Maori Wall Art3",Price=22,Description="Maori wall art decorations",SupplierID=1, CategoryID=1,
        Image="/images/products/maorigifts/wall-arts-3.jpg"},
            new Souvenir{Name="Pendants Earringgs",Price=18,Description="Maori Pendants earringgs for girls from any age",SupplierID=1, CategoryID=1,
        Image="/images/products/maorigifts/pendants-earringgs.jpg"},
            new Souvenir{Name="Maori Patu",Price=168,Description="Traditional maori patu war club souvenir",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/patu-maori.jpg"},
            new Souvenir{Name="Maori Necklace",Price=49,Description="maori necklace, a special gift for the coming Summer",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/maori-necklace.jpg"},
            new Souvenir{Name="Maori Mask",Price=88,Description="maori style mask, suitable in festival",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/maori-mask.jpg"},
            new Souvenir{Name="Maori Hook",Price=23,Description="Maori style hook for home decorations",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/maori-hook.jpg"},
            new Souvenir{Name="Maori Gift box",Price=58,Description="Maori traditional gift box for little child",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/maori-gift-box.jpg"},
            new Souvenir{Name="Maori Dolls",Price=125,Description="Little cute Maori style dolls",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/maori-dolls.jpg"},
            new Souvenir{Name="Kotiate maori",Price=36,Description="Kotiate maori style decorations",SupplierID=2, CategoryID=1,
        Image="/images/products/maorigifts/kotiate-maori.jpg"},
        // Mugs
            new Souvenir{Name="Anchored Direction",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/anchored-direction-mug.jpg"},
            new Souvenir{Name="Bride and Groom Skeletons",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/bride-and-groom-skeletons-mug.jpg"},
            new Souvenir{Name="Dashing Through",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/dashing-through-the-no-mug.jpg"},
            new Souvenir{Name="Deep Space Diver",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/deep-space-diver-mug.jpg"},
            new Souvenir{Name="Caffeine Elixir",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=4, CategoryID=2,
        Image="/images/products/mugs/dr-caffeine-elixir-mug.jpg"},
            new Souvenir{Name="Dreamscape",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=4, CategoryID=2,
        Image="/images/products/mugs/dreamscape-mug.jpg"},
            new Souvenir{Name="Island Dream",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=4, CategoryID=2,
        Image="/images/products/mugs/island-dream-mug.jpg"},
            new Souvenir{Name="Just For You",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/just-for-you-skeletons-mug.jpg"},
            new Souvenir{Name="Just Married",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/just-married-skeletons-mug.jpg"},
            new Souvenir{Name="Kiwiana Mug",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=4, CategoryID=2,
        Image="/images/products/mugs/kiwi-az-bro-kiwiana-mug.jpg"},
            new Souvenir{Name="My Hearter",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=4, CategoryID=2,
        Image="/images/products/mugs/kiwi-to-my-heart-kiwiana-mug.jpg"},
            new Souvenir{Name="Mystery Island",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/mystery-island-mug.jpg"},
            new Souvenir{Name="The Island",Price=16,Description="Must-buy, the essential goods in every day, make you in best mood.",SupplierID=3, CategoryID=2,
        Image="/images/products/mugs/the-island-mug.jpg"},
        //T-shirts
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts1.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts2.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts3.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts4.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts5.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=5, CategoryID=3,
        Image="/images/products/tshirts/nztshirts6.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts7.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts8.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts9.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts10.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts11.jpg"},
            new Souvenir{Name="NZ Style T-shirts",Price=28,Description="With New Zealand featured decorations, very special T-shirts 100% made in NZ",SupplierID=6, CategoryID=3,
        Image="/images/products/tshirts/nztshirts12.jpg"},
        //Home&Living
            new Souvenir{Name="Coaster",Price=15,Description="NZ made coster with maori features",SupplierID=7, CategoryID=4,
        Image="/images/products/homeandliving/coaster1.jpg"},
            new Souvenir{Name="Coaster",Price=15,Description="NZ sightseeing coaster",SupplierID=7, CategoryID=4,
        Image="/images/products/homeandliving/coaster2.jpg"},
            new Souvenir{Name="Coaster",Price=15,Description="Coster with cute owls",SupplierID=7, CategoryID=4,
        Image="/images/products/homeandliving/coaster3.jpg"},
            new Souvenir{Name="Maori Draw Bag",Price=17,Description="Maori mix draw bags",SupplierID=8, CategoryID=4,
        Image="/images/products/homeandliving/maori-mix-draw-bags.jpg"},
            new Souvenir{Name="Bolster",Price=22,Description="Soft bolster, warm your the whole winter",SupplierID=8, CategoryID=4,
        Image="/images/products/homeandliving/nzbolster.jpg"},
            new Souvenir{Name="Ornament",Price=19,Description="Ornament for your home, best decorations.",SupplierID=9, CategoryID=4,
        Image="/images/products/homeandliving/ornament.jpg"},
            new Souvenir{Name="Fridge Magnet",Price=9,Description="Rimu New Zealand native bird fridge magnet",SupplierID=9, CategoryID=4,
        Image="/images/products/homeandliving/rimu-new-zealand-native-bird-fridge-magnet.jpg"},
            new Souvenir{Name="Treasure Box",Price=32,Description="Traditional Maori papahou treasure box",SupplierID=10, CategoryID=4,
        Image="/images/products/homeandliving/traditional-maori-papahou-treasure-box-paua-shell-lid.jpg"},
            new Souvenir{Name="Wall Art",Price=16,Description="Local home wall art",SupplierID=10, CategoryID=4,
        Image="/images/products/homeandliving/wall-art1.jpg"},
            new Souvenir{Name="Wall Art",Price=16,Description="Aroha wall art",SupplierID=11, CategoryID=4,
        Image="/images/products/homeandliving/wall-art2.jpg"},
            new Souvenir{Name="Wood Ornament",Price=19,Description="Wood Ornament for your home, best decorations.",SupplierID=11, CategoryID=4,
        Image="/images/products/homeandliving/wood-ornament.jpg"}
        };

            foreach (Souvenir s in souvenirs)
            {
                context.Souvenirs.Add(s);

            }
            context.SaveChanges();


        }
    }
}
